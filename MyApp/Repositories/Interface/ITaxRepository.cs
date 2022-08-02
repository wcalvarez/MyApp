using MyApp.Dto.TaxJar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public interface ITaxRepository
    {
        Task<decimal> GetTaxRate(TaxRateInput location);
        Task<decimal> CalculateTax(TaxForOrder order);
    }
}
