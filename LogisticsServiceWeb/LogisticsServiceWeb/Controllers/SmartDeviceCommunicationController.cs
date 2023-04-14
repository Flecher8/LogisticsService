using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartDeviceCommunicationController : ControllerBase
    {
        public readonly ISmartDeviceCommunicationService _smartDeviceCommunicationService;

        public readonly ILogger<SmartDeviceCommunicationController> _logger;

        public SmartDeviceCommunicationController(
            ISmartDeviceCommunicationService smartDeviceCommunicationService, 
            ILogger<SmartDeviceCommunicationController> logger)
        {
            _smartDeviceCommunicationService = smartDeviceCommunicationService;
            _logger = logger;
        }

        [HttpGet("WriteCoordinates")]
        public async Task<IActionResult> WriteCoordinates(int smartDeviceId, double latitude, double longitute)
        {
            try
            {
                SmartDeviceCommunicationDto dto = new SmartDeviceCommunicationDto();
                dto.SmartDeviceId = smartDeviceId;
                dto.Latitude = latitude;
                dto.Longitude = longitute;
                var result = _smartDeviceCommunicationService.WriteOrderTrackers(dto);

                if(result)
                {
                    return Ok(result);

                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("ActivateSensor")]
        public async Task<IActionResult> ActivateSensor(int sensorId)
        {
            try
            {
                var result = _smartDeviceCommunicationService.ActivateSensor(sensorId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

    }
}
