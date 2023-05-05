using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IRateService
    {
        Rate? GetRateById(int rateId);

        Rate? GetRateByLogisticCompanyId(int logisticCompanyId);

        void CreateRate(Rate rate);

        void UpdateRate(RateDto rate);
    }
}
