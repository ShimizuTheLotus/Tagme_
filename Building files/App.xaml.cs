using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Tagme_
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        //Classes
        /// <summary>
        /// NekoWahs services that could be used to variety of programs.
        /// These functions must not ref any variety or functions of a certain program. 
        /// Compatibility Caution: Windows UWP
        /// </summary>
        public class NekoWahsUWP
        {

        }

        /// <summary>
        /// Tagme_ core that has the main functions of a Tagme_ Program
        /// Compatibility Caution: Windows UWP
        /// Requirements: NuGet Package - Microsoft.Data.Sqlite
        /// </summary>
        public class Tagme_CoreUWP
        {
            /// <summary>
            /// The consts that Tagme_ uses.
            /// </summary>
            public class Consts
            {
                /// <summary>
                /// The folder of Tagme_ infos
                /// </summary>
                public static StorageFolder Tagme_InfoPath = ApplicationData.Current.LocalFolder;
                /// <summary>
                /// The database path of a database that records the paths of all Tagme_ databases.
                /// </summary>
                public static string dataBasePathsDataBasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path,"dbpathsDB.db");
            }

            /// <summary>
            /// The options of Tagme_ Databases.
            /// </summary>
            public class DataBaseOptions
            {
                /// <summary>
                /// Check if a database exists.
                /// </summary>
                public void CheckIfDataBaseExists(string dataBasePath)
                {

                }

                /// <summary>
                /// Check all the databases if they're exist.
                /// It will return a list of databases that dosen't exist.
                /// </summary>
                public List<string> CheckIfAllDataBaseExist()
                {
                    List<string> dataBaseNotExistList = new List<string>();

                    //options

                    return dataBaseNotExistList;
                }

                /// <summary>
                /// Create a database at a spacific path.
                /// </summary>
                /// <param name="CreatePath"></param>
                public void CreateAndInitializeDataBase(string CreatePath)
                {

                }
            }
        }

        /// <summary>
        /// The data that Tagme_ needs while running.
        /// </summary>
        public class Tagme_RunningData
        {
            /// <summary>
            /// The paths of all databases.
            /// </summary>
            public static List<string> dataBasePaths = new List<string>();
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
