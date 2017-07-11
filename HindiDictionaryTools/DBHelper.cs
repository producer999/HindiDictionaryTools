using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace HindiDictionaryTools
{
    public static class DBHelper
    {
        public static string DB_NAME { get; set; }
        public static string DB_PATH {get; set;}

        //create database with specified name
        //the path should usually be Windows.Storage.ApplicationData.Current.LocalFolder
        public static bool CreateDatabase(string filename)
        {
            DB_NAME = filename + ".sqlite";
      
            //If specified databse file does not exist, create a new blank database with correct columns
            if (!CheckFileExists(DB_NAME))
            {
                DB_PATH = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, DB_NAME);

                using (SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {              
                    conn.CreateTable<HindiTranslation>();
                    
                }

                return true;
            }
            else
            {
                DB_PATH = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_NAME);
                return false;
            }
        }

        public static async Task<bool> LoadDatabaseFromFileAsync()
        {
            try
            {
                FileOpenPicker picker = new FileOpenPicker();
                picker.SuggestedStartLocation = PickerLocationId.Desktop;
                picker.FileTypeFilter.Add(".sqlite");
                StorageFile file = await picker.PickSingleFileAsync();

                if (file != null)
                {
                    await file.CopyAsync(ApplicationData.Current.LocalFolder, DB_NAME, NameCollisionOption.ReplaceExisting);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> CopyDatabaseAsync()
        {
            try
            {
                FileSavePicker picker = new FileSavePicker();
                picker.SuggestedStartLocation = PickerLocationId.Desktop;
                picker.FileTypeChoices.Add("SQLite3 Database", new List<string>() { ".sqlite" });
                picker.DefaultFileExtension = ".sqlite";
                picker.SuggestedFileName = "Database";

                StorageFile newFile = await picker.PickSaveFileAsync();
                StorageFile existingFile = await StorageFile.GetFileFromPathAsync(DB_PATH);

                await existingFile.CopyAndReplaceAsync(newFile);

                return true;
            }
            catch
            {
                return false;
            }           
        }

        //check if the file filename exists in the Local data Folder
        private static bool CheckFileExists(string filename)
        {
            try
            {
                // THIS ASYNC CALL DOES NOT WORK WITH RELEASE BUILD
                //var store = await Windows.Storage.ApplicationData.Current.LocalFolder.TryGetItemAsync(filename);
                //return store != null;
                string folder = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
                FileInfo fi = new FileInfo(folder + "\\" + filename);

                return fi.Exists;
            }
            catch
            {
                return false;
            }
        }

        //check if the file filename exists in the Package Assets data Folder
        private static bool CheckAssetExists(string filename)
        {
            try
            {
                string folder = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                FileInfo fi = new FileInfo(folder + "\\" + filename);

                return fi.Exists;
            }
            catch
            {
                return false;
            }
        }

        //Insert new definition into the Definition table
        public static void Insert(HindiTranslation newTranslation)
        {
            using (SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(newTranslation);
                });
            }
        }

        //Retrieve a definition from the database by id
        public static HindiTranslation GetTranslation(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                var def = conn.Query<HindiTranslation>("SELECT * FROM HindiTranslation WHERE ID =" + id).FirstOrDefault();
                return def;
            }
        }

        //Return a list of all definitions in the database
        public static ObservableCollection<HindiTranslation> GetAllTranslations()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {
                    List<HindiTranslation> translations = conn.Table<HindiTranslation>().ToList();
                    ObservableCollection<HindiTranslation> translationList = new ObservableCollection<HindiTranslation>(translations);

                    return translationList;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        //Update existing definition in the database
        public static void UpdateTranslation(HindiTranslation translation)
        {
            using (SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                var existingEntry = conn.Query<HindiTranslation>("SELECT * FROM HindiTranslation WHERE ID =" + translation.ID).FirstOrDefault();

                if (existingEntry != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Update(translation);
                    });
                }
            }
        }

        //Clear the database of all definitions
        public static void DeleteAllTranslations()
        {
            using (SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                conn.DropTable<HindiTranslation>();
                conn.CreateTable<HindiTranslation>();
                conn.Dispose();
                conn.Close();
            }
        }

        //Delete specific definition from the database
        public static void DeleteTranslation(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                var existingEntry = conn.Query<HindiTranslation>("SELECT * FROM HindiTranslation where ID =" + id).FirstOrDefault();

                if (existingEntry != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Delete(existingEntry);
                    });
                }
            }
        }

    }
}
