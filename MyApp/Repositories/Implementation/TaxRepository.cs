using MyApp.Dto.TaxJar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{

    public class TaxRepository : ITaxRepository
    {
        public ITaxService _taxService;

        public TaxRepository(ITaxService taxService)
        {
            _taxService = taxService;
        }

        public async Task<decimal> GetTaxRate(TaxRateInput location)
        {
            decimal rate = await _taxService.GetTaxRate(location);
            return rate;
        }

        public async Task<decimal> CalculateTax(TaxForOrder order)
        {
            decimal tax = await _taxService.CalculateTax(order);
            return tax;
        }
    }
}
