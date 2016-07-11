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


            //SubscribeToSaveAcquaintanceMessages();
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
            await GetPosition();
            //await PushAsync(new AcquaintanceEditPage() { BindingContext = new AcquaintanceEditViewModel(Acquaintance) });
        }

        public void SetupMap()
        {
            if (HasLocation)
            {
                MessagingService.Current.SendMessage(MessageKeys.SetupMap);
            }
        }

        public void DisplayGeocodingError()
        {
            MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
            {
                Title = "Geocoding Error",
                Message = "Please check network connection.",
                Cancel = "OK"
            });

            IsBusy = false;
        }

        public async Task<Position> GetPosition()
        {
            //this.City.Latitude = "52.6";
            //this.City.Longitude = "39.57";
            //this.City.Name = "Lipetsk";


            /*if (!HasLocation)
                return new Position(0, 0);

            IsBusy = true;

            Position p;

            p = (await _Geocoder.GetPositionsForAddressAsync(Acquaintance.AddressString)).FirstOrDefault();

            // The Android geocoder (the underlying implementation in Android itself) fails with some addresses unless they're rounded to the hundreds.
            // So, this deals with that edge case.
            if (p.Latitude == 0 && p.Longitude == 0 && AddressBeginsWithNumber(Acquaintance.AddressString) && Device.OS == TargetPlatform.Android)
            {
                var roundedAddress = GetAddressWithRoundedStreetNumber(Acquaintance.AddressString);

                p = (await _Geocoder.GetPositionsForAddressAsync(roundedAddress)).FirstOrDefault();
            }

            IsBusy = false;

            return p;*/
            JsonUtility jsu = new JsonUtility();
            //this.City.Country = jsu.CheckRequest("Lipetsk");
            this.City.Name = this.City.SearchName;
            string s = await jsu.RefreshDataAsync(this.City.SearchName);
            this.City.WeatherMain = s;
            //await Task.Delay(1000);
            City c = await jsu.GetWeather(this.City.SearchName);
            this.City.copy(c);
            c = null;

            //double lat, lon;
            //lat = Double.Parse(String.Format(this.City.Latitude));
            //lon =  Double.Parse(String.Format(this.City.Longitude));
            //lat = Convert.ToDouble(this.City.Latitude, CultureInfo.InvariantCulture);
            //lon = Convert.ToDouble(this.City.Longitude, CultureInfo.InvariantCulture);
            //await ExecuteGetDirectionsCommand(lat, lon);
            //this.City.Name = c.Name;
            //this.City = await jsu.GetWeather("Lipetsk");
            //await jsu.GetWeather("Lipetsk");
            return new Position(0, 0);
        }
        /*
        async Task ExecuteGetDirectionsCommand(double lat, double lon)
        {
            var position = new Position(lat, lon);

            var pin = new Pin() { Position = position };

            await CrossExternalMaps.Current.NavigateTo(pin.Label, pin.Position.Latitude, pin.Position.Longitude, NavigationType.Driving);
        }*/
    }
}
