using FreshMvvm;
using MyApp.DataStore;
using MyApp.Dto;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Essentials;
using System.Globalization;
using MyApp.Dto.TaxJar;
using MyApp.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp
{
    public class AddressPageModel : FreshBasePageModel, INotifyPropertyChanged
    {
       // private readonly IRatesApi _ratesApi;
        public AddressPageModel(LocationDto location)
        {   
            address = new Address();
            CalculateCommand = new Command(SubmitTax);
            CompleteCommand = new Command(CompleteOrder);
            var locs = App.appRepo.GetLocationsAsync();
            
            CalculateTax = true;
            CheckOut = false;   
        }
       
        #region Override Functions
        LocationDto location = new LocationDto();
        public override void Init(object initData)
        {
            base.Init(initData);
            location = initData as LocationDto;
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            
            Countries = new ObservableCollection<CountryDto>();
            Countries.Add(new CountryDto { CountryId = 1, Name = "United States", isoalphacode2 = "US", isoalphacode3 = "USA", languageCode = "en-US" });

            SelectedCountry = Countries[0];

            States = new ObservableCollection<State>();
            List<State> states = await App.appRepo.GetStatesAsync(SelectedCountry.CountryId);
            states = states.OrderBy(x => x.Code).ToList();
            foreach(State state in states)
            {
                States.Add(state);
            }
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }

        #endregion

        #region Commands
        private async void CompleteOrder()
        {
            OrderAmount = 0;
            address = new Address();

            await CoreMethods.PopPageModel();
        }
        private async void SubmitTax()
        {
            bool valid = await validEntries();

            if (!valid)
                return;

            address.Country = selectedCountry.isoalphacode2;

            
           if (selectedCountry.isoalphacode3 == "USA")
           {
               address.State = selectedState.Code;
           }
           else
           {
               address.State = selectedState.Name;
           }

            //Get Tax Amount, Tax Rate from TaxJar
            string subtotal = OrderAmount.ToString("C", cfi);
            SubTotal = "Sub Total:......." + subtotal;

            //get location address
            Models.Location loc = await App.appRepo.GetLocationById(location.LocationId);
            var locAddress = await App.appRepo.GetAddressById(loc.AddressId);

            TaxForOrder taxInput = new TaxForOrder();

            taxInput.from_street = locAddress.Address1;
            taxInput.from_city = locAddress.City;
            taxInput.from_state = locAddress.State;
            taxInput.from_zip = locAddress.Zipcode;
            taxInput.from_country = locAddress.Country;
            taxInput.to_city = address.City;
            taxInput.to_street = address.Address1;
            taxInput.to_state = address.State;
            taxInput.to_zip = address.Zipcode;
            taxInput.to_country = address.Country;
            taxInput.amount = OrderAmount;
            taxInput.shipping = 5;

            TaxRateInput ti = new TaxRateInput();
            ti.city = address.City;
            ti.state = address.State;
            ti.country = address.Country;
            ti.zip = address.Zipcode;

            taxInput.nexus_addresses = new List<NexusAddress>();
            List<Address> nexus = await App.appRepo.GetAllAddresses();
            foreach (Address addr in nexus)
            {
                taxInput.nexus_addresses.Add(
                    new NexusAddress
                    {
                        id = addr.Address1,
                        street = addr.Address1,
                        country = addr.Country,
                        city = addr.City,
                        state = addr.State,
                        zip = addr.Zipcode
                    }
                    );
            }
            //TaxRates rateService = new TaxRates();
            decimal Tax = 0;
            //decimal taxRate = await App.taxService.GetTaxRate(ti);
            decimal taxAmount = await App.taxService.CalculateTax(taxInput);

            //Tax = OrderAmount * taxRate;
            Tax = taxAmount;
            //string tax = Tax.ToString("C", cfi);
            string tax = taxAmount.ToString("C", cfi);
            SalesTax = "Sales Tax:......." + tax;

            decimal total = OrderAmount + Tax;

            string ordtotal = total.ToString("C", cfi);
            OrderTotal = "Grand Total:....." + ordtotal;
            CheckOut = true;
            CalculateTax = false;

        }

        private async Task<bool> validEntries()
        {
            if(OrderAmount == 0 || String.IsNullOrEmpty(address.Address1) ||
                String.IsNullOrEmpty(address.City) || SelectedState == null || String.IsNullOrEmpty(address.Zipcode))
            {
                await App.Current.MainPage.DisplayAlert("Alert", "All entries are required.", "OK");
            }
            else
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Properties
        public static ObservableCollection<Address> addresses { get; set; }
        public ObservableCollection<CountryDto> Countries { get; set; }
        public ObservableCollection<State> States { get; set; }

        public decimal OrderAmount { get; set; }
        public ICommand CalculateCommand { get; }
        public ICommand CompleteCommand { get; }
        public bool CalculateTax { get; set; }
        public bool CheckOut { get; set; }
        public Address address { get; set; }
        public string SubTotal { get; set; }
        public string SalesTax { get; set; }
        public string OrderTotal { get; set; }
        CultureInfo cfi;

        readonly DataManager manager = new DataManager();

        CountryDto selectedCountry;

        public CountryDto SelectedCountry
        {
            get
            {
                return selectedCountry;
            }
            set
            {
                selectedCountry = value;

            }
        }

        State selectedState;

        public State SelectedState
        {
            get
            {
                return selectedState;
            }
            set
            {
                selectedState = value;
            }
        }
        public Address selectedAddress;
        public Address SelectedAddress 
        {
            get { return selectedAddress; }
            set 
            {

            if(selectedAddress != value)
            selectedAddress = value;
                RaisePropertyChanged("SelectedAddress");
            }
        }
        #endregion
    }
}
