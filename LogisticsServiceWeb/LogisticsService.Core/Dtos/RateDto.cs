using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Dtos
{
    public class RateDto
    {
        public int RateId { get; set; }
        public int PriceForKmInDollar { get; set; }
    }
}
