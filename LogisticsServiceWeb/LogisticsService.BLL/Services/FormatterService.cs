using LogisticsService.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class FormatterService : IFormatterService
    {
        public string FormateNumberToInvariantCulture(double number)
        {
            return number.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
