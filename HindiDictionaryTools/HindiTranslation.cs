using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HindiDictionaryTools
{

    public class HindiTranslation : INotifyPropertyChanged
    {

        private string _term;
        private string _userTranslation, _googleTranslation, _importedTranslation;
        private string _altTranslations, _altForms;
        private string _examples;
        private PartsOfSpeech _partOfSpeech;

        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int ID { get; set; }
        public string Term
        {
            get { return _term; }
            set
            {
                if(_term != value)
                {
                    _term = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Term"));
                }    
            }
        }
        public string UserTranslation
        {
            get { return _userTranslation; }
            set
            {
                if(_userTranslation != value)
                {
                    _userTranslation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserTranslation"));
                }            
            }
        }
        public string GoogleTranslation
        {
            get { return _googleTranslation; }
            set
            {
                if(_googleTranslation != value)
                {
                    _googleTranslation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GoogleTranslation"));
                }             
            }
        }
        public string ImportedTranslation
        {
            get { return _importedTranslation; }
            set
            {
                if(_importedTranslation != value)
                {
                    _importedTranslation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImportedTranslation"));
                }             
            }
        }
        public string AltTranslations
        {
            get { return _altTranslations; }
            set
            {
                if(_altTranslations != value)
                {
                    _altTranslations = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AltTranslations"));
                }       
            }
        }
        public string AltForms
        {
            get { return _altForms; }
            set
            {
                if(_altForms != value)
                {
                    _altForms = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AltForms"));
                }      
            }
        }
        public PartsOfSpeech PartOfSpeech
        {
            get { return _partOfSpeech; }
            set
            {
                if(_partOfSpeech != value)
                {
                    _partOfSpeech = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PartOfSpeech"));
                }        
            }
        }
        public string Examples
        {
            get { return _examples; }
            set
            {
                if(_examples != value)
                {
                    _examples = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Examples"));
                }            
            }
        }
 
        public event PropertyChangedEventHandler PropertyChanged;

        public HindiTranslation()
        {

        }

        //Constructor to use for importing translations from the text files

        public HindiTranslation(string term, string importTrans, string pos, string examples="")
        {
            _term = term;
            _userTranslation = "";
            _googleTranslation = "";
            _importedTranslation = importTrans;
            _partOfSpeech = PartsOfSpeechConverter.ImportPartOfSpeechFromString(pos);
            _examples = examples;
        }

        //Constructor to use for manual translation entry

        public HindiTranslation(string term, string userTrans)
        {
            _term = term;
            _userTranslation = userTrans;
            _googleTranslation = "";
            _importedTranslation = "";
            _partOfSpeech = PartsOfSpeech.Other;
        }



    }
}
