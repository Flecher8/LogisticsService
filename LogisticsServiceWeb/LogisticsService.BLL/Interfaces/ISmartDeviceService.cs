using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ISmartDeviceService
    {
        SmartDevice? GetSmartDeviceById(int smartDeviceId);

        List<SmartDevice> GetAllSmartDevices();

        List<SmartDevice> GetSmartDevicesByLogisticCompanyId(int logisticCompanyId);

        void CreateSmartDevice(SmartDeviceDto smartDevice);

        void UpdateSmartDevice(SmartDevice smartDevice);

        void DeleteSmartDevice(int smartDeviceId);
    }
}
