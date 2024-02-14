using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Numerics;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tagme_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //Extend panel into title bar.
            ExtendPanelIntoTitleBar();

            //To make sure the page is loaded, then we can navigate without error.
            Loaded += MainPage_Loaded;

            //Initialize
        }

        //Initializations
        /// <summary>
        /// To make sure the page is loaded, then we can navigate without error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ShowMainPage();
            NavigationFrame.Navigated += NavigationFrame_Navigated;
        }
        
        /// <summary>
        /// Initialize TitleBar.
        /// </summary>
        public void ExtendPanelIntoTitleBar()
        {
            //Extend view into TitleBar
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(DragBar);
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            //Set TitleBar button color
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        /// <summary>
        /// This "MainPage" is not refered to MainPage.xaml!
        /// </summary>
        public void ShowMainPage()
        {
            //Show database list page.
            NavigationFrame.Navigate(typeof(DataBaseListPage));
        }

        //Events
        private void NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //Not at home page.
            if (NavigationFrame.CanGoBack)
            {
                TitleBarBackButton.IsEnabled = true;
                //RelativePanel.AlignCenterWithPanel = "True"
                ProgramTitle.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
                UpdateProgramTitle();
            }
            else
            {
                TitleBarBackButton.IsEnabled = false;
                //Let ProgramTitle align left.
                ProgramTitle.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
                UpdateProgramTitle();
            }
        }


        //Update UI
        //Update ProgramTitle
        /// <summary>
        /// Change the text of ProgramTitle and fit it with current page.
        /// </summary>
        private void UpdateProgramTitle()
        {
            //Title text
            string title = "Tagme_";
            //CUrrent page name
            string pageName = NavigationFrame.Content.ToString();
            //Resouece getter(.resw)
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            switch (pageName)
            {
                //At create database page
                case "Tagme_.CreateDataBasePage":
                    title += $" - {resourceLoader.GetString("MainPage/CS/StatusTitle/ProgramTitle_CreateDataBase/Text")}";
                    break;
                default:
                    break;
            }
            ProgramTitle.Text = title;
        }
        

        //UI events
        private void TitleBarBackButton_Click(object sender, RoutedEventArgs e)
        {
            //Update go back button usability status
            if (NavigationFrame.CanGoBack)
            {
                NavigationFrame.GoBack();
                if (NavigationFrame.CanGoBack)
                {
                    TitleBarBackButton.IsEnabled = true;
                }
                else
                {
                    TitleBarBackButton.IsEnabled = false;
                }
            }
        }
    }
}
