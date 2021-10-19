using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDBLib
{
    public class Adress
    {

        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int? PostalCode { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }

    }
}
