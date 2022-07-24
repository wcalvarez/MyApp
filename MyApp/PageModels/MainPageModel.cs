using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FreshMvvm;
using System;

namespace MyApp
{
    public class MainPageModel : FreshBasePageModel
    {

        #region Default Override functions  
        public override void Init(object initData)
        {
            base.Init(initData);
            MainPageLabel = "Welcome to Fresh Mvvm Tutorial!";
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
        }

        protected  override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            //await CoreMethods.PushPageModel<AddressPageModel>();

        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }
        #endregion

        #region Commands  
        public Command GotoSecondPageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<AddressPageModel>();
                });
            }
        }
        #endregion

        #region Properties  
        string _mainPageLabel = string.Empty;
        public string MainPageLabel
        {
            get
            {
                return _mainPageLabel;
            }
            set
            {
                if (_mainPageLabel != value)
                {
                    _mainPageLabel = value;
                    RaisePropertyChanged(nameof(MainPageLabel));
                }
            }
        }
        #endregion
    }
}