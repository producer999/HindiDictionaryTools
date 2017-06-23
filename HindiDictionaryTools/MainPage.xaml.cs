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

        private bool _isAddTranslationEnabled;
        public bool IsAddTranslationEnabled
        {
            get
            {
                _isAddTranslationEnabled = !String.IsNullOrWhiteSpace(NewTermEntryField.Text);
                return _isAddTranslationEnabled;
            }
            set
            {
                _isAddTranslationEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsAddTranslationEnabled"));
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            CurrentDictionary = new HindiDictionary("HINDI_DB_01");

            var _posEnum = Enum.GetValues(typeof(PartsOfSpeech)).Cast<PartsOfSpeech>();
            posCombo.ItemsSource = _posEnum.ToList();
        }

        private void AddNewTranslation_Click(object sender, RoutedEventArgs e)
        {
            CurrentDictionary.AddNewTranslation(NewTermEntryField.Text, NewTranslationEntryField.Text);
            NewTermEntryField.Text = "";
            NewTranslationEntryField.Text = "";
        }

        private void Search_Clicked(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(SearchBox.Text))
            {
                TranslationListView.SetFilter<HindiTranslation>(t =>
                    t.Term.Contains(SearchBox.Text) ||
                    t.ImportedTranslation.ToLower().Contains(SearchBox.Text.ToLower()) ||
                    t.UserTranslation.ToLower().Contains(SearchBox.Text.ToLower()) ||
                    t.GoogleTranslation.ToLower().Contains(SearchBox.Text.ToLower())
                    );
            }
            else
            {
                TranslationListView.RemoveFilter();
            }
        }

        //
        //Used to accept the Enter key in the Search Box to trigger the Search Button
        //NOT working possibly delete...
        private void SearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                Search_Clicked(this, new RoutedEventArgs());
                e.Handled = true;
            }    
        }

        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Search_Clicked(this, new RoutedEventArgs());
            }
            base.OnKeyDown(e);
        }
    }
}
