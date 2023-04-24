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

        SmartDevice CreateSmartDevice(SmartDeviceDto smartDevice);

        SmartDevice UpdateSmartDevice(SmartDeviceDto smartDeviceDto);

        void DeleteSmartDevice(int smartDeviceId);
    }
}
