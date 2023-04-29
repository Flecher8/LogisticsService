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
    public class SmartDeviceService : ISmartDeviceService
    {
        private readonly IDataRepository<SmartDevice> _smartDeviceRepository;
        private readonly ILogisticCompanyService _logisticCompanyService;

        private readonly ILogger<SmartDeviceService> _logger;

        public SmartDeviceService(
            IDataRepository<SmartDevice> smartDeviceRepository,
            ILogisticCompanyService logisticCompanyService,
            ILogger<SmartDeviceService> logger)
        {
            _smartDeviceRepository = smartDeviceRepository;
            _logisticCompanyService = logisticCompanyService;
            _logger = logger;
        }

        public SmartDevice CreateSmartDevice(SmartDeviceDto smartDeviceDto)
        {
            SmartDevice smartDevice = new SmartDevice();
            smartDevice.NumberOfSensors = smartDeviceDto.NumberOfSensors;
            smartDevice.LogisticCompany = _logisticCompanyService
                .GetLogisticCompanyById(smartDeviceDto.LogisticCompanyId);

            IsSmartDeviceValid(smartDevice);

            try
            {
                _smartDeviceRepository.InsertItem(smartDevice);
                return smartDevice;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        private bool IsSmartDeviceValid(SmartDevice smartDevice)
        {
            if(smartDevice.NumberOfSensors <= 0)
            {
                throw new ArgumentOutOfRangeException("Number of sensors must be greater than 0");
            }
            return true;
        }

        public void DeleteSmartDevice(int smartDeviceId)
        {
            try
            {
                SmartDevice? smartDevice = _smartDeviceRepository.GetItemById(smartDeviceId);
                if(smartDevice == null)
                {
                    return;
                }

                foreach (var sensor in smartDevice.Sensors)
                {
                    if(sensor.Status == SensorStatus.Active)
                    {
                        throw new ArgumentException("Some of smart device sensors are still active.");
                    }
                }
                _smartDeviceRepository.DeleteItem(smartDeviceId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public List<SmartDeviceDto> GetAllSmartDevices()
        {
            var smartDevices = new List<SmartDevice>();
            var SmartDeviceDtos = new List<SmartDeviceDto>();
            try
            {
                smartDevices = _smartDeviceRepository.GetAllItems();
                foreach(var smartDevice in smartDevices)
                {
                    SmartDeviceDto smartDeviceDto = new SmartDeviceDto();
                    smartDeviceDto.SmartDeviceId = smartDevice.SmartDeviceId;
                    smartDeviceDto.NumberOfSensors = smartDevice.NumberOfSensors;
                    smartDeviceDto.LogisticCompanyId = 
                        smartDevice.LogisticCompany == null ? 0: smartDevice.LogisticCompany.LogisticCompanyId;
                    SmartDeviceDtos.Add(smartDeviceDto);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return SmartDeviceDtos;
        }

        public SmartDevice? GetSmartDeviceById(int smartDeviceId)
        {
            try
            {
                SmartDevice? smartDevice = _smartDeviceRepository.GetItemById(smartDeviceId);

                
                return smartDevice;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public SmartDevice UpdateSmartDevice(SmartDeviceDto smartDeviceDto)
        {
            SmartDevice smartDevice = new SmartDevice();
            smartDevice.SmartDeviceId = smartDeviceDto.SmartDeviceId;
            smartDevice.NumberOfSensors = smartDeviceDto.NumberOfSensors;
            smartDevice.LogisticCompany = _logisticCompanyService
                .GetLogisticCompanyById(smartDeviceDto.LogisticCompanyId);

            IsSmartDeviceValid(smartDevice);

            try
            {
                _smartDeviceRepository.UpdateItem(smartDevice);
                return smartDevice;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public List<SmartDeviceDto> GetSmartDevicesByLogisticCompanyId(int logisticCompanyId)
        {
            var smartDevices = new List<SmartDevice>();
            var SmartDeviceDtos = new List<SmartDeviceDto>();
            try
            {
                smartDevices = _smartDeviceRepository
                    .GetFilteredItems(s => s.LogisticCompany.LogisticCompanyId == logisticCompanyId);

                foreach (var smartDevice in smartDevices)
                {
                    SmartDeviceDto smartDeviceDto = new SmartDeviceDto();
                    smartDeviceDto.SmartDeviceId = smartDevice.SmartDeviceId;
                    smartDeviceDto.NumberOfSensors = smartDevice.NumberOfSensors;
                    smartDeviceDto.LogisticCompanyId = smartDevice.LogisticCompany.LogisticCompanyId;
                    SmartDeviceDtos.Add(smartDeviceDto);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return SmartDeviceDtos;
        }
    }
}
