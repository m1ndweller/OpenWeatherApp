using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

using Newtonsoft.Json;
using System.Runtime.Serialization.Json;


namespace OpenWeatherApp
{
    public class JsonUtility
    {
        HttpClient client;

        public JsonUtility()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        private static string CreateRequest(string queryString)
        {
            string UrlRequest = "http://api.openweathermap.org/data/2.5/weather?q=" +
                                 queryString +
                                 "&APPID=" +
                                 Constants.OWMApiKey;
            return (UrlRequest);
        }

        public string CheckRequest(string query)
        {
            return CreateRequest(query);
        }

        public async Task<string> RefreshDataAsync(string query)
        {
            string content="no data";
            try
            {
                var uri = new Uri(CreateRequest(query));
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return content;
        }
       
    }
}
