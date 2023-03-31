using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Enums
{
    public enum OrderStatus
    {
        WaitingForAcceptanceByPrivateCompany,
        WaitingForAcceptanceByLogisticCompany,
        OrderAccepted,
        PreparingForDispatch,
        InTransit,
        Delivered,
        Cancelled,
    }
}
