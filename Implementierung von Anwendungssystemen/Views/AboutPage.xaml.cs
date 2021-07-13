using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Implementierung_von_Anwendungssystemen.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(3)));

            resultLocation.Text = $"la-t:{result.Latitude}, lng: {result.Longitude}";

        }
        async void BtnLocation_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var source = txtSource.Text;
                var sourceLocation = await Geocoding.GetLocationsAsync(source);
                var destination = txtDestination.Text;
                var destinationLocation = await Geocoding.GetLocationsAsync(destination);
                if (sourceLocation != null)
                {
                    var sourceLocations = sourceLocation?.FirstOrDefault();
                    var destinationLocations = destinationLocation?.FirstOrDefault();
                    Location sourceCoordinates = new Location(sourceLocations.Latitude, sourceLocations.Longitude);
                    Location destinationCoordinates = new Location(destinationLocations.Latitude, destinationLocations.Longitude);
                    double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                    lblDistance.Text = distance.ToString();
                }



            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }

        }
    }
}