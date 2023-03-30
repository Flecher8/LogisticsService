using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressName { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitute { get; set; }
    }
}
