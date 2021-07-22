using Implementierung_von_Anwendungssystemen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using System.Data.SqlClient;

namespace Implementierung_von_Anwendungssystemen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DistanceTables : ContentPage
    {
        DataTable dtUserDistances = new DataTable();
        DBAccess objDBAccess = new DBAccess();


        public static string distanceView, typeOfSportView, dayTimeView, durationView, averageSpeedView, caloriesBurnedView;

        int detailIndex = 0;
        private void PlusButton_Clicked(object sender, EventArgs e)
        {
            if(detailIndex < dtUserDistances.Rows.Count -1)
                detailIndex++;
            refreshDetails();
        }

        private void MinusButton_Clicked(object sender, EventArgs e)
        {
            if (detailIndex > 0)
                detailIndex--;
            refreshDetails();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ManualDistance)}");
        }

        public DistanceTables()
        {
            InitializeComponent();

            //Looks if there is a User that has the same Email and Password as the ones a user just entered in the frontend. Also checks if the user is not deactivated.
            refreshDetails();




        }

        public void refreshDetails()
        {

            string query = "Select * from UserDistances Where Id= '" + LoginPage.newID + "' Order by DistanceId DESC";

            objDBAccess.ReadDatathroughAdapter(query, dtUserDistances);

            if (dtUserDistances.Rows.Count > 0)
            {
                distanceView = dtUserDistances.Rows[detailIndex]["Distance"].ToString();
                userDistanceView.Text = distanceView;
                typeOfSportView = dtUserDistances.Rows[detailIndex]["TypeOfSport"].ToString();
                userTypeOfSportView.Text = typeOfSportView;
                dayTimeView = dtUserDistances.Rows[detailIndex]["DayTime"].ToString();
                userDayTime.Text = dayTimeView;
                durationView = dtUserDistances.Rows[detailIndex]["Duration"].ToString();
                userDurationView.Text = durationView;
                averageSpeedView = dtUserDistances.Rows[detailIndex]["AverageSpeed"].ToString();
                userAverageSpeedView.Text = averageSpeedView;
                caloriesBurnedView = dtUserDistances.Rows[detailIndex]["CaloriesBurned"].ToString();
                usercaloriesBurnedView.Text = caloriesBurnedView;
            }
            else
            {
                DisplayAlert("Error", "Could not log in. Either the email or password are wrong or your account was deactivated", "OK");
            }
        }
    }
}