using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;


namespace Implementierung_von_Anwendungssystemen.Views
{
    public partial class AboutPage : ContentPage
    {
        DBAccess objDBAccess = new DBAccess();


        Stopwatch stopwatch;
        private bool stop1;
        private bool dis1;
        double distance;
        int duration = 20;
        double averageSpeed = 15;
        int caloriesBurned = 700;
        string dayTime = "14:56";
        //int Ide = LoginPage.newID;



        public AboutPage()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();

            lblStopwatch.Text = "00:00";
            stringDistance.Text = "0";

        }

        /*private void btnStart_Clicked(object sender, EventArgs e)
        {
            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();

                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    lblStopwatch.Text = stopwatch.Elapsed.ToString();

                    if (!stopwatch.IsRunning)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }

                );
            }



        } */
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }


        private void btnReset_Clicked(object sender, EventArgs e)
        {
            lblStopwatch.Text = "00:00";
            // btnStart.Text = "Start";
            stopwatch.Reset();
            distance = 0;
            stop1 = false;

         
        }

        private async void BtnCalc_Clicked(object sender, EventArgs e)
        {
            stop1 = false;
            dis1 = true;
            stopwatch.Start();
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                lblStopwatch.Text = stopwatch.Elapsed.ToString();
                return true;

            });
            var StartLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(3)));

            var CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(3)));

            while  (dis1 == true) {


                var gerade = CurrentLocation;

                CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(2)));
                distance += Location.CalculateDistance(gerade, CurrentLocation, DistanceUnits.Kilometers);
                stringDistance.Text = distance.ToString("0.####"+"km") ;
                

                //Console.WriteLine(distance.ToString());




                if (stop1 == true)
                {

                    btnCalculate.Text = "Resume";
                    stopwatch.Stop();
                    dis1 = false;


                }

            }


        }

            private void BtnStop_Clicked(object sender, EventArgs e)
            {
                stop1 = true;
            stopwatch.Stop();

            SqlCommand insertCommand = new SqlCommand("insert into UserDistances(Distance, Duration, AverageSpeed,Daytime, CaloriesBurned) values(@distance,@duration,@averageSpeed,@dayTime,@caloriesBurned)");

            /*This Part is to make the Data private*/
            insertCommand.Parameters.AddWithValue("@distance", distance);
            insertCommand.Parameters.AddWithValue("@duration", duration);
            insertCommand.Parameters.AddWithValue("@averageSpeed", averageSpeed);
            insertCommand.Parameters.AddWithValue("@dayTime", dayTime);
            insertCommand.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
            //insertCommand.Parameters.AddWithValue("@Ide", Ide);
            int row = objDBAccess.ExecuteQuery(insertCommand);
        }
        }

    }

 
    // ^^ dieser teil für Calculate distance? wie noch umschreiben mit bool?




    /*   async void Button_Clicked(object sender, EventArgs e)
       {
           var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(3)));

           resultLocation.Text = $"la-t:{result.Latitude}, lng: {result.Longitude}";


    }

         private  async void BtnLocation_Clicked(object sender, System.EventArgs e)
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
    */

 


       /* private void Btn_Clicked(object sender, EventArgs e)
        {

       

            const double EARTH_RADIUS = 6371009;

            double ToRadians(double input)
            {
                return input / 180.0 * Math.PI;
            }

            double DistanceRadians(double lat1, double lng1, double lat2, double lng2)
            {
                double Hav(double x)
                {
                    double sinHalf = Math.Sin(x * 0.5);
                    return sinHalf * sinHalf;
                }
                double ArcHav(double x)
                {
                    return 2 * Math.Asin(Math.Sqrt(x));
                }
                double HavDistance(double lat1b, double lat2b, double dLng)
                {
                    return Hav(lat1b - lat2b) + Hav(dLng) * Math.Cos(lat1b) * Math.Cos(lat2b);
                }
                return ArcHav(HavDistance(lat1, lat2, lng1 - lng2));
            }

            double ComputeDistanceBetween(LatLng from, LatLng to)
            {
                double ComputeAngleBetween(LatLng From, LatLng To)
                {
                    return DistanceRadians(ToRadians(from.Latitude), ToRadians(from.Longitude),
                                                  ToRadians(to.Latitude), ToRadians(to.Longitude));
                }
                return ComputeAngleBetween(from, to) * EARTH_RADIUS;
            }

            double ComputeLength(List<LatLng> path)
            {
                if (path.Count < 2)
                    return 0;

                double length = 0;
                LatLng prev = path[0];
                double prevLat = ToRadians(prev.Latitude);
                double prevLng = ToRadians(prev.Longitude);
                foreach (LatLng point in path)
                {
                    double lat = ToRadians(point.Latitude);
                    double lng = ToRadians(point.Longitude);
                    length += DistanceRadians(prevLat, prevLng, lat, lng);
                    prevLat = lat;
                    prevLng = lng;
                }
                return length * EARTH_RADIUS;
            }
        }

    }


    }
    public static class Meters
{
    const double EARTH_RADIUS = 6371009;

    static double ToRadians(double input)
    {
        return input / 180.0 * Math.PI;
    }

    static double DistanceRadians(double lat1, double lng1, double lat2, double lng2)
    {
        double Hav(double x)
        {
            double sinHalf = Math.Sin(x * 0.5);
            return sinHalf * sinHalf;
        }
        double ArcHav(double x)
        {
            return 2 * Math.Asin(Math.Sqrt(x));
        }
        double HavDistance(double lat1b, double lat2b, double dLng)
        {
            return Hav(lat1b - lat2b) + Hav(dLng) * Math.Cos(lat1b) * Math.Cos(lat2b);
        }
        return ArcHav(HavDistance(lat1, lat2, lng1 - lng2));
    }

    public static double ComputeDistanceBetween(LatLng from, LatLng to)
    {
        double ComputeAngleBetween(LatLng From, LatLng To)
        {
            return DistanceRadians(ToRadians(from.Latitude), ToRadians(from.Longitude),
                                          ToRadians(to.Latitude), ToRadians(to.Longitude));
        }
        return ComputeAngleBetween(from, to) * EARTH_RADIUS;
    }

    private static double ToRadians(object longitude)
    {
        throw new NotImplementedException();
    }



    public static double ComputeLength(List<LatLng> path)
    {
        if (path.Count < 2)
            return 0;

        double length = 0;
        LatLng prev = path[0];
        double prevLat = ToRadians(prev.Latitude);
        double prevLng = ToRadians(prev.Longitude);
        foreach (LatLng point in path)
        {
            double lat = ToRadians(point.Latitude);
            double lng = ToRadians(point.Longitude);
            length += DistanceRadians(prevLat, prevLng, lat, lng);
            prevLat = lat;
            prevLng = lng;
        }
        return length * EARTH_RADIUS;
    }
} */
