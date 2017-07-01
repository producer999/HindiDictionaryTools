using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HindiDictionaryTools
{
    public class AltTranslationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string data = (string)value;

            if(String.IsNullOrWhiteSpace(data))
            {
                return new ObservableCollection<string>();
            }
            else
            {
                TranslationAltTranslations t = Newtonsoft.Json.JsonConvert.DeserializeObject<TranslationAltTranslations>(data);

                ObservableCollection<string> output = new ObservableCollection<string>();

                for (int i = 0; i < t.altTrans.Length; i++)
                {
                    output.Add(t.altTrans[i]);
                }
                return output;
            }   
        }

        //
        // This ConvertBack() method is never called with TwoWay Binding... it doesn't work for now
        // See readme for more info
        //
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class AltFormsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string data = (string)value;

            if (String.IsNullOrWhiteSpace(data))
            {
                return new ObservableCollection<string>();
            }
            else
            {
                TranslationAltForm af = Newtonsoft.Json.JsonConvert.DeserializeObject<TranslationAltForm>(data);

                ObservableCollection<string> output = new ObservableCollection<string>();

                for (int i = 0; i < af.altForms.Length; i++)
                {
                    output.Add(af.altForms[i]);
                }
                return output;
            }           
        }

        //
        // This ConvertBack() method is never called with TwoWay Binding... it doesn't work for now
        // See readme for more info
        //
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class ExamplesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string data = (string)value;

            if (String.IsNullOrWhiteSpace(data))
            {
                return new ObservableCollection<string>();
            }
            else
            {
                TranslationExamples e = Newtonsoft.Json.JsonConvert.DeserializeObject<TranslationExamples>(data);

                ObservableCollection<string> output = new ObservableCollection<string>();

                for (int i = 0; i < e.examples.Length; i++)
                {
                    output.Add(e.examples[i]);
                }
                return output;
            }          
        }

        //
        // This ConvertBack() method is never called with TwoWay Binding... it doesn't work for now
        // See readme for more info
        //
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {

            var data = value as ObservableCollection<string>;
            string output = "";

            output += "{ \"examples\": [ ";

            if(data.Count > 0)
            {
                for(int i=0; i<data.Count; i++)
                {
                    output += "\"" + data[i] + "\"";
                    if(i != data.Count-1)
                    {
                        output += ", ";
                    }
                }
            }

            output += " ]}";           
            return output;
        }
    }
}
