using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ISmartDeviceCommunicationService
    {
        bool WriteOrderTrackers(SmartDeviceCommunicationDto communicationDto);

        bool ActivateSensor(int sensorId);
    }
}
