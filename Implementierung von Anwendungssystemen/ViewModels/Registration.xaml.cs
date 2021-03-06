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
    public partial class Registration : ContentPage
    {
        DBAccess objDBAccess = new DBAccess();
        DataTable dtUsers = new DataTable();
        string name;
        public void PickerUniversity_SelectedIndexChanged(object sender, EventArgs e)
        {

            var name = PickerUniversity.Items[PickerUniversity.SelectedIndex];

        }

        public Registration()
        {
            InitializeComponent();
            // Sets Picker values for the Universities
            PickerUniversity.Items.Add("Siegen");
            PickerUniversity.Items.Add("Crete");
            PickerUniversity.Items.Add("Maribor");
            PickerUniversity.Items.Add("Orleans");
            PickerUniversity.Items.Add("Porto");
            PickerUniversity.Items.Add("Rome");
            PickerUniversity.Items.Add("Vilnius");

        }
        //sets all the variables that a user put into the xaml entry and assignes them to different variables
        void Button_Clicked(object sender, EventArgs e)
        {
            string userName = EntryUserName.Text;
            string userPassword = EntryUserPassword.Text;
            string userUniversity;
            bool   userLocked = false;
            string userRole = "User";
            string userEmail;
            string alreadyExists;
            //checks if university Picker is empty
            if (PickerUniversity.SelectedIndex < 0)
            {
                userUniversity = "";
            }
            else
            {
                userUniversity = PickerUniversity.Items[PickerUniversity.SelectedIndex];
            }
            //checks if the email has the right format and if the user is from one of the participating universities
            var email = EntryUserEmail.Text;
            var emailPattern =
                (@"^[a-zA-Z0-9._%+-]+(@student.uni-siegen.de|@unicusano.it|@unicusano.com|@student.um.si|@um.si|@hmu.gr|@vgtu.lt|@stud.vgtu.lt|@vilniustech.lt|@ipp.pt|@etu.univ-orleans.fr)$");
            if (Regex.IsMatch(email, emailPattern))
            {
                userEmail = email;
            }
            else
            {
                userEmail = "";
            }
            // checks if the email is already used
          string query = "Select * from Users Where Email= '" + userEmail + "'";

            objDBAccess.ReadDatathroughAdapter(query, dtUsers);

            if (dtUsers.Rows.Count > 0)
            {
                alreadyExists = "true";
            }
            else
            {
                alreadyExists = "false";
            }

            //checks if all the entries are filled
            if (string.IsNullOrEmpty(userName))
            {
                DisplayAlert("Error", "Please enter a name", "OK");
            }
            else if (string.IsNullOrEmpty(userPassword))
            {
                DisplayAlert("Error", "Please enter a password", "OK");
            }
            else if (string.IsNullOrEmpty(userEmail))
            {
                DisplayAlert("Error", "University mail from participating universities required", "OK");
            }
            else if (string.IsNullOrEmpty(userUniversity))
            {
                DisplayAlert("Error", "Please select a university", "OK");
            }
            //displays an error if the user already exists
            else if (alreadyExists == "true")
                {
                    DisplayAlert("Error", "That email adress is already used", "OK");

                } 
                else
                {   //assignes fields to the right columns of the database
                    SqlCommand insertCommand = new SqlCommand("insert into Users(Name,Email,Password,University,Roles,Locked) values(@userName, @userEmail, @userPassword, @userUniversity, @userRole, @userLocked)");

                    //insert the data to the right colums in the database
                    insertCommand.Parameters.AddWithValue("@userName", userName);
                    insertCommand.Parameters.AddWithValue("@userEmail", userEmail);
                    insertCommand.Parameters.AddWithValue("@userPassword", userPassword);
                    insertCommand.Parameters.AddWithValue("@userUniversity", userUniversity);
                    insertCommand.Parameters.AddWithValue("@userRole", userRole);
                    insertCommand.Parameters.AddWithValue("@userLocked", userLocked);

                int row = objDBAccess.ExecuteQuery(insertCommand);
                    if (row == 1)
                    {
                        DisplayAlert("Registration successful", "Account created successfully", "Continue to login");

                    //sets all the inputs back to zero and navigates to the LoginPage
                    EntryUserName.Text = null;
                    EntryUserPassword.Text = null;
                    EntryUserEmail.Text = null;
                    PickerUniversity.Items[PickerUniversity.SelectedIndex] = null;
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