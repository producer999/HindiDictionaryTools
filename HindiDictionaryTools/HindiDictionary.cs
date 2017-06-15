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

        //Public Fields

        public string FileName { get; set; }
        public ObservableCollection<HindiTranslation> Dictionary { get; private set; }

        //Private Fields

        private HindiTranslation _currentTranslation;
        public HindiTranslation CurrentTranslation
        {
            get { return _currentTranslation; }
            set
            {
                _currentTranslation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTranslation"));
            }
        }

        //Commands

        private DelegateCommand _updateTranslationCommand;
        public DelegateCommand UpdateTranslationCommand
        {
            get { return _updateTranslationCommand; }
            set
            {
                _updateTranslationCommand = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UpdateTranslationCommand"));
            }
        }

        private DelegateCommand _addTranslationCommand;
        public DelegateCommand AddTranslationCommand
        {
            get { return _addTranslationCommand; }
            set
            {
                _addTranslationCommand = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AddTranslationCommand"));
            }
        }

        //Constructors

        public HindiDictionary()
        {
            
            FileName = "HINDI_DB_DEFAULT";
            DBHelper.CreateDatabase(FileName);

            Dictionary = DBHelper.GetAllTranslations();

            UpdateTranslationCommand = new DelegateCommand(ExecuteUpdateTranslation, CanExecuteUpdateTranslation);

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

            UpdateTranslationCommand = new DelegateCommand(ExecuteUpdateTranslation, CanExecuteUpdateTranslation);

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

        public void UpdateTranslation()
        {
            DBHelper.UpdateTranslation(CurrentTranslation);
        }

        //Command Methods

        private bool CanExecuteUpdateTranslation(object obj)
        {
            return CurrentTranslation != null;
        }
        public void ExecuteUpdateTranslation(object obj)
        {
            DBHelper.UpdateTranslation(CurrentTranslation);
        }


        private bool CanExecuteAddTranslation(object obj)
        {
            return true;
        }
        public void ExecuteAddNewTranslation(object obj)
        {

        }
    }
}
