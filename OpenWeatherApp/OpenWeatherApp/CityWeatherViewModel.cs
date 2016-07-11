using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using FormsToolkit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using OpenWeatherApp.Data;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;

namespace OpenWeatherApp
{
    public class CityWeatherViewModel : BaseNavigationViewModel
    {
        public CityWeatherViewModel(City city)
        {

            _Geocoder = new Geocoder();

            City = city;


        }

        public City City { private set; get; }

        public bool HasLocation => !string.IsNullOrWhiteSpace(City?.Coordinates);

        readonly Geocoder _Geocoder;

        Command _SearchCityCommand;

        public Command SearchCityCommand
        {
            get
            {
                return _SearchCityCommand ??
                    (_SearchCityCommand = new Command(async () => await ExecuteSearchCityCommand()));
            }
        }

        async Task ExecuteSearchCityCommand()
        {
            await GetWeatherData();
        }

        public void SetupMap()
        {
            if (HasLocation)
            {
                MessagingService.Current.SendMessage(MessageKeys.SetupMap);
            }
        }
                
        public async Task<Position> GetWeatherData()
        {
            JsonUtility jsu = new JsonUtility();
            //this.City.Country = jsu.CheckRequest("Lipetsk");
            this.City.Name = this.City.SearchName;
            //string s = await jsu.RefreshDataAsync(this.City.SearchName);
            //this.City.WeatherMain = s;
            //await Task.Delay(1000);
            try
            {
                City c = await jsu.GetWeather(this.City.SearchName);
                this.City.copy(c);
                c = null;
            } catch (Exception e)
            {
                City c = new City();
                this.City.copy(c);
                this.City.Name = "Not found";
                c = null;
            }
            return new Position(0, 0);
        }        
    }
}
