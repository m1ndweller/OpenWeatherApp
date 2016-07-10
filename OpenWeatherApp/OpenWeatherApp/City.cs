using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using Newtonsoft.Json;

namespace OpenWeatherApp
{
    public class City : ObservableObject
    {
        string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        string _Country;
        public string Country
        {
            get { return _Country; }
            set { SetProperty(ref _Country, value); }
        }


        [JsonIgnore]
        public string Coordinates => ToString();

        public override string ToString() => $"{Latitude}, {Longitude}";

        /*string _Coordinates;
        public string Coordinates
        {
            get { return _CityCoordinates; }
            set { SetProperty(ref _Coordinates, value); }
        }*/

        string _Longitude;
        public string Longitude
        {
            get { return _Longitude; }
            set
            {
                SetProperty(ref _Longitude, value);
                OnPropertyChanged(nameof(Coordinates));
            }
        }

        string _Latitude;
        public string Latitude
        {
            get { return _Latitude; }
            set
            {
                SetProperty(ref _Latitude, value);
                OnPropertyChanged(nameof(Coordinates));
            }
        }

        string _WeatherIconUrl;
        public string WeatherIconUrl
        {
            get { return _WeatherIconUrl; }
            set { SetProperty(ref _WeatherIconUrl, value); }
        }

        string _WeatherTempKelvin;
        public string WeatherTempKelvin
        {
            get { return _WeatherTempKelvin; }
            set { SetProperty(ref _WeatherTempKelvin, value); }
        }

        string _WeatherTempCelsius;
        public string WeatherTempCelsius
        {
            get { return _WeatherTempCelsius; }
            set { SetProperty(ref _WeatherTempCelsius, value); }
        }

        string _WeatherMain;
        public string WeatherMain
        {
            get { return _WeatherMain; }
            set { SetProperty(ref _WeatherMain, value); }
        }

        string _WeatherWind;
        public string WeatherWind
        {
            get { return _WeatherWind; }
            set { SetProperty(ref _WeatherWind, value); }
        }

        string _WeatherCloudiness;
        public string WeatherCloudiness
        {
            get { return _WeatherCloudiness; }
            set { SetProperty(ref _WeatherCloudiness, value); }
        }

        string _WeatherPressure;
        public string WeatherPressure
        {
            get { return _WeatherPressure; }
            set { SetProperty(ref _WeatherPressure, value); }
        }

        string _WeatherHumidity;
        public string WeatherHumidity
        {
            get { return _WeatherHumidity; }
            set { SetProperty(ref _WeatherHumidity, value); }
        }

        string _WeatherSunrise;
        public string WeatherSunrise
        {
            get { return _WeatherSunrise; }
            set { SetProperty(ref _WeatherSunrise, value); }
        }

        string _WeatherSunset;
        public string WeatherSunset
        {
            get { return _WeatherSunset; }
            set { SetProperty(ref _WeatherSunset, value); }
        }

    }
}
