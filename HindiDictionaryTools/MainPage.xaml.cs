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
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HindiDictionaryTools
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public HindiDictionary CurrentDictionary { get; set; }

        private string _currentSearchFilter;

        public MainPage()
        {
            this.InitializeComponent();

            CurrentDictionary = new HindiDictionary("HINDI_DB_01_LG");

            var _posEnum = Enum.GetValues(typeof(PartsOfSpeech)).Cast<PartsOfSpeech>();
            PartOfSpeechSelector.ItemsSource = _posEnum.ToList();
        }

        public bool SearchFilter(Object o)
        {
            var t = o as HindiTranslation;

            if (t.Term.Contains(_currentSearchFilter) ||
                        t.ImportedTranslation.ToLower().Contains(_currentSearchFilter.ToLower()) ||
                        t.UserTranslation.ToLower().Contains(_currentSearchFilter.ToLower()) ||
                        t.GoogleTranslation.ToLower().Contains(_currentSearchFilter.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AddNewTranslation_Click(object sender, RoutedEventArgs e)
        {
            CurrentDictionary.AddNewTranslation(NewTermEntryField.Text, NewTranslationEntryField.Text);
            NewTermEntryField.Text = "";
            NewTranslationEntryField.Text = "";
        }

        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            _currentSearchFilter = sender.Text;
            TranslationGrid.View.Filter = SearchFilter;
            TranslationGrid.View.RefreshFilter();
        }

    }
}
