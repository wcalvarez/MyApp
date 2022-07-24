using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp
{
    public interface IAPIService
    {
        string GetTaxJarBaseUrl();
        string GetTaxJarApiKey();
    }
}
