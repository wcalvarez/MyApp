using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Dto.TaxJar
{
    public class NexusAddress
    {
        public string id { get; set; } //location.Name
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; } //loc.state
        public string zip { get; set; } //loc.zipcode
        public string country { get; set; } //location.Country
    }
}
