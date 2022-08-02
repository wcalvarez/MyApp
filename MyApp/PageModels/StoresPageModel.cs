using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FreshMvvm;
using System.Linq;
using MyApp.Dto;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyApp
{
    
    public class StoresPageModel : FreshBasePageModel , INotifyPropertyChanged
    {
        
        public StoresPageModel()
        {
            
        }
        #region Function Overrides
        public async override void Init(object initData)
        {
            base.Init(initData);

        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            List<Location> locs = await App.appRepo.GetLocationsAsync();
            locs = locs.OrderBy(x => x.Name).ToList();
            List<LocationDto> locList = new List<LocationDto>();
            Locations = new ObservableCollection<LocationDto>();
            if (locs != null)
            {
                foreach (Location loc in locs)
                {
                    Address address = await App.appRepo.GetAddressById(loc.AddressId);
                    LocationDto ldto = new LocationDto
                    {
                        LocationId = loc.LocationId,
                        Name = loc.Name
                    };
                    if (address != null)
                    {
                        ldto.Address = address.Address1 + ", " + address.City + ", "  + address.State +
                            " " + address.Zipcode + " " + address.Country;
                    }
                    locList.Add(ldto);
                }
            }

            foreach (LocationDto ld in locList)
            {
                Locations.Add(ld);
            }
        }

        protected   override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);

        }
        #endregion
        #region Commands
        // private readonly IRatesApi _ratesApi;
        
        public Command<LocationDto> LocationSelected
        {
            get
            {
                return new Command<LocationDto>(async (location) => {
                    await CoreMethods.PushPageModel<AddressPageModel>(location);
                });
            }
        }
        #endregion
        #region Properties
        public ObservableCollection<LocationDto> Locations { get; set; }

        LocationDto _selectedStore;

        public LocationDto SelectedStore
        {
            get
            {
                return _selectedStore;
            }
            set
            {
                _selectedStore = value;
                if (value != null)
                    LocationSelected.Execute(value);
            }
        }
      //  private LocationDto locationDto;
        //public LocationDto LocationDto { get; set; }
        #endregion

    }
}
