using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsToolkit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeatherApp
{
    public class CityWeatherViewModel : BaseNavigationViewModel
    {
        public CityWeatherViewModel(City city)
        {

            _Geocoder = new Geocoder();

            City = city;

            //--------------------------------------------------
            City.Latitude = "52.6";
            City.Longitude = "39.57";


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
            /*if (!HasAddress)
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
            await Task.Delay(1000);
            return new Position(0, 0);
        }
    }
}
