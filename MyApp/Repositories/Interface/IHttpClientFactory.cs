using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyApp
{
    public interface IHttpClientFactory
    {
        HttpClient CreateHttpClient(string baseUrl, bool requiresAuthorisation);
    }
}
