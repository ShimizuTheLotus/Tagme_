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
            InitializeUIPositions(1000);
            InitializeDataBaseListView();

            //Repeating Tasks
            KeepUpdateStatusBarDataBaseStorageInfo();
        }

        //Functions

        //Debug

        /// <summary>
        /// Starting debug.
        /// </summary>
        public void DebugStart()
        {
            Tagme_CoreUWP.Debug.IsDebug = true;

            //UI change
            OptionsCommandBarDebugPart.Visibility = Visibility.Visible;
            ShowDebugInfoBar();

            //Tasks
            KeepShadowExisting();
        }

        /// <summary>
        /// Stopping debug.
        /// </summary>
        public void DebugStop()
        {
            Tagme_CoreUWP.Debug.IsDebug = false;

            //UI recover
            OptionsCommandBarDebugPart.Visibility= Visibility.Collapsed;
            HideDebugInfoBar();
        }

        //UI movements
        /// <summary>
        /// Show debug info bar.
        /// </summary>
        public void ShowDebugInfoBar()
        {
            DebugInfoPanel.Margin = new Thickness(8, 8, 8, 8);
            DataBaseListView.Margin = new Thickness(8, 8, DebugInfoPanel.Margin.Right + DebugInfoPanel.ActualWidth + 8, 8);
        }

        /// <summary>
        /// Hide debug info bar.
        /// </summary>
        public void HideDebugInfoBar()
        {
            DebugInfoPanel.Margin = new Thickness(8, 8, MainPagePanel.Margin.Right + 8 - 1280 - DebugInfoPanel.ActualWidth, 8);
            DataBaseListView.Margin = new Thickness(8, 8, MainPagePanel.Margin.Right + 8, 8);
        }


        //Notice push
        /// <summary>
        /// Push the information.
        /// </summary>
        /// <param name="noticeTitle">The title of a push</param>
        /// <param name="description">The description of the info.</param>
        /// <param name="severity">The severity of the info.</param>
        public void PushNotice(string noticeTitle, string description = "", Tagme_CoreUWP.Struct.PushInfoSeverity severity = Tagme_CoreUWP.Struct.PushInfoSeverity.Informational)
        {

            //options

        }

        //Initializations
        /// <summary>
        /// Initialize TitleBar.
        /// </summary>
        public void InitializeTitleBar()
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
        /// Initialize the shadow of the UI elements.
        /// </summary>
        private void InitializeShadows()
        {
            try
            {
                PageSharedShadow.Receivers.Clear();
                OptionBarRelativePanel.Translation += new Vector3(0, 0, 16);
                BrowseStatusRelativePanel.Translation += new Vector3(0, 0, 16);
                DatabaseStorageStatusRelativePanel.Translation += new Vector3(0, 0, 16);
                Tagme_DebugStatusPanel.Translation += new Vector3(0, 0, 16);
                SearchDatabaseAutoSuggestBox.Translation += new Vector3(0, 0, 16);
                DebugInfoPanel.Translation += new Vector3(0, 0, 64);
            }
            catch (Exception ex)
            {
                PushNotice("Error", ex.ToString(), Tagme_CoreUWP.Struct.PushInfoSeverity.Error);
            }
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
            DataBaseListView.Items.Clear();
            Tagme_CustomizedCore.DataBaseListViewSource.Clear();

            //Get databases that exists.
            List<string> ExistDataBasePathsList = Tagme_CoreUWP.Tagme_DataBaseOption.GetExistDataBasesList();

            try
            {
                foreach (string dbpath in ExistDataBasePathsList) 
                {
                    string dbTitle = "";
                    byte[] dbCover = null;
                    string createdTime = "";
                    string modifiedTime = "";
                    string dbFileSize = "";
                    string subitemCount = "";

                    using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                    {
                        db.Open();

                        //Get dbTitle
                        SqliteCommand selectCommand = new SqliteCommand("SELECT @Item FROM @Table");
                        selectCommand.Connection = db;
                        selectCommand.Parameters.Clear();
                        selectCommand.Parameters.AddWithValue("@Item", Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Item.DataBaseName.Name);
                        selectCommand.Parameters.AddWithValue("@Table", Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Name);
                        SqliteDataReader reader = selectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            dbTitle = reader.GetString(0);
                        }
                        //Get dbCover
                        selectCommand = new SqliteCommand("SELECT @Item FROM @Table");
                        selectCommand.Connection = db;
                        selectCommand.Parameters.Clear();
                        selectCommand.Parameters.AddWithValue("@Item", Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Item.DataBaseCover.Name);
                        selectCommand.Parameters.AddWithValue("@Table", Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Name);
                        dbCover = (byte[])selectCommand.ExecuteScalar();
                        //Get createdTime
                        selectCommand = new SqliteCommand("SELECT @Item FROM @Table");
                        selectCommand.Connection = db;
                        selectCommand.Parameters.Clear();
                        selectCommand.Parameters.AddWithValue("@Item", Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Item.CreatedTimeStamp.Name);
                        selectCommand.Parameters.AddWithValue("@Table", Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Name);
                        reader = selectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            createdTime = reader.GetString(0);
                        }
                        //Get modifiedTime
                        selectCommand = new SqliteCommand("SELECT @Item FROM @Table");
                        selectCommand.Connection = db;
                        selectCommand.Parameters.Clear();
                        selectCommand.Parameters.AddWithValue("@Item", Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Item.LastModifiedTimeStamp.Name);
                        selectCommand.Parameters.AddWithValue("@Table", Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Name);
                        reader = selectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            modifiedTime = reader.GetString(0);
                        }
                        //Get dbFileSize
                        DirectoryInfo directoryInfo = new DirectoryInfo(dbpath);
                        FileInfo[] fileInfoList = directoryInfo.GetFiles();
                        dbFileSize = fileInfoList[0].Length.ToString();

                        //Get subitemCount


                        Tagme_CustomizedCore.DataBaseListViewSource.Add(new Tagme_CustomizedCore.TempLates.DataBaseListViewTemplate
                        {
                            DataBasePath = dbpath,
                            DataBaseTitle = dbTitle,
                            DataBaseCover = NekoWahsCoreUWP.TypeService.ByteToImage(dbCover),
                            DataBaseCreatedTime = createdTime,
                            DataBaseModifiedTime = modifiedTime,
                            DataBaseFileSize = dbFileSize,
                            DataBaseAllSubItemCount = subitemCount,
                        });
                        db.Close();
                    }
                }
            }
            catch (Exception ex) { }

            DataBaseListView.ItemsSource = null;
            DataBaseListView.ItemsSource = Tagme_CustomizedCore.DataBaseListViewSource;
            ApplyDataBaseListViewChildShadow();

        }

        private async void ApplyDataBaseListViewChildShadow()
        {
            await Task.Delay(500);
            foreach (var item in DataBaseListView.Items)
            {
                ListViewItem item2 = DataBaseListView.ContainerFromItem(item) as ListViewItem;
                if (item2 != null)
                {
                    item2.Background = null;
                    RelativePanel relativePanel = NekoWahsCoreUWP.UIXAML.FindElementByName(item2, "DataBaseListViewTemplateBackground") as RelativePanel; ;
                    relativePanel.Translation += new Vector3(0, 0, 32);
                }
            }
        }

        /// <summary>
        /// Initialize the position of UI elements
        /// </summary>
        /// <param name="delay">To make sure the UI initializes properly, a delay is a necessary.</param>
        private async void InitializeUIPositions(Int16 delay)
        {
            await Task.Delay(delay);

            HideDebugInfoBar();
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

        /// <summary>
        /// Keep the shadow of controls existing.
        /// </summary>
        private async void KeepShadowExisting()
        {
            while(true)
            {
                InitializeShadows();
                await Task.Delay(4000);
            }
        }

        private void SearchDatabaseAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void SearchDatabaseAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        /// <summary>
        /// The button in debug panel clicked, then switch the debug mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DebugIOButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Tagme_CoreUWP.Debug.IsDebug)
            {
                DebugIOButton.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Color.FromArgb(0x6A,0xA9,0xA9,0xFF));
                DebugStart();
            }
            else
            {
                DebugIOButton.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Color.FromArgb(0x00,0xFF,0xFF,0xFF));
                DebugStop();
            }
        }

        private void DataBaseListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DataBaseListView.SelectedItems.Count > 0)
            {
                Tagme_CoreUWP.CoreRunningData.Tagme_DataBase.UsingDataBasePath = Tagme_CustomizedCore.DataBaseListViewSource[DataBaseListView.SelectedIndex].DataBasePath;

                //Navigate
                Frame.Navigate(typeof(DataBaseViewPage));
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Add database page
            Frame.Navigate(typeof(DataBaseViewPage));
        }
    }
}
