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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tagme_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Structs
        public class Struct
        {
            /// <summary>
            /// PagesStack is a stack that could update the back button status at MainPage.xaml.
            /// </summary>
            public class PagesStack : Stack<Type>
            {
                public new void Push(Type type)
                {
                    base.Push(type);
                    if (base.Count > 1)
                    {
                        MainPage.GlobalUpdateMainPageBackButtonStatus(true);
                    }
                }
                public new Type Pop()
                {
                    MainPage.GlobalUpdateMainPageBackButtonStatus(base.Count > 1);
                    return base.Pop();
                }
            }
        }
        public MainPage()
        {
            this.InitializeComponent();

            //Extend panel into title bar.
            ExtendPanelIntoTitleBar();

            //To make sure the page is loaded, then we can navigate without error.
            Loaded += MainPage_Loaded;
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
            Tagme_CustomizedCore.CustomizedRunningData.PageStack.Push(typeof(MainPage));
        }

        public static void GlobalUpdateMainPageBackButtonStatus(bool enable)
        {
            var page = new MainPage();
            page.UpdateBackButtonStatus(enable);
        }
        private void UpdateBackButtonStatus(bool enable)
        {
            TitleBarBackButton.IsEnabled = enable;
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

        private void TitleBarBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationFrame.CanGoBack)
            {
                NavigationFrame.GoBack();

            }
        }
    }
}
