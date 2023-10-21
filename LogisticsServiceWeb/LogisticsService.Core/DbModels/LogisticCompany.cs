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
        public string CompanyName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public string? Description { get; set; }
        public List<SmartDevice>? SmartDevices { get; set; }
        public List<LogisticCompaniesAdministrator>? LogisticCompaniesAdministrators { get; set; }
        public List<LogisticCompaniesDriver>? LogisticCompaniesDrivers { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Transaction>? Transactions { get; set; }

    }
}
