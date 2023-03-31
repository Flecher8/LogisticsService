using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
