using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using OpenWeatherApp.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public City FillCityDataFromJson(string json_string)
        {
            City c = new City();
            
            dynamic js = JObject.Parse(json_string);
            c.Name = js.name;
            c.Country = js.sys.country;
            c.Latitude = js.coord.lat;
            c.Longitude = js.coord.lon;            
            c.WeatherIconUrl = /*"Resources/" +*/ "r" + js.weather[0].icon + ".png";
            c.WeatherTempKelvin = js.main.temp;            
            c.WeatherTempCelsius = (js.main.temp - 273.15).ToString().Replace(",", ".") + "\u00B0 C";
            c.WeatherMain = js.weather[0].main;
            c.WeatherWind = js.wind.speed + " m/s";
            c.WeatherCloudiness = js.weather[0].description;
            c.WeatherPressure = js.main.pressure + " hpa";
            c.WeatherHumidity = js.main.humidity + "%";
            double sunrise = js.sys.sunrise;
            double sunset = js.sys.sunset;
            c.WeatherSunrise = UnixTimeStampToDateTime(sunrise).TimeOfDay.ToString();
            c.WeatherSunset = UnixTimeStampToDateTime(sunset).TimeOfDay.ToString();
            
            return c;
            
        }

        public async Task<City> GetWeather(string city_name)
        {
            string json_data;
            City c = new City();
            json_data = await RefreshDataAsync(city_name);
            c = FillCityDataFromJson(json_data);
            return c; 
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
