using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IAnalyticsService
    {
        string GetAverageDeliveryTimeByPrivateCompany(int privateCompanyId);

        string GetAverageDeliveryTimeByLogisticCompany(int logisticCompanyId);

        double GetAverageDeliveryPathLengthByPrivateCompany(int privateCompanyId, string metric);

        double GetAverageDeliveryPathLengthByLogisticCompany(int logisticCompanyId, string metric);

        int GetNumberOfDeliveredOrdersByPrivateCompany(int privateCompanyId);

        int GetNumberOfDeliveredOrdersByLogisticCompany(int logisticCompanyId);

        int GetNumberOfNotDeliveredOrdersByPrivateCompany(int privateCompanyId);

        int GetNumberOfNotDeliveredOrdersByLogisticCompany(int logisticCompanyId);

        double GetAverageOrderPriceByPrivateCompany(int privateCompanyId);

        double GetAverageOrderPriceByLogisticCompany(int logisticCompanyId);

    }
}
