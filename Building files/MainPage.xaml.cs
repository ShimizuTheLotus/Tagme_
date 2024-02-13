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
        //Running data
        public static class RunningData
        {
            public static Stack<Type> PageStack = new Stack<Type>();
        }

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
            RunningData.PageStack.Push(typeof(MainPage));
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
            UpdateBackButtonStatus();
        }


        //UI updates
        private void UpdateBackButtonStatus()
        {
            TitleBarBackButton.IsEnabled = RunningData.PageStack.Count > 1;
        }
        

        //UI events
        private void TitleBarBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationFrame.CanGoBack)
            {
                NavigationFrame.GoBack();
                RunningData.PageStack.Pop();
                if (RunningData.PageStack.Count < 2)
                {
                    TitleBarBackButton.IsEnabled = false;
                }
            }
        }
    }
}
