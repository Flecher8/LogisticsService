using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Helpers.Transactions
{
    public class TransactionEarnedAmountCalculator : TransactionCalculator
    {
        public TransactionEarnedAmountCalculator(double price)
        {
            Price = price;
        }

        public double Compute()
        {
            return commisionPersent * Price / 100;
        }
    }
}
