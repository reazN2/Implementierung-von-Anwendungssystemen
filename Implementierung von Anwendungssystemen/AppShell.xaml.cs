using Implementierung_von_Anwendungssystemen.ViewModels;
using Implementierung_von_Anwendungssystemen.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Implementierung_von_Anwendungssystemen
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
