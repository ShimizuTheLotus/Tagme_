using Microsoft.Data.Sqlite;
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

    //Classes
    /// <summary>
    /// NekoWahs services that could be used to variety of programs.
    /// These functions must not ref any variety or functions of a certain program. 
    /// Compatibility Caution: Windows UWP
    /// Core version: 0.0.0
    /// </summary>
    public class NekoWahsCoreUWP
    {
        /// <summary>
        /// The constant values that could be changed by developers or users.
        /// If it will be changed by the users, the developer should save the customized values by themselves and initialize the values each time the app runs to apply the user settings.
        /// </summary>
        public class CustomizableConsts
        {
            /// <summary>
            /// If file size is over this value, the core will suggest to open file with stream.
            /// Measurement:Byte
            /// The default value 1048576 Byte is 2 MiB.
            /// </summary>
            //Compatibility Caution: Int64
            public static Int64 thresholdSizeOfFileOpenMode = 1048576;
        }

        public class Struct
        {
            /// <summary>
            /// The status used when checking if file is exist or opening file.
            /// </summary>
            public enum FileGetStatus
            {
                Unknown,
                PathIsEmpty,
                NotExist,
                NotExistAndFailedToCreate,
                JustCreated,
                Exist,
                ExistButFailedToOpen
            }

            /// <summary>
            /// The mode for opening file.
            /// FileNotExist: The  target file is not exist.
            /// AllIn: Use it for small file, usually text files.
            /// Stream: Use stream to read file.
            /// Auto: NekoWahsCore will judge to choose the file open mode automatically.
            /// </summary>
            public enum SuggestedFileOpenMode
            {
                FileNotExist,
                AllIn,
                Stream,
                Auto
            }
        }

        /// <summary>
        /// Options about files.
        /// </summary>
        public class File
        {
            /// <summary>
            /// Check if the path exists
            /// </summary>
            /// <param name="path">File path</param>
            /// <param name="createIfNotExist">When the value is true, the core will try to create a file. Plus, if the directory of the file not exist, the core will also try to creat it.</param>
            /// <returns>The status of the target file.</returns>
            public Struct.FileGetStatus AccessFileChecker(string path, bool createIfNotExist)
            {
                if (System.IO.File.Exists(path))
                {
                    return Struct.FileGetStatus.Exist;
                }
                //Direction is null or empty string.
                if (path == null || path == string.Empty)
                {
                    return Struct.FileGetStatus.PathIsEmpty;
                }
                //Directory not exists.
                if (!System.IO.Directory.Exists(Path.GetDirectoryName(path)))
                {
                    if (createIfNotExist)
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path));
                        }
                        catch
                        {
                            return Struct.FileGetStatus.NotExistAndFailedToCreate;
                        }
                    }
                    else
                    {
                        return Struct.FileGetStatus.NotExist;
                    }
                }

                //Directory failed to be created.
                if (!System.IO.Directory.Exists(Path.GetDirectoryName(path)))
                {
                    return Struct.FileGetStatus.NotExistAndFailedToCreate;
                }
                else
                {
                    try
                    {
                        System.IO.File.Create(Path.GetDirectoryName(path));
                    }
                    catch
                    {
                        return Struct.FileGetStatus.NotExistAndFailedToCreate;
                    }
                }
                //File failed to be created.
                if (!System.IO.File.Exists(path))
                {
                    return Struct.FileGetStatus.NotExistAndFailedToCreate;
                }

                //File now exists.
                return Struct.FileGetStatus.JustCreated;
            }

            /// <summary>
            /// Make sure a file exists and suggest
            /// </summary>
            public Struct.SuggestedFileOpenMode SuggestOpenFileMode(string path)
            {
                var x = new NekoWahsCoreUWP.File();
                //File exists
                if (x.AccessFileChecker(path, false) != Struct.FileGetStatus.PathIsEmpty && x.AccessFileChecker(path, false) != Struct.FileGetStatus.NotExist)
                {
                    if (new FileInfo(path).Length <= CustomizableConsts.thresholdSizeOfFileOpenMode)
                    {
                        return Struct.SuggestedFileOpenMode.AllIn;
                    }
                    else
                    {
                        return Struct.SuggestedFileOpenMode.Stream;
                    }
                }
                else
                {
                    return Struct.SuggestedFileOpenMode.FileNotExist;
                }
            }
        }
    }

    /// <summary>
    /// Tagme_ core that has the main functions of a Tagme_ Program
    /// Compatibility Caution: Windows UWP
    /// Tagme_ core should not ref anything not in itself except NekoWahs core.
    /// Requirements: NuGet Package - Microsoft.Data.Sqlite
    /// </summary>
    public class Tagme_CoreUWP
    {
        /// <summary>
        /// The consts that Tagme_ uses.
        /// </summary>
        public class Const
        {
            /// <summary>
            /// The folder of Tagme_ info.
            /// </summary>
            public static StorageFolder Tagme_InfoPath = ApplicationData.Current.LocalFolder;
            /// <summary>
            /// The database path of a database that records info of Tagme_ core needs, including paths of all Tagme_ databases.
            /// </summary>
            public static string CoreInfoDataBasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "dbpathsDB.db");
        }

        //Struct
        public class Struct
        {
            /// <summary>
            /// The severity of a pushed info.
            /// </summary>
            public enum PushInfoSeverity
            {
                Informational,
                Success,
                Warning,
                Error
            }

            /// <summary>
            /// Use it as a function return when the function is for creating a Tagme_ database.
            /// </summary>
            public enum DataBaseCreateFailedReason
            {
                Unknown,//Failed cause of unknown error
                Success,//File created successfully, in fact it's not an error
                NameUsed//The file using the name already exists
            }

            public enum DataBaseUpdateFailedReason
            {
                Unknown,
                Success,
            }

            public enum DataBaseDeleteFailedReason
            {
                Unknown,
                Success,
            }
        }

        /// <summary>
        /// The data Tagme_ needs for running.
        /// </summary>
        public class CoreRunningData
        {
            /// <summary>
            /// Lists of Tagme_ databases.
            /// </summary>
            public class Tagme_DataBasesList
            {
                /// <summary>
                /// The paths of all databases.
                /// </summary>
                public static List<string> dataBasePaths = new List<string>();
                /// <summary>
                /// The paths that Tagme_ database files exist.
                /// </summary>
                public static List<string> dataBasePathsExistFile = new List<string>();
            }
            /// <summary>
            /// The data about Single Tagme_ database.
            /// </summary>
            public class Tagme_DataBase
            {
                /// <summary>
                /// The data needed to simulate folders.
                /// </summary>
                public class SimulatedFolder
                {
                    /// <summary>
                    /// A stack of opened folders
                    /// The last element is the ID of the folder that is being used.
                    /// </summary>
                    public static Stack<string> UsingFolderIDStack;
                }
            }
        }

        /// <summary>
        /// Functions for getting and setting info.
        /// </summary>
        public class InfoManager
        {
            /// <summary>
            /// Initialize the database for Tagme_ core info.
            /// Please check if the dbpath exists a db file before using this function.
            /// </summary>
            /// <param name="dbpath">The path of string</param>
            public void InitializeInfoDataBase(string dbpath = "")
            {
                if (dbpath == "") dbpath = Const.CoreInfoDataBasePath;

                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand("CREATE TABLE IF NOT EXISTS SETTINGS(" +
                        "NAME TEXT, " +
                        "VALUE TEXT ", db);
                    insertCommand.ExecuteNonQuery();
                    insertCommand = new SqliteCommand("CREATE TABLE IF NOT EXISTS SETTINGS(" +
                        "PATH TEXT)", db);
                    insertCommand.ExecuteNonQuery();

                    db.Close();
                }
            }

            /// <summary>
            /// Get info of a list of database path.
            /// Not checked if the databases are exist.
            /// </summary>
            /// <returns>A list of database path that storaged in Tagme_ info database.</returns>
            public List<string> GetDataBasePathList()
            {
                List<string> getDataBasePathList = new List<string>();

                var x = new NekoWahsCoreUWP.File();
                if (x.AccessFileChecker(Const.CoreInfoDataBasePath, true) == NekoWahsCoreUWP.Struct.FileGetStatus.Exist)
                {
                    //options
                }
                else if(x.AccessFileChecker(Const.CoreInfoDataBasePath, true) == NekoWahsCoreUWP.Struct.FileGetStatus.JustCreated)
                {
                    InitializeInfoDataBase();
                }
                else
                {
                    //Tagme_ info database not exists and failed to create.
                }

                return getDataBasePathList;
            }

            /// <summary>
            /// Log the database path in the Tagme_ info database.
            /// </summary>
            /// <param name="path">The path waiting to be logged in Tagme_ info database</param>
            /// <returns>Is logging success?</returns>
            public bool LogDataBasePath(string path)
            {
                var x = new NekoWahsCoreUWP.File();
                NekoWahsCoreUWP.Struct.FileGetStatus accessStatus = x.AccessFileChecker(path, true);
                if (accessStatus == NekoWahsCoreUWP.Struct.FileGetStatus.Exist)
                {
                    //options
                }
                //The db was not exist but now just created a new one, now it need to be initialized.
                else if (accessStatus == NekoWahsCoreUWP.Struct.FileGetStatus.JustCreated)
                {
                    //options Initialize the info db
                }
                //Failed to create
                else
                {
                    return false;
                }
                return true;
            }

            /// <summary>
            /// Remove a provided Tagme_ database path in Tagme_ info database.
            /// </summary>
            /// <param name="path">The path waiting to be removed from Tagme_ info database.</param>
            public void RemoveSingleDataBasePath(string path)
            {

            }

            /// <summary>
            /// Remove the paths in the provided list from Tagme_ info database.
            /// </summary>
            /// <param name="paths">The path waiting to be removed from Tagme_ info database.</param>
            public void RemoveDataBasePaths(List<string> paths)
            {
                foreach (string path in paths)
                {
                    RemoveSingleDataBasePath(path);
                }
            }
        }

        /// <summary>
        /// The string value of a table or a colume of Tagme_ database
        /// </summary>
        public class Tagme_DataBaseConsts
        {
            /// <summary>
            /// A table with basic database info.
            /// </summary>
            public class BasicDataBaseInfo
            {
                const string Name = "BasicDataBaseInfo";
                public class Column
                {
                    public class DataBaseName
                    {
                        public const string Name = "DataBaseName";
                        public const string SQLiteType = "TEXT";
                    }
                    public class DataBaseCover
                    {
                        public const string Name = "DataBaseCover";
                        public const string SQLiteType = "BLOB";
                    }
                    public class CreatedTime
                    {
                        public const string Name = "CreatedTime";
                        public const string SQLiteType = "TEXT";
                    }
                    public class LastEditTime
                    {
                        public const string Name = "LastEditTime";
                        public const string SQLiteType = "TEXT";
                    }
                    public class LastViewTime
                    {
                        public const string Name = "LastViewTime";
                        public const string SQLiteType = "TEXT";
                    }
                    public class Tagme_DataBaseVersion
                    {
                        public const string Name = "Tagme_DataBaseVersion"; 
                        public const string SQLiteType = "TEXT";
                    }

                }
            }
        }

        /// <summary>
        /// The options of Tagme_ Databases.
        /// </summary>
        public class Tagme_DataBaseOptions
        {
            /// <summary>
            /// Check if a database exists.
            /// </summary>
            public bool CheckIfDataBaseExists(string dataBasePath)
            {
                var x = new NekoWahsCoreUWP.File();
                if (x.AccessFileChecker(dataBasePath, false) == NekoWahsCoreUWP.Struct.FileGetStatus.Exist)
                {
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Check all the databases in list if they're exist.
            /// It will return a list of databases that dosen't exist.
            /// </summary>
            /// <param name="targetpathList">A list filled with database paths that waiting to check if the databases are exist.</param>
            /// <returns>A list of paths that dosen't refer to any file.</returns>
            public List<string> CheckIfAllDataBaseInListExist(List<string> targetpathList)
            {
                List<string> dataBaseNotExistList = new List<string>();

                foreach (string path in targetpathList)
                {
                    if (!CheckIfDataBaseExists(path))
                    {
                        dataBaseNotExistList.Add(path);
                    }
                }

                return dataBaseNotExistList;
            }

            /// <summary>
            /// Create a database at a spacific path.
            /// </summary>
            /// <param name="CreatePath">The path that database will be created</param>
            /// <param name="dataBaseFileName">The name of the database file, it will be used as file name.</param>
            /// <param name="dataBaseName">The name of the database, it will be logged in the database rather than used as the file name.</param>
            /// <param name="cover">The cover image of the database</param>
            public Tagme_CoreUWP.Struct.DataBaseCreateFailedReason CreateAndInitializeTagme_DataBase(string CreatePath,string dataBaseFileName, string dataBaseName, byte[] cover)
            {

                //options

                //When created successfully
                return Struct.DataBaseCreateFailedReason.Success;
            }

            /// <summary>
            /// Open a folder in a Tagme_ database file.
            /// </summary>
            /// <param name="FolderID">The ID of the folder being opened</param>
            public void OpenFolder(string FolderID)
            {
                CoreRunningData.Tagme_DataBase.SimulatedFolder.UsingFolderIDStack.Push(FolderID);
            }

            /// <summary>
            /// Go back to previous level of folder.
            /// Compatibility Caution: Int64
            /// </summary>
            public void GoBack(Int64 times = 1)
            {
                for (Int64 i = times; i >= 0; i--)
                {
                    try
                    {
                        CoreRunningData.Tagme_DataBase.SimulatedFolder.UsingFolderIDStack.Pop();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// The items related with debugging.
        /// </summary>
        public class Debug
        {
            /// <summary>
            /// Whether if Tagme_ in debug mode.
            /// </summary>
            public static bool IsDebug = false;
        }
    }

    /// <summary>
    /// Part of the data that Tagme_ needs while running which not included in the Tagme_Core cause it's not a necessary, such as customized UI.
    /// It's not in the Tagme_Core cause it could be customerized frequently and not always necessary to Tagme_.
    /// </summary>
    public class Tagme_CustomizedRunningData
    {

    }
}
