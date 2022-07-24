using MyApp.Models;
using System;
using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace MyApp
{
    public class AppRepository
    {
        static SQLiteAsyncConnection conn;
        string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyApp.db");
        public string StatusMessage { get; set; }

        public Address selectedRecipient = new Address();

        public AppRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(DatabasePath);
        }

        // CUSTOMER TABLE //
        public async Task<Customer> GetUserByEmail(string email)
        {
            return await conn.Table<Customer>().Where(x => x.Email == email).FirstOrDefaultAsync();
        }
        // LOCATION TABLE //
        public async Task<List<Location>> GetLocationsAsync()
        {
            List<Location> locations = await conn.Table<Location>().ToListAsync();
            return locations;
        }

        // STATES TABLE //
        public async Task<List<State>> GetStatesAsync(int countryId)
        {
            List<State> states = await conn.Table<State>().ToListAsync();
            return states;
        }

        #region ADDRESS TABLE
        public async void AddNewAddress(Address address)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                bool validAddress = (!String.IsNullOrEmpty(address.Address1)) &&
                                    (!String.IsNullOrEmpty(address.City)) &&
                                    (!String.IsNullOrEmpty(address.State)) &&
                                    (!String.IsNullOrEmpty(address.Zipcode));

                if (!validAddress)
                    await  App.Current.MainPage.DisplayAlert("Alert", "Recipient Name, Address & City are required.", "OK");

                result = await conn.InsertAsync(address);

                StatusMessage = string.Format("{0} record(s) added [Address: {1})", result, address);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", address, ex.Message);
            }
        }

        public async void DeleteAddress(Address address)
        {
            conn.DeleteAsync(address);
        }

        public async Task<List<Address>> GetAllAddresses()
        {
            List<Address> addresses = new List<Address>();
            try
            {
                addresses = await conn.Table<Address>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return addresses;
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await conn.Table<Address>().Where(x => x.AddressId == id).FirstOrDefaultAsync();
        }

        public async Task<Location> GetLocationById(int id)
        {
            return await conn.Table<Location>().Where(x => x.LocationId == id).FirstOrDefaultAsync();
        }
        #endregion

    }
}
