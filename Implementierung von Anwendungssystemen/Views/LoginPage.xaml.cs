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
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new Registration());
            Shell.Current.GoToAsync("//Registration");

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string userEmail = EntryUserEmail.Text;
            string userPassword = EntryUserPassword.Text;
            if (string.IsNullOrEmpty(userEmail))
            {
                DisplayAlert("E-Mail fehlt", "Bitte füge einen E-Mail ein", "OK");
            }
            else if (string.IsNullOrEmpty(userPassword))
            {
                DisplayAlert("Passwort fehlt", "Bitte füge ein Passwort ein", "OK");
            } else
            {
                string query = "Select * from Users Where Email= '" + userEmail + "' AND Password = '" + userPassword + "'";

                objDBAccess.ReadDatathroughAdapter(query, dtUsers);

                if(dtUsers.Rows.Count == 1)
                {
                    id = dtUsers.Rows[0]["Id"].ToString();
                    email = dtUsers.Rows[0]["Email"].ToString();
                    university = dtUsers.Rows[0]["University"].ToString();
                    password = dtUsers.Rows[0]["Password"].ToString();
                    name = dtUsers.Rows[0]["Name"].ToString();
                    roles = dtUsers.Rows[0]["Roles"].ToString();

                    DisplayAlert("Login erfolgreich","Sie haben sich erfolgreich eingeloggt","Weiter zur App");
                    objDBAccess.CloseConn();
                    // Navigation.PushAsync(new AboutPage());
                    Shell.Current.GoToAsync("//AboutPage");
                } else
                {
                    DisplayAlert("Fehler", "E-Mail oder Passwort falsch", "OK");
                }
            }
        }
    }
}
