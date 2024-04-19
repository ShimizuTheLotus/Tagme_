using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tagme_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateDataBasePage : Page
    {
        public CreateDataBasePage()
        {
            this.InitializeComponent();

            Loaded += CreateDataBasePage_Loaded;
            SizeChanged += CreateDataBasePage_SizeChanged;
        }


        //Loaded
        private void CreateDataBasePage_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeShadow();
        }

        //SizeChanged
        private void CreateDataBasePage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //throw new NotImplementedException();
            FitLayout();
        }
        private void FitLayout()
        {
            //Reorder flyout
            if (DataBaseCoverImage.ActualWidth / BasePanel.ActualWidth < 0.4)
            {
                EditInfoPanel.Orientation = Orientation.Horizontal;
                DataBaseNameTextBox.Width = BasePanel.ActualWidth 
                    - DataBaseCoverPanel.ActualWidth 
                    - DataBaseCoverPanel.Margin.Left 
                    - DataBaseCoverPanel.Margin.Right
                    - MovableSettingsPanel.Margin.Left
                    - MovableSettingsPanel.Margin.Right
                    - DataBaseNameTextBox.Margin.Left
                    - DataBaseNameTextBox.Margin.Right;
            }
            else
            {
                EditInfoPanel.Orientation = Orientation.Vertical;
                DataBaseNameTextBox.Width = BasePanel.ActualWidth - 2 * DataBaseNameTextBox.Margin.Left;
            }
        }

        //Initialization
        //Initialize UI shadow
        public void InitializeShadow()
        {
            DataBaseCoverPanel.Translation += new System.Numerics.Vector3(0, 0, 16);
            CreateButtonPanel.Translation += new System.Numerics.Vector3(0, 0, 64);
        }


        //Functions
        //Remind naming the database.
        private void RemindNamingDataBase()
        {
            //Update the property list
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            TextBlock originalTextBlock = (TextBlock)FindName("PropertyList_DataBaseName");
            originalTextBlock.Text = resourceLoader.GetString("CreateDataBasePage/PropertyList/DataBaseName/Text") + DataBaseNameTextBox.Text;

            if (DataBaseNameTextBox.Text != string.Empty)
            {
                //Database name is Not Empty
                StackPanel stackPanel = (StackPanel)FindName("PropertyList_DataBaseNamePanel");
                TextBlock newTextBlock = new TextBlock();
                newTextBlock.Name = "PropertyList_DataBaseName";
                newTextBlock.Text = originalTextBlock.Text;
                newTextBlock.Style = originalTextBlock.Style;
                int index = stackPanel.Children.IndexOf(originalTextBlock);
                if (index != -1)
                {
                    stackPanel.Children.Remove(originalTextBlock);
                    stackPanel.Children.Insert(index, newTextBlock);
                }

                //Delete NoEmptyDataBaseNameReminder
                stackPanel = (StackPanel)FindName("MovableSettingsPanel");
                TextBlock textBlock = (TextBlock)FindName("NoEmptyDataBaseNameReminder");
                index = stackPanel.Children.IndexOf(textBlock);
                if (index != -1)
                {
                    stackPanel.Children.RemoveAt(index);
                }
            }
            else
            {
                //Empty
                originalTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);

                //Add NoEmptyDataBaseNameReminder
                StackPanel stackPanel = (StackPanel)FindName("MovableSettingsPanel");
                TextBlock textBlock = new TextBlock();
                textBlock.Name = "NoEmptyDataBaseNameReminder";
                textBlock.Text = resourceLoader.GetString("CreateDataBasePage/CS/InputInfo/NoEmptyDataBaseName/Text");
                SolidColorBrush solidColorBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                textBlock.Foreground = solidColorBrush;
                textBlock.Margin = new Thickness(16, 0, 8, 8);
                int index = stackPanel.Children.IndexOf(DataBaseNameTextBox);
                if (index != -1)
                {
                    stackPanel.Children.Insert(index + 1, textBlock);
                }
            }
        }


        //UI events

        private void DataBaseNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RemindNamingDataBase();
        }

        //Main buttons
        private void CancelCreateDataBaseButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void CreateDataBaseButton_Click(object sender, RoutedEventArgs e)
        {
            RemindNamingDataBase();
        }
    }
}
