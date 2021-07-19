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
    public partial class LoginPage : ContentPage
    {   
        /*Das Nächste ist static, damit man die Daten überall erreichen kann*/
        public static string id, email, university, password, name, roles;

        DBAccess objDBAccess = new DBAccess();
        DataTable dtUsers = new DataTable();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
       // private void Button_Clicked_1(object sender, EventArgs e)
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
           // Navigation.PushAsync(new Registration());

            await Shell.Current.GoToAsync(state: "//LoginPage/Registration");

        }

        private async void Button_Clicked(object sender, EventArgs e)
        //private void Button_Clicked(object sender, EventArgs e)
        {
            string userEmail = EntryUserEmail.Text;
            string userPassword = EntryUserPassword.Text;
            //This bool was created so we can compare this bool with the Column Locked in the SQL Server. The user can only login if the Column 'Locked' in the Database is also false.
            bool locked = false;
            if (string.IsNullOrEmpty(userEmail))
            {
                DisplayAlert ("E-Mail missing", "Please insert a e-mail adress", "OK");
            }
            else if (string.IsNullOrEmpty(userPassword))
            {
                DisplayAlert ("Passwort missing", "Please insert a password", "OK");
            } else
            {
                //Looks if there is a User that has the same Email and Password as the ones a user just entered in the frontend. Also checks if the user is not deactivated.
             string query = "Select * from Users Where Email= '" + userEmail + "' AND Password = '" + userPassword + "' AND Locked = '" + locked + "'";

                objDBAccess.ReadDatathroughAdapter(query, dtUsers);

                if(dtUsers.Rows.Count == 1)
                {
                    id = dtUsers.Rows[0]["Id"].ToString();
                    email = dtUsers.Rows[0]["Email"].ToString();
                    university = dtUsers.Rows[0]["University"].ToString();
                    password = dtUsers.Rows[0]["Password"].ToString();
                    name = dtUsers.Rows[0]["Name"].ToString();
                    roles = dtUsers.Rows[0]["Roles"].ToString();
                    objDBAccess.CloseConn();

                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                } else
                {
                    DisplayAlert ("Error", "E-Mail or Password wrong or your Account was deactivated", "OK");
                }
            }
        }
    }
}
