using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public PrivateCompany PrivateCompany { get; set; } = null!;
        public LogisticCompany LogisticCompany { get; set; } = null!;
        public Order Order { get; set; } = null!;
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public double CommissionPercent { get; set; }
        public double EarnedAmount { get; set; }
        
    }
}
