using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface ISensorService
    {
        Sensor? GetSensorById(int sensorId);

        List<Sensor> GetAllSensorsBySmartDeviceId(int smartDeviceId);

        List<SensorDto> GetAllSensors();

        void CreateSensor(SensorDto sensorDto);

        void UpdateSensor(SensorDto sensor);

        void DeleteSensor(int sensorId);

        void ChangeSensorStatus(int sensorId, SensorStatus sensorStatus);
    }
}
