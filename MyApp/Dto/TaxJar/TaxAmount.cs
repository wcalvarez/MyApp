using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Dto.TaxJar
{
    public class TaxAmount
    {
        public decimal order_total_amount { get; set; }
        public decimal shipping { get; set; }
        public decimal taxable_amount {get;set;}
        public decimal amount_to_collect { get; set; }
        public decimal rate { get; set; }
        public bool freight_taxable { get; set; }
        public string tax_source { get; set; }
}
}
