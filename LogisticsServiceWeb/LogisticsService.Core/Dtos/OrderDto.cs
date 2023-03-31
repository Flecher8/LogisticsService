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
        public int PrivateCompanyId { get; set; }
        public int LogisticCompanyId { get; set; }
        public int CargoId { get; set; }
        public int StartDeliveryAddressId { get; set; }
        public int EndDeliveryAddressId { get; set; }

        public DateTime EstimatedDeliveryDateTime { get; set; }

    }
}
