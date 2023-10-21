using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class CancelledOrder
    {
        public int CancelledOrderId { get; set; }
        public Order Order { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateTime { get; set; }
        public OrderCancellationAuthority CancelledBy { get; set; }
        public int CancelledById { get; set; }
    }
}
