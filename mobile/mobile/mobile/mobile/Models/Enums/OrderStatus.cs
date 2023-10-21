using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models.Enums
{
    public enum OrderStatus
    {
        WaitingForAcceptanceByLogisticCompany,
        WaitingForPaymentByPrivateCompany,
        OrderAccepted,
        PreparingForDispatch,
        InTransit,
        Delivered,
        Cancelled,
    }
}
