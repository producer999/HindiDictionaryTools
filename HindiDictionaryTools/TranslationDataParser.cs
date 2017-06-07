using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace HindiDictionaryTools
{
    public static class TranslationDataParser
    {
        public static async void ImportFromText()
        {
            try
            {
                FileOpenPicker picker = new FileOpenPicker();
                picker.SuggestedStartLocation = PickerLocationId.Desktop;
                picker.FileTypeFilter.Add(".txt");
                StorageFile file = await picker.PickSingleFileAsync();

                if (file != null)
                {
                    await ParseTranslations(file);
                    //return true;
                }
                else
                {
                    //return false;
                }
            }
            catch
            {
                //return false;
            }
        }
        
        private static async Task<bool> ParseTranslations(StorageFile file)
        {
            var lines = await FileIO.ReadLinesAsync(file);
            List<string> importedList = lines.ToList<string>();
            List<string> errors = new List<string>();

            foreach (var i in importedList)
            {
                Debug.WriteLine("PROCESSING: " + i);
                Debug.WriteLine("");
                string[] delimiters = { "[", "]\"", "]”", "]{}\"", "]{}”", "\"(", "”(", ")<", ">;" };
                string[] data = i.Split(delimiters, StringSplitOptions.None);

                string term, def, posstr, ex;
                string[] pos;

                if (data.Length == 6)
                {

                    term = data[1];

                    if (!String.IsNullOrEmpty(data[2]))
                        def = data[2];
                    else
                        def = "";

                    pos = data[3].Split(new char[] { ',' });
                    posstr = pos[0];

                    if (!String.IsNullOrEmpty(data[5]))
                        ex = data[5];
                    else
                        ex = "";

                    DBHelper.Insert(new HindiTranslation(term, def, posstr, ex));
                }

                else
                {
                    errors.Add(i);
                    errors.Add("");
                    Debug.WriteLine("ERROR ON IMPORT - ADDED TO LOG");
                    Debug.WriteLine("");
                    for (int j = 0; j < data.Length; j++)
                    {
                        Debug.WriteLine("data " + j + ": " + data[j]);
                        errors.Add("data " + j + ": " + data[j]);
                    }
                    errors.Add("");
                }

                Debug.WriteLine("");
            }

            errors.ForEach(y => Debug.WriteLine(y));

            Debug.WriteLine("ALL DONE!");

            return true;
        }
    }
}
