using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Helpers.Transactions
{
    public class TransactionAmountCalculator : TransactionCalculator
    {
        public TransactionAmountCalculator(double price)
        {
            Price = price;
        }

        public double Compute()
        {
            return Price + commisionPersent * Price / 100;
        }
    }
}
