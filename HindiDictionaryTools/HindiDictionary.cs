using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HindiDictionaryTools
{
    public enum SortStatus
    {
        SortByIdAscend,
        SortByIdDescend,
        SortByTermAscend,
        SortByTermDescend,
        SortByTransAscend,
        SortByTransDescend
    }

    public class HindiDictionary : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private ObservableCollection<HindiTranslation> _dictionary = new ObservableCollection<HindiTranslation>();
        private HindiTranslation _currentTranslation;
        
        public ObservableCollection<HindiTranslation> Dictionary { get; private set; }

        public HindiTranslation CurrentTranslation
        {
            get { return _currentTranslation; }
            set
            {
                _currentTranslation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTranslation"));
            }
        }

        public string FileName { get; set; }

        public HindiDictionary()
        {
            FileName = "HINDI_DB_01";
            DBHelper.CreateDatabase(FileName);

            Dictionary = DBHelper.GetAllTranslations();

            if (Dictionary.Count == 0)
            {
                CurrentTranslation = null;
            }
            else
            {
                CurrentTranslation = Dictionary.First();
            }
        }

        public HindiDictionary(string fileName)
        {
            FileName = fileName;
            DBHelper.CreateDatabase(FileName);

            Dictionary = DBHelper.GetAllTranslations();

            if (Dictionary.Count == 0)
            {
                CurrentTranslation = null;
            }
            else
            {
                CurrentTranslation = Dictionary.First();
               
            }
        }

        public void AddNewTranslation(HindiTranslation newTranslation)
        {
            Dictionary.Add(newTranslation);
            DBHelper.Insert(newTranslation);
        }


    }
}
