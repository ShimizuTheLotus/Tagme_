using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        //Initialize UI shadow
        public void InitializeShadow()
        {
            DataBaseCoverPanel.Translation += new System.Numerics.Vector3(0, 0, 16);
            CreateButtonPanel.Translation += new System.Numerics.Vector3(0, 0, 16);
        }
    }
}
