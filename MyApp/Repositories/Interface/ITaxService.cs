using MyApp.Dto.TaxJar;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public interface ITaxService
    {
        Task<decimal> CalculateTax(TaxForOrder order);
        Task<decimal> GetTaxRate(TaxRateInput location);
       // [Get("/v2/rates/{zip}")]
       // Task<ApiResponse<TaxRate>> GetRates(string zipCode, string country, string city = null);
    }
}
