using System;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;
using Xamarin.UITest.Queries;

namespace MyApp.UITest1
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform, bool clearData)
        {
            if (platform == Platform.Android)
            {
                
                return ConfigureApp.Android
                    .InstalledApp("com.companyname.myapp")
                    .EnableLocalScreenshots()
                    .StartApp(clearData ?
                         AppDataMode.Clear : AppDataMode.DoNotClear);
            }

            return ConfigureApp.iOS
                .InstalledApp("com.companyname.MyApp")
                .EnableLocalScreenshots()
                .StartApp(
                        clearData ?
                            AppDataMode.Clear : AppDataMode.DoNotClear);
        }
    }
}