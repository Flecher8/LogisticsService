using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class Order
    {
        public int OrderId { get; set; }

        public PrivateCompany PrivateCompany { get; set; } = null!;
        public LogisticCompany LogisticCompany { get; set; } = null!;
        public LogisticCompaniesDriver? LogisticCompaniesDriver { get; set; }
        public Sensor? Sensor { get; set; }
        public Cargo Cargo { get; set; } = null!;

        public Address StartDeliveryAddress { get; set; } = null!;
        public Address EndDeliveryAddress { get; set; } = null!;

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreationDateTime { get; set; }
        public DateTime EstimatedDeliveryDateTime { get; set; }
        public DateTime? StartDeliveryDateTime { get; set; }
        public DateTime? DeliveryDateTime { get; set; }

        // Price saved in dollars
        public double Price { get; set; }
        // PathLength in meters
        public double PathLength { get; set; }

    }
}
