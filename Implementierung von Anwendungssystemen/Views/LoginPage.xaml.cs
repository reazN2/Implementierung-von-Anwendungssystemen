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
        DBAccess objDBAccess = new DBAccess();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
        async void Button_Clicked(object sender, EventArgs e)
        {
            string userName = EntryUserName.Text;
            string userPassword = EntryUserPassword.Text;
            string userUniversity = EntryUserUniversity.Text;
            string userEmail = EntryUserEmail.Text;
            if (userName.Equals(""))
            {
                Console.WriteLine("Bitte füge einen Namen ein");
            }
            else
            {
                SqlCommand insertCommand = new SqlCommand("insert into Users(Name,Email,Password,University) values(@userName, @userEmail, @userPassword,@UserUniversity)");
               
                /*This Part is to make the Data private*/
                insertCommand.Parameters.AddWithValue("@userName", userName);
                insertCommand.Parameters.AddWithValue("@userEmail", userEmail);
                insertCommand.Parameters.AddWithValue("@userPassword", userPassword);
                insertCommand.Parameters.AddWithValue("@userUniversity", userUniversity);

                int row = objDBAccess.executeQuery(insertCommand);
                if (row == 1)
                {

                }
            }

        }

    }
}
