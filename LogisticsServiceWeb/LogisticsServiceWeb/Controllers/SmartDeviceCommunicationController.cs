using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
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
        [HttpGet("TestC")]
        public async Task<IActionResult> GetTestC()
        {
            //if(satellites > 0)
            //{
            //    _logger.LogCritical("satellites: " + satellites.ToString());
            //}
            //_logger.LogInformation("satellites: " + satellites.ToString());
            //_logger.LogInformation("latitude: " + latitude.ToString());
            //_logger.LogInformation("longitute: " + longitute.ToString());
            //_logger.LogInformation("DateTime: " + DateTime.Now.ToString());
            //_logger.LogInformation("satellites: {stellites} la: {la}, lo: {lo}, DateTime: {DateTime}", satellites.ToString(), latitude.ToString(), longitute.ToString(), DateTime.Now.ToString());
            //_logger.LogInformation("smartDeviceId: {smartDeviceId} la: {la}, lo: {lo}, DateTime: {DateTime}", smartDeviceId.ToString(), latitude.ToString(), longitute.ToString(), DateTime.Now.ToString());
            _logger.LogInformation("Connection check {DateTime}", DateTime.Now.ToString());
            return Ok();
        }

        [HttpGet("WriteCoordinates")]
        public async Task<IActionResult> WriteCoordinates(int smartDeviceId, double latitude, double longitute)
        {
            try
            {
                _logger.LogInformation("COORDINATES: smartDeviceId: {smartDeviceId} la: {la}, lo: {lo}, DateTime: {DateTime}\n\n", smartDeviceId.ToString(), latitude.ToString(), longitute.ToString(), DateTime.Now.ToString());
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
        public async Task<IActionResult> ActivateSensor(int sensorId, double latitude, double longitute)
        {
            try
            {
                _logger.LogInformation("ACTIVATE SENSOR: {sensorId} la: {la}, lo: {lo}, DateTime: {DateTime}\n\n", sensorId.ToString(), latitude.ToString(), longitute.ToString(), DateTime.Now.ToString());
                //return Ok();
                SmartDeviceCommunicationDto dto = new SmartDeviceCommunicationDto();
                dto.SensorId = sensorId;
                dto.Latitude = latitude;
                dto.Longitude = longitute;
                var result = _smartDeviceCommunicationService.ActivateSensor(dto);
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
