using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using LogisticsService.Core.Enums;
using LogisticsService.DAL.Interfaces;
using LogisticsService.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class SensorService : ISensorService
    {
        private readonly IDataRepository<Sensor> _sensorRepository;
        private readonly ISmartDeviceService _smartDeviceService;

        private readonly ILogger<SensorService> _logger;

        public SensorService(
            IDataRepository<Sensor> sensorRepository,
            ISmartDeviceService smartDeviceService,
            ILogger<SensorService> logger)
        {
            _sensorRepository = sensorRepository;
            _smartDeviceService = smartDeviceService;
            _logger = logger;
        }

        public void CreateSensor(SensorDto sensorDto)
        {
            IsSensorValid(sensorDto);

            Sensor sensor = new Sensor();
            sensor.SmartDevice = _smartDeviceService.GetSmartDeviceById(sensorDto.SmartDeviceId);
            sensor.Status = SensorStatus.Inactive;
            
            try
            {
                _sensorRepository.InsertItem(sensor);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private bool IsSensorValid(SensorDto sensorDto)
        {
            if(_smartDeviceService.GetSmartDeviceById(sensorDto.SmartDeviceId) == null)
            {
                throw new ArgumentOutOfRangeException("SmartDevice with such id does not exist");
            }
            return true;
        }

        public void DeleteSensor(int sensorId)
        {
            try
            {
                _sensorRepository.DeleteItem(sensorId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public List<Sensor> GetAllSensors()
        {
            var sensors = new List<Sensor>();
            try
            {
                sensors = _sensorRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return sensors;
        }

        public List<Sensor> GetAllSensorsBySmartDeviceId(int smartDeviceId)
        {
            var sensors = new List<Sensor>();
            try
            {
                sensors = _sensorRepository.GetFilteredItems(s => s.SmartDevice.SmartDeviceId == smartDeviceId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return sensors;
        }

        public Sensor? GetSensorById(int sensorId)
        {
            try
            {
                Sensor? sensor = _sensorRepository.GetItemById(sensorId);
                return sensor;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public void UpdateSensor(Sensor sensor)
        {
            try
            {
                _sensorRepository.UpdateItem(sensor);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
