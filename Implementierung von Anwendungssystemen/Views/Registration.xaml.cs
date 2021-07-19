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

        public Registration()
        {
            InitializeComponent();

        }
        void Button_Clicked(object sender, EventArgs e)
        {
            string userName = EntryUserName.Text;
            string userPassword = EntryUserPassword.Text;
            string userUniversity = EntryUserUniversity.Text;
            bool   userLocked = false;
            string userRole = "User";
            string userEmail;
            // string alreadyExists;
            var email = EntryUserEmail.Text;
            var emailPattern =
                (@"^[a-zA-Z0-9._%+-]+(@student.uni-siegen.de|@unicusano.it|@unicusano.com|@student.um.si|@um.si|@hmu.gr|@vgtu.lt|@stud.vgtu.lt|@vilniustech.lt|@ipp.pt|@etu.univ-orleans.fr)$");
            if (Regex.IsMatch(email, emailPattern))
            {
                userEmail = EntryUserEmail.Text;
            }
            else
            {
                userEmail = "";
            }

          /* string query = "Select * from Users Where Email= '" + userEmail + "' AND Roles = '" + userRole + "'";

            objDBAccess.ReadDatathroughAdapter(query, dtUsers);

            if (dtUsers.Rows.Count > 0)
            {
                alreadyExists = "true";
            }
            else
            {
                alreadyExists = "false";
            }

          */

               //string userRole = "User";
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
                    DisplayAlert("Error", "Please insert a university name", "OK");
                }
               /* else if (alreadyExists == "true")
                {
                    DisplayAlert("Error", "That email adress is already used", "OK");

                } */
                else
                {
                    SqlCommand insertCommand = new SqlCommand("insert into Users(Name,Email,Password,University,Roles,Locked) values(@userName, @userEmail, @userPassword, @userUniversity, @userRole, @userLocked)");

                    /*This Part is to make the Data private*/
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