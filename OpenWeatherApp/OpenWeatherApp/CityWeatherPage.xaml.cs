using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.ComponentModel;

namespace OpenWeatherApp
{
    public partial class CityWeatherPage : ContentPage
    {
        protected CityWeatherViewModel ViewModel => BindingContext as CityWeatherViewModel;

        public CityWeatherPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Typically, is preferable to call into the viewmodel for OnAppearing() logic to be performed,
            // but we're not doing that in this case because we need to interact with the Xamarin.Forms.Map property on this Page.
            // In the future, the Map type and it's properties may get more binding support, so that the map setup can be omitted from code-behind.
            //await SetMapStartPosition();
            
            SetPosition();
            await Task.Delay(100);
        }

        void SetPosition()
        {
            CityMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(52.6, 39.57), Distance.FromKilometers(5)));
            /*CityMap.Pins.Add(new Pin()
            {
                Position = new Position(52.6, 39.57),
                Label = "Lipetsk"
            });*/
        }

        void SearchBar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                ViewModel.City.SearchName = CitySearchBar.Text;
            }
        }

        void Coordinates_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                Position cityPosition = new Position(Convert.ToDouble(ViewModel.City.Latitude, CultureInfo.InvariantCulture),
                    Convert.ToDouble(ViewModel.City.Longitude, CultureInfo.InvariantCulture));
                CityMap.MoveToRegion(MapSpan.FromCenterAndRadius(cityPosition, Distance.FromKilometers(5)));
                CityMap.Pins.Add(new Pin()
                {
                    Position = cityPosition,
                    Label = "City"
                });
            }
        }
        /*
        async Task SetMapStartPosition()
        {
            if (ViewModel.HasLocation)
            {
                CityMap.IsVisible = false;

                // set to a default position
                Position position;

                try
                {
                    //position = await ViewModel.GetPosition();
                    position = new Position(52.6, 39.57);
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    ViewModel.DisplayGeocodingError();

                    return;
                }

                // if lat and lon are both 0, then it's assumed that position acquisition failed
                if (position.Latitude == 0 && position.Longitude == 0)
                {
                    ViewModel.DisplayGeocodingError();

                    return;
                }
                var pin = new Pin()
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = ViewModel.City.Name                    
                };

                CityMap.Pins.Clear();

                CityMap.Pins.Add(pin);

                CityMap.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromKilometers(5)));

                CityMap.IsVisible = true;
            }
        }*/
    }
}
