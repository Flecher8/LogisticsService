using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class LogisticCompany
    {
        public int LogisticCompanyId { get; set; }
        public string LogisticCompanyName { get; set; } = null!;
        public string LogisticCompanyEmail { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public string? Description { get; set; }
    }
}
