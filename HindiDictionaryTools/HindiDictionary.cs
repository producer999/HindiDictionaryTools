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
                IsCurrentTranslationSelected = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTranslation"));
            }
        }

        private bool _isCurrentTranslationSelected;
        public bool IsCurrentTranslationSelected
        {
            get
            {
                _isCurrentTranslationSelected = CurrentTranslation != null;
                return _isCurrentTranslationSelected;
            }
            set
            {
                _isCurrentTranslationSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCurrentTranslationSelected"));
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
                IsCurrentTranslationSelected = true;
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

                CurrentTranslation = newTranslation;
                IsCurrentTranslationSelected = true;
            }         
        }

        public async void ImportTranslationsFromFile()
        {
            if(await TranslationDataParser.ImportFromText())
            {
                Dictionary = DBHelper.GetAllTranslations();
                CurrentTranslation = Dictionary.First();
                IsCurrentTranslationSelected = true;

                //force PropertyChanged event on the Dictionary or it will not refresh in the ListView
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Dictionary"));
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
            if(CurrentTranslation != null)
            {
                DBHelper.DeleteTranslation(CurrentTranslation.ID);
                Dictionary.Remove(CurrentTranslation);
                CurrentTranslation = null;
                IsCurrentTranslationSelected = false;
            }     
        }

        public void DeleteAllTranslations()
        {
            DBHelper.DeleteAllTranslations();
            Dictionary.Clear();
            CurrentTranslation = null;
            IsCurrentTranslationSelected = false;
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
