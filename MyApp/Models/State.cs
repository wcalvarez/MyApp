using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    public class State
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
    }
}
