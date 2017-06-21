using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HindiDictionaryTools
{
    public static class DBHelper
    {

        public static string DB_PATH {get; set;}

        //create database with specified name
        //the path should usually be Windows.Storage.ApplicationData.Current.LocalFolder
        public static bool CreateDatabase(string DB_NAME)
        {
            DB_NAME += ".sqlite";

            //If specified databse file does not exist, ask to import data from a text file into a new database
            if (!CheckFileExists(DB_NAME).Result)
            {
                DB_PATH = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, DB_NAME);

                using (SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {              
                    conn.CreateTable<HindiTranslation>();
                    
                }

                //TranslationDataParser.ImportFromText();

                return true;
            }
            else
            {
                DB_PATH = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, DB_NAME);
                return false;
            }
        }

        //check if the file filename exists
        private static async Task<bool> CheckFileExists(string filename)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                return true;
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
