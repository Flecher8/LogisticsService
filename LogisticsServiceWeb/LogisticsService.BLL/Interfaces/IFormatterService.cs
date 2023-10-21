using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IFormatterService
    {
        public string FormateNumberToInvariantCulture(double number);
    }
}
