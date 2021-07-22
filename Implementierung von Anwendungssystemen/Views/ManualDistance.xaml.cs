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
        }

        private void AddDistance_Clicked(object sender, EventArgs e)
        {
            float manualDistance;
            string manualTypeOfSport;
            string manualDayTime;
            double manualDuration;
            float manualAverageSpeed;
            int manualCaloriesBurned;

            float value0;
            string stringManualDistance = EntryManualDistance.Text;

            float value1;
            string stringManualDuration = EntryManualDuration.Text;

            float value2;
            string stringManualAverageSpeed = EntryManualAverageSpeed.Text;

            int value3;
            string stringManualCaloriesBurned = EntryManualCaloriesBurned.Text;
            manualTypeOfSport = MainPicker.Items[MainPicker.SelectedIndex];
            manualDayTime = EntryManualTimeOfTheDay.Text;

            if (string.IsNullOrEmpty(stringManualDistance))
            {
                DisplayAlert("No distance", "You have to add a distance in [Km]", "OK");
            } 
            else if (string.IsNullOrEmpty(stringManualDuration))
            {
                DisplayAlert("No duration", "You have to add a duration in [min]", "OK");
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
            else if (!float.TryParse(stringManualDuration, out value1))
            {
                DisplayAlert("Error", "Please insert the duration only as a number in [min]", "OK");
            }
            else if (!float.TryParse(stringManualAverageSpeed, out value2))
                {
                DisplayAlert("Error", "Please insert the the average speed only as a number in [Km/h]", "OK");
            }
            else if (!int.TryParse(stringManualCaloriesBurned, out value3))
            {
                DisplayAlert("Error", "Please insert the the calories burned only in [Kcal]", "OK");
            }
            else if (!int.TryParse(manualDayTime, out value3))
            {
                DisplayAlert("Error", "Please insert the the time in the following format [hh:mm] ", "OK");
            }
            else
            {
                manualDistance = float.Parse(stringManualDistance);
                manualDuration = float.Parse(stringManualDuration);
                manualAverageSpeed = float.Parse(stringManualAverageSpeed);
                manualCaloriesBurned = int.Parse(stringManualCaloriesBurned);


                SqlCommand insertCommand = new SqlCommand("insert into UserDistances(Distance,TypeOfSport,DayTime,Duration,AverageSpeed,CaloriesBurned,Id) values(@manualDistance, @manualTypeOfSport, @manualDayTime, @manualDuration, @manualAverageSpeed, @manualCaloriesBurned, @Ide)");

                /*This Part is to make the Data private*/
                insertCommand.Parameters.AddWithValue("@manualDistance", manualDistance);
                insertCommand.Parameters.AddWithValue("@manualTypeOfSport", manualTypeOfSport);
                insertCommand.Parameters.AddWithValue("@manualDayTime", manualDayTime);
                insertCommand.Parameters.AddWithValue("@manualDuration", manualDuration);
                insertCommand.Parameters.AddWithValue("@manualAverageSpeed", manualAverageSpeed);
                insertCommand.Parameters.AddWithValue("@manualCaloriesBurned", manualCaloriesBurned);
                insertCommand.Parameters.AddWithValue("@Ide", LoginPage.newID);


                int row = objDBAccess.ExecuteQuery(insertCommand);
                if (row == 1)
                {
                    DisplayAlert("Activity added!", "You successfully added an activity", "Continue");
                }
                else
                {
                    DisplayAlert("Error", "Activity could not be added", "OK");
                }

            }


        }


    }
}