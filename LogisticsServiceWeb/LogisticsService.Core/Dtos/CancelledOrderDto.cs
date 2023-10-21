using LogisticsService.Core.DbModels;
using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Dtos
{
    public class CancelledOrderDto
    {
        public int OrderId { get; set; }
        public string Reason { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string CancelledBy { get; set; } = null!;
        public int CancelledById { get; set; }
    }
}
