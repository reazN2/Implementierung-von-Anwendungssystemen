using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Implementierung_von_Anwendungssystemen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualDistance : ContentPage
    {
        DBAccess objDBAccess = new DBAccess();
        string name;
        public void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var name = MainPicker.Items[MainPicker.SelectedIndex];

        }

        public ManualDistance()
        {
            InitializeComponent();
            MainPicker.Items.Add("Running");
            MainPicker.Items.Add("Swimming");
            MainPicker.Items.Add("Cycling");
            timePicker.Time = DateTime.Now.TimeOfDay;
        }

        private void AddDistance_Clicked(object sender, EventArgs e)
        {
            //type of variables get defined
            float manualDistance;
            string manualTypeOfSport;
            string manualDayTime;
            double manualDuration;
            float manualAverageSpeed;
            int manualCaloriesBurned;
            
            /* value 0-3 are only here to check if the other variables can be parsed */
            float value0;
            float value1;
            float value2;
            int value3;

            //Entries of the user are assigned to a variable
            string stringManualDistance = EntryManualDistance.Text;
            string stringManualDuration = EntryManualDuration.Text;
            string stringManualAverageSpeed = EntryManualAverageSpeed.Text;
            string stringManualCaloriesBurned = EntryManualCaloriesBurned.Text;

            // time of the day get assigned to a variable with the correct format
            manualDayTime = timePicker.Time.ToString(@"hh\:mm");

            //Checks if the user selected a sport

            if (MainPicker.SelectedIndex < 0)
            {
                manualTypeOfSport = "";

            }
            else
            {
                manualTypeOfSport = MainPicker.Items[MainPicker.SelectedIndex];
            }

            // If statements are here to check if the user added all the necessary data and if they are in the correct format

            if (string.IsNullOrEmpty(stringManualDistance))
            {
                DisplayAlert("No distance", "You have to add a distance in [Km]", "OK");
            } 
            else if (string.IsNullOrEmpty(stringManualDuration))
            {
                DisplayAlert("No duration", "You have to add a duration in [HH:mm:ss]", "OK");
            }
            else if (string.IsNullOrEmpty(stringManualAverageSpeed))
            {
                DisplayAlert("No averagespeed", "You have to add an average speed in [Km/h]", "OK");
            }
            else if (string.IsNullOrEmpty(stringManualCaloriesBurned))
            {
                DisplayAlert("No calories burned", "You have to add the burned calories in [Kcal]", "OK");
            }
            else if (string.IsNullOrEmpty(manualTypeOfSport))
            {
                DisplayAlert("No Type of Sport", "You have to select one of the given Opportunities", "OK");
            }
            else if (string.IsNullOrEmpty(manualDayTime))
            {
                DisplayAlert("No DayTime", "You have to add the time in the following format [hh:mm]", "OK");
            }

            else if (!float.TryParse(stringManualDistance, out value0))
            {
                DisplayAlert("Error", "Please insert the distance only as a number in [Km]", "OK");
            }
            else if (!float.TryParse(stringManualAverageSpeed, out value2))
                {
                DisplayAlert("Error", "Please insert the the average speed only as a number in [Km/h]", "OK");
            }
            else if (!int.TryParse(stringManualCaloriesBurned, out value3))
            {
                DisplayAlert("Error", "Please insert the the calories burned only in [Kcal]", "OK");
            }

            else
            {
                //if all the data are in the correct format the rest of the variables get the right data with the right format
                manualDistance = float.Parse(stringManualDistance);
                manualAverageSpeed = float.Parse(stringManualAverageSpeed);
                manualCaloriesBurned = int.Parse(stringManualCaloriesBurned);

                // assignes the table columns to the right values
                SqlCommand insertCommand = new SqlCommand("insert into UserDistances(Distance,TypeOfSport,DayTime,Duration,AverageSpeed,CaloriesBurned,Id) values(@manualDistance, @manualTypeOfSport, @manualDayTime, @manualDuration, @manualAverageSpeed, @manualCaloriesBurned, @Ide)");

                // This part inserts the data into the database
                insertCommand.Parameters.AddWithValue("@manualDistance", manualDistance);
                insertCommand.Parameters.AddWithValue("@manualTypeOfSport", manualTypeOfSport);
                insertCommand.Parameters.AddWithValue("@manualDayTime", manualDayTime);
                insertCommand.Parameters.AddWithValue("@manualDuration", stringManualDuration);
                insertCommand.Parameters.AddWithValue("@manualAverageSpeed", manualAverageSpeed);
                insertCommand.Parameters.AddWithValue("@manualCaloriesBurned", manualCaloriesBurned);
                insertCommand.Parameters.AddWithValue("@Ide", LoginPage.newID);


                int row = objDBAccess.ExecuteQuery(insertCommand);
                if (row == 1)
                { 
                    DisplayAlert("Activity added!", "You successfully added an activity", "Continue");
                    //resets all the values that are displayed
                    EntryManualAverageSpeed.Text = null;
                    EntryManualDistance.Text = null;
                    EntryManualDuration.Text = null;
                    EntryManualCaloriesBurned.Text = null;
                    MainPicker.Items[MainPicker.SelectedIndex] = null;
                }
                else
                {
                    DisplayAlert("Error", "Activity could not be added", "OK");
                }

            }


        }


    }
}