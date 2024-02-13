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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tagme_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDataBasePage : Page
    {
        public AddDataBasePage()
        {
            this.InitializeComponent();

            Loaded += AddDataBasePage_Loaded;
        }

        //Functions
        //Loaded
        private void AddDataBasePage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        //Over rides
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MainPage.RunningData.PageStack.Push(typeof(AddDataBasePage));
        }
    }
}
