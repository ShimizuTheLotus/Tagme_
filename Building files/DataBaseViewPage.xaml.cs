using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tagme_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataBaseViewPage : Page
    {
        public DataBaseViewPage()
        {
            this.InitializeComponent();

            Loaded += DataBaseViewPage_Loaded;
        }

        private void DataBaseViewPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetDataBaseCoverImageOnPropertyPagePanel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("DataBaseViewPageOptionBarConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(OperationPanel);
            }
            anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("DataBaseViewPageOptionBarSortAppBarButtonConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(OptionBarSortAppBarButton);
            }
            anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("DataBaseViewPageOptionBarViewModeAppBarButtonConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(OptionBarViewModeAppBarButton);
            }
            anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("DataBaseViewPageOptionCommandBarBrowseLabelConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(OptionBarBrowseLabel);
            }
            anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("DataBaseViewPageOptionsCommandBarSeparator1ConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(OptionBarCommandBarSeparator1);
            }
            anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("DataBaseViewPageOptionBarCreateDataBaseAppBarButtonConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(OptionBarAddItemAppBarButton);
            }
            anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("DataBaseViewPageOptionBarMultiSelectAppBarButtonConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(OptionBarMultiSelectAppBarButton);
            }
            anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("DataBaseViewPageOptionCommandBarEditLabelConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(OptionBarEditLabel);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackDataBaseViewPageOptionBarConnectedAnimation", OperationPanel);
                animation.Configuration = new DirectConnectedAnimationConfiguration();
                animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackDataBaseViewPageOptionBarSortAppBarButtonConnectedAnimation", OptionBarSortAppBarButton);
                animation.Configuration = new DirectConnectedAnimationConfiguration();
                animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackDataBaseViewPageOptionBarViewModeAppBarButtonConnectedAnimation", OptionBarViewModeAppBarButton);
                animation.Configuration = new DirectConnectedAnimationConfiguration();
                animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackDataBaseViewPageOptionCommandBarBrowseLabelConnectedAnimation", OptionBarBrowseLabel);
                animation.Configuration = new DirectConnectedAnimationConfiguration();
                animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackDataBaseViewPageOptionsCommandBarSeparator1ConnectedAnimation", OptionBarCommandBarSeparator1);
                animation.Configuration = new DirectConnectedAnimationConfiguration();
                animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackDataBaseViewPageOptionCommandBarEditLabelConnectedAnimation", OptionBarEditLabel);
                animation.Configuration = new DirectConnectedAnimationConfiguration();
                try
                {
                    animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackDataBaseViewPageOptionBarCreateDataBaseAppBarButtonConnectedAnimation", OptionBarAddItemAppBarButton);
                    animation.Configuration = new DirectConnectedAnimationConfiguration();
                }
                catch { }
                try
                {
                    animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackDataBaseViewPageOptionBarMultiSelectAppBarButtonConnectedAnimation", OptionBarMultiSelectAppBarButton);
                    animation.Configuration = new DirectConnectedAnimationConfiguration();
                }catch(Exception ex) { }



            }
        }


        //Functions
        private void SetDataBaseCoverImageOnPropertyPagePanel()
        {
            if (File.Exists(Tagme_CoreUWP.CoreRunningData.Tagme_DataBase.UsingDataBasePath))
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={Tagme_CoreUWP.CoreRunningData.Tagme_DataBase.UsingDataBasePath}"))
                {
                    db.Open();

                    //Get image
                    SqliteCommand selectCommand = new SqliteCommand($"SELECT {Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Item.DataBaseCover.Name} FROM {Tagme_CoreUWP.Tagme_DataBaseConst.BasicDataBaseInfo.Name}");
                    selectCommand.Connection = db;
                    OptionBarCurrentDataBasePropertyPageButtonImage.Source = ShimizuCoreUWP.TypeService.ByteToBitmapImage((byte[])selectCommand.ExecuteScalar());

                    db.Close();
                }
            }
        }


        private void OptionBarCurrentDataBasePropertyPageButton_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to DataBasePropertyPage
        }
    }
}
