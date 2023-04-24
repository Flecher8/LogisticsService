using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Dtos
{
    public class SubscriptionDto
    {
        public int SubscriptionId { get; set; }
        public int LogisticCompanyId { get; set; }
        public int SubscriptionTypeId { get; set; }
    }
}
