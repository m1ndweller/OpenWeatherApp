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
            SetStartPosition();
            await Task.Delay(100);
        }

        void SetStartPosition()
        {
            CityMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(52.6, 39.57), Distance.FromKilometers(5)));
        }

        void SearchBar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                ViewModel.City.SearchName = CitySearchBar.Text;
            }
        }

        /*void Image_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Source")
            {
                Weather_Icon.Source = ImageSource.FromFile(ViewModel.City.WeatherIconUrl);
            }
        }*/

        void Coordinates_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                if (!string.IsNullOrWhiteSpace(ViewModel.City.Latitude)&& !string.IsNullOrWhiteSpace(ViewModel.City.Longitude))
                {
                    Position cityPosition = new Position(Convert.ToDouble(ViewModel.City.Latitude, CultureInfo.InvariantCulture),
                        Convert.ToDouble(ViewModel.City.Longitude, CultureInfo.InvariantCulture));
                    CityMap.MoveToRegion(MapSpan.FromCenterAndRadius(cityPosition, Distance.FromKilometers(10)));
                }             
            }
        }       
    }
}
