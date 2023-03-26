using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsService.Core.Enums;

namespace LogisticsService.Core.DbModels
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public LogisticCompany LogisticCompany { get; set; } = null!;
        public SubscriptionType SubscriptionType { get; set; } = null!;
        public SubscriptionStatus SubscriptionStatus { get; set; }
    }
}
