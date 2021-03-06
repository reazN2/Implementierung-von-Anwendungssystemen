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
    public partial class Einstellungen : ContentPage
    {
        DBAccess objDBAccess = new DBAccess();
        string name;
        public void PickerUniversitySettings_SelectedIndexChanged(object sender, EventArgs e)
        { 
            name = PickerUniversitySettings.Items[PickerUniversitySettings.SelectedIndex];
        }
        public Einstellungen()
        {
            InitializeComponent();
            // Sets Picker values
            PickerUniversitySettings.Items.Add("Siegen");
            PickerUniversitySettings.Items.Add("Crete");
            PickerUniversitySettings.Items.Add("Maribor");
            PickerUniversitySettings.Items.Add("Orleans");
            PickerUniversitySettings.Items.Add("Porto");
            PickerUniversitySettings.Items.Add("Rome");
            PickerUniversitySettings.Items.Add("Vilnius");

            //catches the values of the Person that is logged in and helps to display them
            EntryUserName.Text = LoginPage.name;
            EntryUserEmail.Text = LoginPage.email;
            EntryUserPassword.Text = LoginPage.password;


        }
        

        private void UpdateAccountInfo(object sender, EventArgs e)
        {
            //defines strings that are used for updating the account
            string newUserName = EntryUserName.Text;
            string newUserEmail; 
            string newUserPassword = EntryUserPassword.Text;
            string newUserUniversity;


            //checks if University is selected
            if (PickerUniversitySettings.SelectedIndex < 0)
            {
                newUserUniversity = "";
            }
            else
            {
                newUserUniversity = PickerUniversitySettings.Items[PickerUniversitySettings.SelectedIndex];
            }

            //next few lines check if the E-Mail has the correct format and is only from participating universities
            var email = EntryUserEmail.Text;
            var emailPattern =
                (@"^[a-zA-Z0-9._%+-]+(@student.uni-siegen.de|@unicusano.it|@unicusano.com|@student.um.si|@um.si|@hmu.gr|@vgtu.lt|@stud.vgtu.lt|@vilniustech.lt|@ipp.pt|@etu.univ-orleans.fr)$");
            if (Regex.IsMatch(email, emailPattern))
            {
                newUserEmail = EntryUserEmail.Text;
            }
            else
            {
                newUserEmail = "";
            }
            //checks if all the entries are not empty
            if (string.IsNullOrEmpty(newUserName))
            {
                DisplayAlert("No Name", "Please insert a Name", "OK");
            }
            else if (string.IsNullOrEmpty(newUserEmail))
            {
                DisplayAlert("Error", "University mail from participating universities required", "OK");
            }
            else if (string.IsNullOrEmpty(newUserPassword))
            {
                DisplayAlert("No Password", "Please insert a Password", "OK");
            }
             else if (string.IsNullOrEmpty(newUserUniversity))
            {
                DisplayAlert("No University", "Please insert a University", "OK");
            }
            else
            {
                //assignes the new values to the correct cloumns 
                string query = "Update Users SET Name = '" + @newUserName + "', Email = '" + @newUserEmail + "', Password = '" + @newUserPassword + "', University = '" + @newUserUniversity + "' where ID = '" + LoginPage.id + "'";
                SqlCommand updateCommand = new SqlCommand(query);

                // updates the database
                updateCommand.Parameters.AddWithValue("@userName", @newUserName);
                updateCommand.Parameters.AddWithValue("@userEmail", @newUserEmail);
                updateCommand.Parameters.AddWithValue("@userPassword", @newUserPassword);
                updateCommand.Parameters.AddWithValue("@userUniversity", @newUserUniversity);

                int row = objDBAccess.ExecuteQuery(updateCommand);
                if (row == 1)
                {
                    DisplayAlert("Success", "Account information updated successfully", "Continue");
                    Shell.Current.GoToAsync("//Einstellungen");
                }
                else
                {
                    DisplayAlert("Error", "Could not update user profile", "OK");
                }
            }

        }
        //this is the method to delete the account
        private async void DeleteAccount(object sender, EventArgs e)
        {
            bool answer =  await DisplayAlert("Delete?", "Do you really want to delete your account?", "Delete", "Cancel");
            if (answer)
            { 
                //defines what should be deleted
                string query = "DELETE from Users Where ID  = '" + LoginPage.id + "'";

                SqlCommand deleteCommand = new SqlCommand(query);

                // deletes the account and navigates to the loginPage
                int row = objDBAccess.ExecuteQuery(deleteCommand);
                if (row == 1)
                {
                    await DisplayAlert("Success", "Your account was deleted", "Continue");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
                else
                {
                    await DisplayAlert ("Error", "Could not delete user profile", "OK");
                }
            }
        }


    }
}