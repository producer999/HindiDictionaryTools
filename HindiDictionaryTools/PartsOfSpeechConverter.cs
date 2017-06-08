using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HindiDictionaryTools
{

    public enum PartsOfSpeech
    {
        Other,
        NounM,
        NounF,
        Verb,
        VerbT,
        VerbNT,
        Adjective,
        Adverb,
        Postposition,
        Pronoun,
        Conjunction,
        Interjection,
        CaseMarker
    }

    public class PartsOfSpeechConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            PartsOfSpeech pos = (PartsOfSpeech)value;

            switch (pos)
            {
                case PartsOfSpeech.NounF:
                    return "N(F)";
                case PartsOfSpeech.NounM:
                    return "N(M)";
                case PartsOfSpeech.Verb:
                    return "V";
                case PartsOfSpeech.VerbNT:
                    return "V(N-T)";
                case PartsOfSpeech.VerbT:
                    return "V(T)";
                case PartsOfSpeech.Adverb:
                    return "ADV";
                case PartsOfSpeech.Adjective:
                    return "ADJ";
                case PartsOfSpeech.Pronoun:
                    return "PRON";
                case PartsOfSpeech.Conjunction:
                    return "CONJ";
                case PartsOfSpeech.Interjection:
                    return "INTJ";
                case PartsOfSpeech.Postposition:
                    return "POSTPN";
                case PartsOfSpeech.CaseMarker:
                    return "CASE";
                default:
                    return "OTHER";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string pos = value.ToString();

            switch (pos)
            {
                case "N":
                    return PartsOfSpeech.NounF;
                case "N(F)":
                    return PartsOfSpeech.NounF;
                case "N(M)":
                    return PartsOfSpeech.NounM;
                case "V":
                    return PartsOfSpeech.Verb;
                case "V(N-T)":
                    return PartsOfSpeech.VerbNT;
                case "V(T)":
                    return PartsOfSpeech.VerbT;
                case "ADV":
                    return PartsOfSpeech.Adverb;
                case "ADJ":
                    return PartsOfSpeech.Adjective;
                case "PRON":
                    return PartsOfSpeech.Pronoun;
                case "CONJ":
                    return PartsOfSpeech.Conjunction;
                case "INTJ":
                    return PartsOfSpeech.Interjection;
                case "POSTPN":
                    return PartsOfSpeech.Postposition;
                case "CASE":
                    return PartsOfSpeech.CaseMarker;
                case "OTHER":
                    return PartsOfSpeech.Other;
                default:
                    return PartsOfSpeech.Other;
            }
        }

        public static PartsOfSpeech ImportPartOfSpeechFromString(string pos)
        {
            switch (pos)
            {
                case "N":
                    return PartsOfSpeech.NounF;
                case "NF":
                    return PartsOfSpeech.NounF;
                case "NM":
                    return PartsOfSpeech.NounM;
                case "V":
                    return PartsOfSpeech.Verb;
                case "VINT":
                    return PartsOfSpeech.VerbNT;
                case "VT":
                    return PartsOfSpeech.VerbT;
                case "ADV":
                    return PartsOfSpeech.Adverb;
                case "ADJ":
                    return PartsOfSpeech.Adjective;
                case "PRON":
                    return PartsOfSpeech.Pronoun;
                case "CONJ":
                    return PartsOfSpeech.Conjunction;
                case "INTJ":
                    return PartsOfSpeech.Interjection;
                case "POSTPOSTN":
                    return PartsOfSpeech.Postposition;
                case "CASE-MARK":
                    return PartsOfSpeech.CaseMarker;
                default:
                    return PartsOfSpeech.Other;
            }
        }
    }
}
