using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public double Latitude { get; set; }
        public double Longitute { get; set; }
    }
}
