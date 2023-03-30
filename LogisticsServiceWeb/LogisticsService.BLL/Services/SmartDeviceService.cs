using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
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

        public void CreateSmartDevice(SmartDeviceDto smartDeviceDto)
        {
            SmartDevice smartDevice = new SmartDevice();
            smartDevice.NumberOfSensors = smartDeviceDto.NumberOfSensors;
            smartDevice.LogisticCompany = _logisticCompanyService
                .GetLogisticCompanyById(smartDeviceDto.LogisticCompanyId);

            IsSmartDeviceValid(smartDevice);

            try
            {
                _smartDeviceRepository.InsertItem(smartDevice);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
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
                _smartDeviceRepository.DeleteItem(smartDeviceId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public List<SmartDevice> GetAllSmartDevices()
        {
            var smartDevices = new List<SmartDevice>();
            try
            {
                smartDevices = _smartDeviceRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return smartDevices;
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

        public void UpdateSmartDevice(SmartDevice smartDevice)
        {
            try
            {
                _smartDeviceRepository.UpdateItem(smartDevice);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
