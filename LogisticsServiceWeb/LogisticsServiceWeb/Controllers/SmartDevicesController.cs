using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartDevicesController : ControllerBase
    {
        private readonly ISmartDeviceService _smartDeviceService;

        private readonly ILogger<SubscriptionTypesController> _logger;

        public SmartDevicesController(ISmartDeviceService smartDeviceService, ILogger<SubscriptionTypesController> logger)
        {
            _smartDeviceService = smartDeviceService;
            _logger = logger;
        }

        [HttpGet("id/{smartDeviceId}")]
        public async Task<IActionResult> GetSmartDeviceById(int smartDeviceId)
        {
            try
            {
                var result = _smartDeviceService.GetSmartDeviceById(smartDeviceId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSmartDevices()
        {
            try
            {
                var result = _smartDeviceService.GetAllSmartDevices();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("logisticCompanyId/{logisticCompanyId}")]
        public async Task<IActionResult> GetSmartDevicesByLogisticCompanyId(int logisticCompanyId)
        {
            try
            {
                var result = _smartDeviceService
                    .GetSmartDevicesByLogisticCompanyId(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostSmartDevice(SmartDeviceDto smartDevice)
        {
            try
            {
                _smartDeviceService.CreateSmartDevice(smartDevice);
                return Ok(smartDevice);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutSmartDevice(SmartDevice smartDevice)
        {
            try
            {
                _smartDeviceService.UpdateSmartDevice(smartDevice);
                return Ok(smartDevice);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id/{smartDeviceId}")]
        public async Task<IActionResult> DeleteSmartDevice(int smartDeviceId)
        {
            try
            {
                _smartDeviceService.DeleteSmartDevice(smartDeviceId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
