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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HindiDictionaryTools
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public HindiDictionary CurrentDictionary { get; set; }


        public MainPage()
        {
            this.InitializeComponent();

            CurrentDictionary = new HindiDictionary("HINDI_DB_01");

            var _posEnum = Enum.GetValues(typeof(PartsOfSpeech)).Cast<PartsOfSpeech>();
            posCombo.ItemsSource = _posEnum.ToList();
        }

        private void AddNewTranslation_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(NewTermEntryField.Text))
            {
                CurrentDictionary.AddNewTranslation(NewTermEntryField.Text, NewTranslationEntryField.Text);
            }
        }
    }
}
