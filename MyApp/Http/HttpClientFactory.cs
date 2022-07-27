
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public class HttpClientFactory : IHttpClientFactory
    {
        #region Fields

        private readonly IApiConfigurator _apiConfigurator;

        private string _baseUrl;
        private HttpClient _httpClientAuthorised;
        private HttpClient _httpClientNotAuthorised;

        #endregion Fields

        #region Constructors

        public HttpClientFactory(IApiConfigurator apiConfigurator)
        {
            _apiConfigurator = apiConfigurator;
        }

        #endregion Constructors

        #region Public Methods

        public HttpClient CreateHttpClient(string baseUrl, bool requiresAuthorisation)
        {
            _baseUrl = baseUrl;

            var httpClient = requiresAuthorisation
                ? CreateAuthorisedHttpClient()
                : CreateNotAuthorisedHttpClient();
            return httpClient;
        }

        #endregion Public Methods

        #region Private Methods

        private HttpClient CreateAuthorisedHttpClient()
        {
            if (_httpClientAuthorised == null || _httpClientAuthorised.BaseAddress.AbsoluteUri != _baseUrl)
            {
                HttpMessageHandler handler = new AuthorizedHandler(GetAuthenticationHeader);
                handler = new LoggerHttpMessageHandler(handler);
                _httpClientAuthorised = new HttpClient(handler);
                _httpClientAuthorised.BaseAddress = new Uri(_baseUrl);
            }

            return _httpClientAuthorised;
        }

        private HttpClient CreateNotAuthorisedHttpClient()
        {
            if (_httpClientNotAuthorised == null || _httpClientAuthorised.BaseAddress.AbsoluteUri != _baseUrl)
            {
                HttpMessageHandler handler = new HttpClientHandler();
                handler = new LoggerHttpMessageHandler(handler);
                _httpClientNotAuthorised = new HttpClient(handler);
                _httpClientNotAuthorised.BaseAddress = new Uri(_baseUrl);
            }

            return _httpClientNotAuthorised;
        }

        private Task<string> GetAuthenticationHeader()
        {
            return Task.FromResult(_apiConfigurator.GetApiKey());
        }

        #endregion Private Methods
    }
}
