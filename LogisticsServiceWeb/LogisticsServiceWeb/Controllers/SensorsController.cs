using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensoreService;

        private readonly ILogger<SensorsController> _logger;

        public SensorsController(ISensorService sensoreService, ILogger<SensorsController> logger)
        {
            _sensoreService = sensoreService;
            _logger = logger;
        }

        [HttpGet("id/{sensorId}")]
        public async Task<IActionResult> GetSensorById(int sensorId)
        {
            try
            {
                var result = _sensoreService.GetSensorById(sensorId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSensors()
        {
            try
            {
                var result = _sensoreService.GetAllSensors();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("smartDeviceId/{smartDeviceId}")]
        public async Task<IActionResult> GetSensorsBySmartDeviceId(int smartDeviceId)
        {
            try
            {
                var result = _sensoreService.GetAllSensorsBySmartDeviceId(smartDeviceId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostSensor(SensorDto sensorDto)
        {
            try
            {
                _sensoreService.CreateSensor(sensorDto);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutSensor(SensorDto sensor)
        {
            try
            {
                _sensoreService.UpdateSensor(sensor);
                return Ok(sensor);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id/{sensorId}")]
        public async Task<IActionResult> DeleteSensor(int sensorId)
        {
            try
            {
                _sensoreService.DeleteSensor(sensorId);
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
