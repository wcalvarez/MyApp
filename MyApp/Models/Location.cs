using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    public class Location
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id { get; set; }  //local table primary key
        public int LocationId { get; set; } //backend Table Primary Key
        public int AddressId { get; set; }
        public string Name { get; set; }  //i.e Store Name
    }
}
