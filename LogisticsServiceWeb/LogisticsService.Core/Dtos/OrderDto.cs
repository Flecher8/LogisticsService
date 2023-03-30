using LogisticsService.Core.DbModels;
using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int? PrivateCompanyId { get; set; }
        public int? LogisticCompanyId { get; set; } 
        public int? LogisticCompaniesDriverId { get; set; } 
        public int? SensorId { get; set; } 
        public int? CargoId { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }
        public DateTime EstimatedDeliveryDateTime { get; set; }
        public DateTime StartDeliveryDateTime { get; set; }
        public DateTime DeliveryDateTime { get; set; }

        // Price saved in dollars
        public double Price { get; set; }
        // PathLength in meters
        public double PathLength { get; set; }

        public string DeliveryStartAddress { get; set; } = null!;
        public string DeliveryEndAddress { get; set; } = null!;
    }
}
