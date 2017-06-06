using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HindiDictionaryTools
{
    public class GoogleTranslationResponse
    {
        public GoogleTranslationData data;

        public GoogleTranslationResponse()
        {

        }
        public GoogleTranslationResponse(GoogleTranslationData res)
        {
            data = res;
        }

    }

    public class GoogleTranslationData
    {
        public GoogleTranslation[] translations;

        public GoogleTranslationData()
        {

        }
        public GoogleTranslationData(GoogleTranslation[] trans)
        {
            translations = trans;
        }
    }

    public class GoogleTranslation
    {
        public string translatedText;

        public GoogleTranslation()
        {
            translatedText = "";
        }
        public GoogleTranslation(string trans)
        {
            translatedText = trans;
        }
    }

    public static class GoogleTranslateService
    {
        public static async Task<string> Translate(string hindiWord)
        {
            if (!String.IsNullOrEmpty(hindiWord))
            {
                Uri uri = new Uri("https://translation.googleapis.com/language/translate/v2?key=AIzaSyC1uP0Uw1jEoDFv61cIzLVK2bP4J3E8vaw&source=hi&target=en&q=" + hindiWord);

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseGet = await client.GetAsync(uri);
                    string response = await responseGet.Content.ReadAsStringAsync();
                    GoogleTranslationResponse res = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleTranslationResponse>(response);

                    return res.data.translations[0].translatedText;
                }
            }
            else
            {
                Debug.WriteLine("Attempt to get Google Translation failed. Argument was null or empty.");
                return null;
            }
        }



    }
}
