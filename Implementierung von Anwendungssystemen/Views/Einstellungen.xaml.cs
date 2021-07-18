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
    public partial class Einstellungen : ContentPage
    {
        DBAccess objDBAccess = new DBAccess();
        public Einstellungen()
        {
            InitializeComponent();


            EntryUserName.Text = LoginPage.name;
            EntryUserEmail.Text = LoginPage.email;
            EntryUserPassword.Text = LoginPage.password;
            EntryUserUniversity.Text = LoginPage.university;
        }
        

        private void UpdateAccountInfo(object sender, EventArgs e)
        {
            string newUserName = EntryUserName.Text;
            string newUserEmail = EntryUserEmail.Text;
            string newUserPassword = EntryUserPassword.Text;
            string newUserUniversity = EntryUserUniversity.Text;

           if (string.IsNullOrEmpty(newUserName))
            {
                DisplayAlert("No Name", "Please insert a Name", "OK");
            }
            else if (string.IsNullOrEmpty(newUserEmail))
            {
                DisplayAlert("No E-Mail", "Please insert a E-Mail", "OK");
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
                string query = "Update Users SET Name = '" + @newUserName + "', Email = '" + @newUserEmail + "', Password = '" + @newUserPassword + "', University = '" + @newUserUniversity + "' where ID = '" + LoginPage.id + "'";
                SqlCommand updateCommand = new SqlCommand(query);

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

        private async void DeleteAccount(object sender, EventArgs e)
        {
            bool answer =  await DisplayAlert("Delete?", "Do you really want to delete your account?", "Delete", "Cancel");
            if (answer)
            {
                string query = "DELETE from Users Where ID  = '" + LoginPage.id + "'";

                SqlCommand deleteCommand = new SqlCommand(query);

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