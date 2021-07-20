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
        double duration;
        double averageSpeed = 15;
        int caloriesBurned = 700;
        string dayTime = "14:56";
        



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
           // Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            duration = stopwatch.Elapsed.TotalSeconds;

          //  string elapsedTime = String.Format("{0:00}:{1:00}.{3:00}");
         //    ts.Minutes, ts.Seconds, ts.

        


            // transferiere die daten in die DB 

            SqlCommand insertCommand = new SqlCommand("insert into UserDistances(Distance, Duration, AverageSpeed,Daytime, CaloriesBurned) values(@distance,@duration,@averageSpeed,@dayTime,@caloriesBurned)");

            /*This Part is to make the Data private*/
            insertCommand.Parameters.AddWithValue("@distance", distance);
            insertCommand.Parameters.AddWithValue("@duration", duration);
            insertCommand.Parameters.AddWithValue("@averageSpeed", averageSpeed);
            insertCommand.Parameters.AddWithValue("@dayTime", dayTime);
            insertCommand.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
            int row = objDBAccess.ExecuteQuery(insertCommand);
        }
        }

    }

 
    