using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class PrivateCompany
    {
        public int PrivateCompanyId { get; set; }
        public string? CompanyName { get; set; } = null!;
        public string CompanyEmail { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public string? Description { get; set; }
    }
}
