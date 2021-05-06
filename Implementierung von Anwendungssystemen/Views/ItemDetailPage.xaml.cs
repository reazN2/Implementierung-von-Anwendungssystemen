using Implementierung_von_Anwendungssystemen.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Implementierung_von_Anwendungssystemen.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}