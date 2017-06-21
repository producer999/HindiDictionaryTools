using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

        private DelegateCommand _addNewTranslationCommand;
        public DelegateCommand AddNewTranslationCommand
        {
            get { return _addNewTranslationCommand; }
            set
            {
                _addNewTranslationCommand = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AddNewTranslationCommand"));
            }
        }

        //Constructors

        public HindiDictionary()
        {
            /*
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
            */
        }

        public HindiDictionary(string fileName)
        {
            FileName = fileName;
            DBHelper.CreateDatabase(FileName);

            Dictionary = DBHelper.GetAllTranslations();

            AddNewTranslationCommand = new DelegateCommand(AddNewTranslation, CanExecuteAddNewTranslation);

            if (Dictionary.Count == 0)
            {
                CurrentTranslation = null;
            }
            else
            {
                CurrentTranslation = Dictionary.First();
               
            }
        }

        public void AddNewTranslation(string newTerm, string newUserTranslation)
        {
            if(!String.IsNullOrWhiteSpace(newTerm))
            {
                if(String.IsNullOrWhiteSpace(newUserTranslation))
                {
                    newUserTranslation = "";
                }

                HindiTranslation newTranslation = new HindiTranslation(newTerm, newUserTranslation);

                Dictionary.Add(newTranslation);
                DBHelper.Insert(newTranslation);
            }         
        }

        public async void ImportTranslationsFromFile()
        {
            if(await TranslationDataParser.ImportFromText())
            {
                //Dictionary.Clear();
                Dictionary = DBHelper.GetAllTranslations();
                CurrentTranslation = Dictionary.First();
            }        
        }

        public void UpdateCurrentTranslation()
        {
            if(CurrentTranslation != null)
            {
                DBHelper.UpdateTranslation(CurrentTranslation);
            }            
        }

        public void DeleteCurrentTranslation()
        {
            DBHelper.DeleteTranslation(CurrentTranslation.ID);
            Dictionary.Remove(CurrentTranslation);
            CurrentTranslation = null;
        }

        public void DeleteAllTranslations()
        {
            DBHelper.DeleteAllTranslations();
            Dictionary.Clear();
            CurrentTranslation = null;
        }

        public async void UpdateCurrentGoogleTranslation()
        {
            if(CurrentTranslation != null)
            {
                try
                {
                    string googleTrans = await GoogleTranslateService.Translate(CurrentTranslation.Term);

                    CurrentTranslation.GoogleTranslation = googleTrans;
                    UpdateCurrentTranslation();
                }
                catch
                {
                    Debug.WriteLine("Attempt to get Google Translation for " + CurrentTranslation.Term + " failed.");
                }
            }             
        }

        //Command Methods

        private bool CanExecuteAddNewTranslation(object parameter)
        {
            return parameter != null;
        }
        public void AddNewTranslation(object parameter)
        {
            DBHelper.UpdateTranslation(CurrentTranslation);
        }

    }
}
