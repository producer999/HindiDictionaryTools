using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyToolkit.Controls;
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HindiDictionaryTools
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public HindiDictionary CurrentDictionary { get; set; }

        private string _currentSearchFilter;
        public string CurrentSearchFilter
        {
            get { return _currentSearchFilter; }
            set
            {
                _currentSearchFilter = value;
                if (!String.IsNullOrWhiteSpace(_currentSearchFilter))
                {
                    TranslationListView.SetFilter<HindiTranslation>(t =>
                        t.Term.Contains(_currentSearchFilter) ||
                        t.ImportedTranslation.ToLower().Contains(_currentSearchFilter.ToLower()) ||
                        t.UserTranslation.ToLower().Contains(_currentSearchFilter.ToLower()) ||
                        t.GoogleTranslation.ToLower().Contains(_currentSearchFilter.ToLower()) 
                        
                        );
                }
                else
                {
                    TranslationListView.RemoveFilter();
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentSearchFilter"));
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            CurrentDictionary = new HindiDictionary("HINDI_DB_01");

            var _posEnum = Enum.GetValues(typeof(PartsOfSpeech)).Cast<PartsOfSpeech>();
            PartOfSpeechSelector.ItemsSource = _posEnum.ToList();
        }

        private void AddNewTranslation_Click(object sender, RoutedEventArgs e)
        {
            CurrentDictionary.AddNewTranslation(NewTermEntryField.Text, NewTranslationEntryField.Text);
            NewTermEntryField.Text = "";
            NewTranslationEntryField.Text = "";
        }
    }
}
