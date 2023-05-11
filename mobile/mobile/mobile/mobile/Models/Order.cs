using mobile.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public PrivateCompany PrivateCompany { get; set; } 
        public LogisticCompany LogisticCompany { get; set; } 
        public LogisticCompaniesDriver LogisticCompaniesDriver { get; set; }
        public Sensor Sensor { get; set; }
        public Cargo Cargo { get; set; }

        public Address StartDeliveryAddress { get; set; } 
        public Address EndDeliveryAddress { get; set; } 

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreationDateTime { get; set; }
        public DateTime EstimatedDeliveryDateTime { get; set; }
        public DateTime? StartDeliveryDateTime { get; set; }
        public DateTime? DeliveryDateTime { get; set; }

        // Price saved in dollars
        public double Price { get; set; }
        // In meters
        public int PathLength { get; set; }
    }
}
