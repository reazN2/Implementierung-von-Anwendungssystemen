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

        public ManualDistance()
        {
            InitializeComponent();
            MainPicker.Items.Add("Running");
            MainPicker.Items.Add("Cycling");

        }

        private void AddDistance_Clicked(object sender, EventArgs e)
        {
            float manualDistance;
            string manualTypeOfSport = EntryManualTypeOfSport.Text;
            string manualDayTime = EntryManualTimeOfTheDay.Text;
            double manualDuration;
            float manualAverageSpeed;
            int manualCaloriesBurned;

            float value0;
            string stringManualDistance = EntryManualDistance.Text;
            if (float.TryParse(stringManualDistance, out value0))
            {
                manualDistance = float.Parse(stringManualDistance);
            }
            else
            {
                DisplayAlert("Error", "Please insert the distance only as numbers", "OK");
            }
            double value1;
            string stringManualDuration = EntryManualDuration.Text;
            if (double.TryParse(stringManualDuration, out value1))
            {
                manualDuration = double.Parse(stringManualDuration);
            }
            else
            {
                DisplayAlert("Error", "Please insert the duration only as numbers", "OK");
            }

            float value2;
            string stringManualAverageSpeed = EntryManualAverageSpeed.Text;
            if (float.TryParse(stringManualDuration, out value2))
            {
                manualAverageSpeed = float.Parse(stringManualDuration);
            }
            else
            {
                DisplayAlert("Error", "Please insert the the average speed only as numbers", "OK");
            }

            int value3;
            string stringManualCaloriesBurned = EntryManualCaloriesBurned.Text;
            if (int.TryParse(stringManualCaloriesBurned, out value3))
            {
                manualCaloriesBurned = int.Parse(stringManualDuration);
            }
            else
            {
                DisplayAlert("Error", "Please insert the the calories burned only as numbers", "OK");
            }


        }

        private void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = MainPicker.Items[MainPicker.SelectedIndex];
            DisplayAlert(name, "Selected Sport", "OK");
        }
    }
}