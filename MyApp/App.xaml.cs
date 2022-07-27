using FreshMvvm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Models;
using MyApp.Repositories.Interface;
using Refit;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp
{
    public partial class App : Application
    {
        public static AppRepository appRepo { get; set; }
        public static TaxService taxService { get; set; }
        private static MyAppDatabase myAppDatabase;
        public static Customer User;
        private string[] args;

        public static MyAppDatabase MyAppDatabase
        {
            get
            {
                if(myAppDatabase == null)
                {
                    myAppDatabase = new MyAppDatabase();
                }
                return myAppDatabase;
            }
        }

        public  App(string dbPath)
        {
            InitializeComponent();
            MyAppDatabase myAppDb = MyAppDatabase;
            appRepo = new AppRepository(dbPath);


             taxService = new TaxService();
            // To set MainPage for the Application  
            //var page = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            //var database = new MyAppDatabase();

            //var page = FreshPageModelResolver.ResolvePageModel<AddressPageModel>();
            var page = FreshPageModelResolver.ResolvePageModel<StoresPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page);

 
            MainPage = basicNavContainer;
        }

        protected async override void OnStart()
        {
            App.User = await appRepo.GetUserByEmail("wcalvarez@icloud.com");
            //using IHost host = Host.CreateDefaultBuilder(args)
            //.ConfigureServices((_, services) =>
            //{
            //    services
            //    .AddRefitClient<IUsersClient>()
            //    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"));
            //}).Build();
            //var usersClient = host.Services.GetRequiredService<IUsersClient>();
            //var users = await usersClient.GetAll();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
