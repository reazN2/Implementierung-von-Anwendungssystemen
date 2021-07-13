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
    public partial class Registration : ContentPage
    {
        DBAccess objDBAccess = new DBAccess();
        public Registration()
        {
            InitializeComponent();

        }
        void Button_Clicked(object sender, EventArgs e)
        {
            string userName = EntryUserName.Text;
            string userPassword = EntryUserPassword.Text;
            string userUniversity = EntryUserUniversity.Text;
            string userEmail = EntryUserEmail.Text;
            if (string.IsNullOrEmpty(userName)) {


                //Console.WriteLine("Bitte füge einen Namen ein");
                DisplayAlert("Error", "Bitte füge einen Namen ein", "OK");
            }
            else if (string.IsNullOrEmpty(userPassword))
            {
                //Console.WriteLine("Bitte füge ein Password ein");
                DisplayAlert("Error", "Bitte füge ein Passwort ein", "OK");
            }
            else if (string.IsNullOrEmpty(userEmail))
            {

                //Console.WriteLine("Bitte füge eine E-Mail ein");
                DisplayAlert("Error", "Bitte füge eine E-Mail Adresse ein", "OK");
            }
            else if (string.IsNullOrEmpty(userUniversity))
            {
                //Console.WriteLine("Bitte füge eine Universität ein");
                DisplayAlert("Error", "Bitte füge eine Universität ein", "OK");
            }

            else
            {
                SqlCommand insertCommand = new SqlCommand("insert into Users(Name,Email,Password,University) values(@userName, @userEmail, @userPassword,@UserUniversity)");

                /*This Part is to make the Data private*/
                insertCommand.Parameters.AddWithValue("@userName", userName);
                insertCommand.Parameters.AddWithValue("@userEmail", userEmail);
                insertCommand.Parameters.AddWithValue("@userPassword", userPassword);
                insertCommand.Parameters.AddWithValue("@userUniversity", userUniversity);

                int row = objDBAccess.ExecuteQuery(insertCommand);
                if (row == 1)
                {
                    DisplayAlert("Registration successfull", "Account created successfully", "Continue to login");
                    Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    DisplayAlert("Error", "Account could not be created", "OK");
                }
            }

        }
    }
}