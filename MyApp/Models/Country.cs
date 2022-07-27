using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    public class Country
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string isoalphacode2 { get; set; }
        public string isoalphacode3 { get; set; }
        public string numeric3 { get; set; }
        public string languageCode { get; set; }
    }
}
