using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp
{
    public class TaxRate
    {
        public string zip { get; set; }
        public string state { get; set; }
        public decimal state_rate { get; set; }
        public string county { get; set; }
        public string county_rate { get; set; }
        public string city { get; set; }
        public decimal city_rate { get; set; }
        public decimal combined_district_rate { get; set; }
        public decimal combined_rate { get; set; }
        public bool freight_taxable { get; set; }
    }
}
