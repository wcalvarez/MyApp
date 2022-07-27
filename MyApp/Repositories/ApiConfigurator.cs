
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Repositories
{
    public class ApiConfigurator : IApiConfigurator
    {
        public string GetApiUrl() => "https://api.taxjar.com";
        public string GetApiKey() => "5da2f821eee4035db4771edab942a4cc";
    }
}
