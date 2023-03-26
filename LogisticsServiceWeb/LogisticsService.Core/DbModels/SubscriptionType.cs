using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class SubscriptionType
    {
        public int SubscriptionTypeId { get; set; }
        public string SubscriptionTypeName { get; set; } = null!;
        public int DurationInDays { get; set; }
        public double Price { get; set;  }
    }
}
