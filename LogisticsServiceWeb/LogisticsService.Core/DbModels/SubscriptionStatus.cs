using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class SubscriptionStatus
    {
        public int SubscriptionStatusId { get; set; }
        public string SubscriptionStatusName { get; set; } = null!;
    }
}
