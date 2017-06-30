using System;
using System.Collections.Generic;
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

            TranslationAltTranslations t = Newtonsoft.Json.JsonConvert.DeserializeObject<TranslationAltTranslations>(data);

            string output = "";

            for (int i = 0; i < t.altTrans.Length; i++)
            {
                output += t.altTrans[i];

                if(i != t.altTrans.Length-1)
                    output += ", ";
            }
            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //string data = (string)value;
            return value;
        }
    }

    public class AltFormsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string data = (string)value;

            TranslationAltForm af = Newtonsoft.Json.JsonConvert.DeserializeObject<TranslationAltForm>(data);

            string output = "";

            for (int i = 0; i < af.altForms.Length; i++)
            {
                output += af.altForms[i];

                if(i != af.altForms.Length-1)
                    output += ", ";
            }
            return output;
        }

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

            TranslationExamples e = Newtonsoft.Json.JsonConvert.DeserializeObject<TranslationExamples>(data);

            //string output = "";

            //for (int i = 0; i < e.examples.Length; i++)
            //{
            //    output += e.examples[i];

            //    if(i != e.examples.Length-1)
            //        output += ", ";
            //}
            //return output;

            List<string> output = new List<string>();

            for (int i = 0; i < e.examples.Length; i++)
            {
                output.Add(e.examples[i]);
            }
            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
