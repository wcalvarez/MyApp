using MyApp.Dto;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressPage : ContentPage
    {
        public AddressPage()
        {
            InitializeComponent();
            orderAmount.Text = "";
        }

        public async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            //// Turn on network indicator
            //this.IsBusy = true;

            try
            {
                var picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;

                if (selectedIndex != -1)
                {
                    //CountryDto selectedCountry = (CountryDto)countrypicker.SelectedItem;
                    //retrieve States/Provinces for selected Country
                    //var states = await manager.GetStates(selectedCountry.CountryID);
                    //statelist = JsonConvert.DeserializeObject<IList<StateDto>>(states);
                    //statepicker.ItemsSource = statelist.ToList();
                }
            }
            catch (Exception err)
            {
                this.IsBusy = false;
                //await DisplayAlert("Ooops..", "Unable to load States/Provinces" + err.Message, "OK");
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    // Connection to internet is available
                    await DisplayAlert("Ooops..", "Unable to load State/Provinces, " + err.Message, "OK");
                }
                else
                {
                    await DisplayAlert("Ooops..", "Network Error: " + "You lost internet connection, try again", "OK");

                }
                await Navigation.PopToRootAsync();
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}