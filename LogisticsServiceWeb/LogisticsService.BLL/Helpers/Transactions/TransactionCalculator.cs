using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Helpers.Transactions
{
    public class TransactionCalculator
    {
        protected double Price;
        private const double commisionPersent = 1;


        public double CommisionPersent
        {
            get { return commisionPersent; }
        }
    }
}
