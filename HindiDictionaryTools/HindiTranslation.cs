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
                _term = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Term"));
            }
        }
        public string UserTranslation
        {
            get { return _userTranslation; }
            set
            {
                _userTranslation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserTranslation"));
            }
        }
        public string GoogleTranslation
        {
            get { return _googleTranslation; }
            set
            {
                _googleTranslation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GoogleTranslation"));
            }
        }
        public string ImportedTranslation
        {
            get { return _importedTranslation; }
            set
            {
                _importedTranslation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImportedTranslation"));
            }
        }
        public string AltTranslations
        {
            get { return _altTranslations; }
            set
            {
                _altTranslations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AltTranslations"));
            }
        }
        public string AltForms
        {
            get { return _altForms; }
            set
            {
                _altForms = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AltForms"));
            }
        }
        public PartsOfSpeech PartOfSpeech
        {
            get { return _partOfSpeech; }
            private set
            {
                _partOfSpeech = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PartOfSpeech"));
            }
        }
        public string Examples
        {
            get { return _examples; }
            set
            {
                _examples = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Examples"));
            }
        }
        

     

        public event PropertyChangedEventHandler PropertyChanged;

        public HindiTranslation()
        {

        }

        public HindiTranslation(string term, string importTrans, string pos, string examples="")
        {
            _term = term;
            _userTranslation = "";
            _googleTranslation = "";
            _importedTranslation = importTrans;
            _partOfSpeech = PartsOfSpeechConverter.ImportPartOfSpeechFromString(pos);
            _examples = examples;
        }



    }
}
