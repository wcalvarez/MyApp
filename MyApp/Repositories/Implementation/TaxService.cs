using Microsoft.Extensions.Hosting;
using MyApp.Dto.TaxJar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyApp
{
    public class TaxService : ITaxService
    {
        #region Http stuff
        //Refit Solution
        //
        //
 

    //
    //
    private APIService _apiService;
        private async Task<HttpClient> GetClient()

        {
            
        
        var client = new HttpClient();
            _apiService = new APIService();
            
            HttpMessageHandler handler = new AuthorizedHandler(GetAuthenticationHeader);
           // handler = new LoggerHttpMessageHandler(handler);
            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add(GetKey(), GetValue());
            //client.BaseAddress = new Uri(_apiService.GetTaxJarBaseUrl());

            return client;
        }

        public string GetKey()
        {
            return "Authorization";
        }

        public string GetValue()
        {
            return "Token token=" + _apiService.GetTaxJarApiKey();
        }
        private Task<string> GetAuthenticationHeader()
        {
            return Task.FromResult(_apiService.GetTaxJarApiKey());
        }

        #endregion
        public async Task<decimal> CalculateTax(TaxForOrder order)
        {
            string Url2 = "https://api.taxjar.com/v2/taxes";
            HttpClient client = await GetClient();
            
            var myContent = JsonConvert.SerializeObject(order);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = new HttpResponseMessage();
            TaxAmount taxes = new TaxAmount();
            try
            {
                response = await client.PostAsync(Url2, byteContent);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                taxes = JsonConvert.DeserializeObject<TaxAmount>(contents);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", response.ReasonPhrase, "OK");
            }
            return taxes.amount_to_collect;
        }

        public async Task<decimal> GetTaxRate(TaxRateInput location)
        {
            TaxRate rate = new TaxRate();

            string Url = "https://api.taxjar.com/v2/rates/";
            Url += location.zip;


            HttpClient client = await GetClient();
            var builder = new UriBuilder(Url);
  
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["country"] = location.country;
            query["city"] = location.city;
            builder.Query = query.ToString();
            string url = builder.ToString();
            try
            {
                var jsonString = await client.GetStringAsync(url);
                 rate = JsonConvert.DeserializeObject<TaxRate>(jsonString);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
            
            return rate.combined_rate;
        }
    }
}
