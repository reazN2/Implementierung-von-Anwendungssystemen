﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Implementierung_von_Anwendungssystemen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Distanzen : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public Distanzen()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "Laufen",
                "Schwimmen",
                "Fahrrad Fahren",
                "spazieren",
                "Krafttraining"
            };

            MyListView.ItemsSource = Items;
        }


        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}