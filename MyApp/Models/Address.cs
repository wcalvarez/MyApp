using SQLite;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    public class Address
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
    }
}
