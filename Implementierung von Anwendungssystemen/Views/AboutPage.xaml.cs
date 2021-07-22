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
        string duration1;
        double duration;
        double averageSpeed;
        double caloriesBurned;
        string dayTime = "14:56";
        string typeOfSport;
       





        public AboutPage()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();

            lblStopwatch.Text = "00:00";
            stringDistance.Text = "0km";
            averageSpeed1.Text = "0";
            caloriesBurned1.Text = "0";
            
            ActivityPicker.Items.Add("Running");
            ActivityPicker.Items.Add("Swimming");
            ActivityPicker.Items.Add("Cycling");
        }
        private void ActivityPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var name = ActivityPicker.Items[ActivityPicker.SelectedIndex];

        }




        
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }


        private void btnReset_Clicked(object sender, EventArgs e)
        {
            lblStopwatch.Text = "00:00";
            btnCalculate.Text = "Start Activity";
            // btnStart.Text = "Start";

            //duration = stopwatch.Elapsed.TotalMinutes + stopwatch.Elapsed.TotalSeconds;
            var ts = stopwatch.Elapsed;
            duration1 = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            duration = stopwatch.Elapsed.TotalSeconds;
            // averageSpeed = distance / duration * 1000;
            averageSpeed = distance * 3600 / duration;
            averageSpeed1.Text = averageSpeed.ToString("#.#" + "km/h");
            caloriesBurned = 10 * duration / 60;
            caloriesBurned1.Text = caloriesBurned.ToString("####" + "kcal");


            SqlCommand insertCommand = new SqlCommand("insert into UserDistances(Distance, Duration, AverageSpeed,Daytime, CaloriesBurned,Id, TypeOfSport) values(@distance,@duration1,@averageSpeed,@dayTime,@caloriesBurned,@Ide,@typeOfSport)");

            //This Part is to make the Data private//
            insertCommand.Parameters.AddWithValue("@distance", distance);
            insertCommand.Parameters.AddWithValue("@duration1", duration1);
            insertCommand.Parameters.AddWithValue("@averageSpeed", averageSpeed);
            insertCommand.Parameters.AddWithValue("@dayTime", dayTime);
            insertCommand.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
            insertCommand.Parameters.AddWithValue("@typeOfSport", typeOfSport);
            insertCommand.Parameters.AddWithValue("@Ide", LoginPage.newID);
            int row = objDBAccess.ExecuteQuery(insertCommand);

            /*if (duration > stopwatch.Elapsed.Minutes
            {
                caloriesBurned = 1 * 120;
                    }; */


            //  averageSpeed1.Text = averageSpeed.ToString("#.#" + "km/h");
            stopwatch.Reset();
            distance = 0;
            averageSpeed = 0;
            caloriesBurned = 0;

            stop1 = false;


            // transferiere die daten in die DB 



            // averageSpeed = distance / 60;
            //averageSpeed1.Text = averageSpeed.ToString("0.###" + "km/h");



        }

        private async void BtnCalc_Clicked(object sender, EventArgs e)
        {
            if (ActivityPicker.SelectedIndex < 0)
            {
                typeOfSport = "";

            }
            else
            {
                typeOfSport = ActivityPicker.Items[ActivityPicker.SelectedIndex];
            }
            if (typeOfSport == "")

            { DisplayAlert("No Activity", "Please select an Activity", "OK");}
            else { 
                string run = ActivityPicker.Items[ActivityPicker.SelectedIndex];
                if ("Running" == run)
                { ImageRun.IsVisible = true;
                    ImageSwim.IsVisible = false;
                    ImageCycle.IsVisible = false;
                }
                else if ("Swimming" == run)
                {
                    ImageRun.IsVisible = false;
                    ImageSwim.IsVisible = true;
                    ImageCycle.IsVisible = false;
                }
                else if ("Cycling" == run)
                {
                    ImageRun.IsVisible = false;
                    ImageSwim.IsVisible = false;
                    ImageCycle.IsVisible = true;
                }


                stop1 = false;
                dis1 = true;
                stopwatch.Start();
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    var ts = stopwatch.Elapsed;
                    lblStopwatch.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    return true;


                });
                var StartLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(3)));

                var CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(3)));

                while (dis1 == true)
                {


                    var gerade = CurrentLocation;

                    CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(2)));
                    distance += Location.CalculateDistance(gerade, CurrentLocation, DistanceUnits.Kilometers);
                    stringDistance.Text = distance.ToString("0.00" + "km");



                    //Console.WriteLine(distance.ToString());




                    if (stop1 == true)
                    {

                        btnCalculate.Text = "Resume";
                        stopwatch.Stop();
                        dis1 = false;



                    }




                }
            }

        }

        private void BtnStop_Clicked(object sender, EventArgs e)
        {
            stop1 = true;
            stopwatch.Stop();
            // Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            duration = stopwatch.Elapsed.TotalMinutes + stopwatch.Elapsed.TotalSeconds;




        }

    }

    }
           
 
    