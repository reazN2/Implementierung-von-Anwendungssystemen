using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Implementierung_von_Anwendungssystemen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Einstellungen : ContentPage
    {
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

        }
    }
}