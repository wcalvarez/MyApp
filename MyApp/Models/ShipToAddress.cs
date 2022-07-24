using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    public class ShipToAddress : Address
    {
        public new int Id { get; set; }
        public int ShipToAddressId { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
    }
}
