using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class Rate
    {
        public int RateId { get; set; }
        public int PriceForKmInDollar { get; set; }
        public LogisticCompany LogisticCompany { get; set; } = null!;
    }
}
