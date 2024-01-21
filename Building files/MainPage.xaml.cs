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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Tagme_
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //Initialize
            InitializeTitleBar();
            InitializeShadows();
            InitializeStatusPanel();

            //Repeating Tasks
            KeepUpdateStatusBarDataBaseStorageInfo();
        }

        //Functions

        //Initializations
        /// <summary>
        /// Initialize TitileBar.
        /// </summary>
        public void InitializeTitleBar()
        {
            //Extend view into TitleBar
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(DragBar);
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            //Set TitleBar button colot
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        /// <summary>
        /// Initialize the shadow of the UI elements.
        /// </summary>
        private void InitializeShadows()
        {
            PageSharedShadow.Receivers.Clear();
            OptionBarRelativePanel.Translation += new Vector3(0, 0, 32);
            BrowseStatusRelativePanel.Translation += new Vector3(0, 0, 32);
            DatabaseStorageStatusRelativePanel.Translation += new Vector3(0, 0, 32);
            SearchDatabaseAutoSuggestBox.Translation += new Vector3(0, 0, 32);
        }

        /// <summary>
        /// InitializeStatusPanel
        /// </summary>
        private void InitializeStatusPanel()
        {
            BrowseSortStatusIcon.Rotation = 90;
        }

        /// <summary>
        /// Initialize the ListView of database list.
        /// </summary>
        private void InitializeDataBaseListView()
        {

        }


        //Repeating tasks
        /// <summary>
        /// Get database storage usage every few seconds and update the info on the status bar.
        /// </summary>
        private async void KeepUpdateStatusBarDataBaseStorageInfo()
        {
            while (true)
            {
                //options

                await Task.Delay(4000);
            }
        }

        private void SearchDatabaseAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void SearchDatabaseAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }
    }
}
