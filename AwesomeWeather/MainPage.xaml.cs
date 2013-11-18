using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using Newtonsoft.Json;
using System.Dynamic;
using System.Diagnostics;

namespace AwesomeWeather
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("Getting GPS...");

            var gps = new Windows.Devices.Geolocation.Geolocator();
            var position = await gps.GetGeopositionAsync();
            Debug.WriteLine("Got GPS Location!");
            var apiKey = "ec8eb1f649db89338ea7598e8d6d31ea";
            var latitude = position.Coordinate.Latitude;
            var longitude = position.Coordinate.Longitude;
            Debug.WriteLine("Asking Forecast.io for data...");
            var client = new WebClient();
            client.DownloadStringAsync(new Uri(string.Format("https://api.forecast.io/forecast/{0}/{1},{2}", apiKey, latitude, longitude)));
            client.DownloadStringCompleted += client_DownloadStringCompleted;
        }

        private async void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Debug.WriteLine("Got Data!");
            Debug.WriteLine("Deserializing the data...");
            DataContext = await JsonConvert.DeserializeObjectAsync<ForecastIO.ForecastIOResponse>(e.Result);
            Debug.WriteLine("Done!");
        }
    }
}