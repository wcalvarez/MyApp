using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoresPage : ContentPage
    {
        public StoresPage()
        {
            InitializeComponent();
        }
        async void OnImageButtonClicked(object sender, EventArgs e)
        {

            //ImageButton button = (ImageButton)sender;
            //var loc = button.CommandParameter;
            //Models.Location detailLocation = (Models.Location)loc;
            //await Navigation.PushAsync(new LocationDetailPage(detailLocation));
        }
        protected void ListItems_Refreshing(object sender, EventArgs e)
        {

            //locations = new ObservableCollection<Models.Location>();
            //geo_locations = new ObservableCollection<Models.Location>();

            //downloadLocations("WJ3");
            //lstView.EndRefresh();
        }
        async void ShowProducts(object sender, ItemTappedEventArgs e)
        {
            //App.BrowsingLocation = (Models.Location)e.Item;
            //lstView.SelectedItem = null;
            //await Navigation.PushAsync(new ProductsPage(App.BrowsingLocation.LocationID));
        }
    }
}