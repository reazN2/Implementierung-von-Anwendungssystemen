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
        string dayTime;
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
            timePicker1.Time = DateTime.Now.TimeOfDay;
            dayTime = timePicker1.Time.ToString(@"hh\:mm");
        }
        private void ActivityPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var name = ActivityPicker.Items[ActivityPicker.SelectedIndex];

        }




        
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        // Reset button clicked
        private void btnReset_Clicked(object sender, EventArgs e)
        {
            lblStopwatch.Text = "00:00";
            btnCalculate.Text = "Start Activity"; // Reset the stopwatch displayed text and also change the startbutton to "Start Activity"


            // Once the button is clicked, calculate average speed and also calories burned. 
            var ts = stopwatch.Elapsed;
            duration1 = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            duration = stopwatch.Elapsed.TotalSeconds;
            averageSpeed = distance * 3600 / duration;
            averageSpeed1.Text = averageSpeed.ToString("#.#" + "km/h");
            caloriesBurned = 20 * duration / 60;
            caloriesBurned1.Text = caloriesBurned.ToString("####" + "kcal");

            // inserting the specific data, e.g. distance/ duration / averagespeed etc. into the SQL database
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

            // reset the stopwatch, after inserting the specific data into sql, reset the parameters of distance,average speed and calories burned.
            stopwatch.Reset();
            distance = 0;
            averageSpeed = 0;
            caloriesBurned = 0;


            stop1 = false;






        }
        // start the gps and time tracking
        private async void BtnCalc_Clicked(object sender, EventArgs e)
        {
            if (ActivityPicker.SelectedIndex < 0)  // if the Activitypicker is not chosen, display Error "no activity" and also the button wont work.
            {
                typeOfSport = "";

            }
            else
            {
                typeOfSport = ActivityPicker.Items[ActivityPicker.SelectedIndex];
            }
            if (typeOfSport == "")

            { DisplayAlert("No Activity", "Please select an Activity", "OK");}
            else 
            {
                averageSpeed1.Text = "0";
                caloriesBurned1.Text = "0";
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

                // stop1 bool is false and dis1 is true. start stopwatch and also track the elapsed time
                stop1 = false;
                dis1 = true;
                stopwatch.Start();
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    var ts = stopwatch.Elapsed;
                    lblStopwatch.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    return true;


                });

                // name 2 different vars to check the start and current location
                var StartLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(3)));

                var CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(3)));

                // as long as dis1 = true, check the currentlocation and calculate 
                while (dis1 == true)
                {


                    var gerade = CurrentLocation;

                    CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(2)));
                    distance += Location.CalculateDistance(gerade, CurrentLocation, DistanceUnits.Kilometers);
                    stringDistance.Text = distance.ToString("0.00" + "km");



                    




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
             duration = stopwatch.Elapsed.TotalMinutes + stopwatch.Elapsed.TotalSeconds;




        }

    }

    }
           
 
    