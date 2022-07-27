using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp
{
    public interface IApiConfigurator
    {
        string GetApiUrl();
        string GetApiKey();
    }
}
