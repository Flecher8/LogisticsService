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
    public class LogisticCompaniesDriversController : ControllerBase
    {
        public readonly ILogisticCompaniesDriverService _logisticCompaniesDriverService;

        public readonly ILogger<LogisticCompaniesDriversController> _logger;

        public LogisticCompaniesDriversController(
            ILogisticCompaniesDriverService logisticCompaniesDriverService, 
            ILogger<LogisticCompaniesDriversController> logger)
        {
            _logisticCompaniesDriverService = logisticCompaniesDriverService;
            _logger = logger;
        }

        [HttpGet("id/{logisticCompaniesDriverId}")]
        public async Task<IActionResult> GetLogisticCompaniesDriverById(int logisticCompaniesDriverId)
        {
            try
            {
                var result = _logisticCompaniesDriverService
                    .GetLogisticCompaniesDriverById(logisticCompaniesDriverId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogisticCompaniesDrivers()
        {
            try
            {
                var result = _logisticCompaniesDriverService
                    .GetAllLogisticCompaniesDrivers();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("logisticCompanyId/{logisticCompanyId}")]
        public async Task<IActionResult> GetLogisticCompaniesDriversByLogisticCompanyId(int logisticCompanyId)
        {
            try
            {
                var result = _logisticCompaniesDriverService
                    .GetAllLogisticCompaniesDriversByLogisticCompanyId(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostLogisticCompaniesDriver(PersonDto person)
        {
            try
            {
                _logisticCompaniesDriverService
                    .CreateLogisticCompaniesDriver(person);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        //[HttpPut]
        //public async Task<IActionResult> PutLogisticCompaniesDriver(
        //    LogisticCompaniesDriver logisticCompaniesDriver)
        //{
        //    try
        //    {
        //        _logisticCompaniesDriverService
        //            .UpdateLogisticCompaniesDriver(logisticCompaniesDriver);

        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpDelete("id/{logisticCompaniesDriverId}")]
        public async Task<IActionResult> DeleteLogisticCompaniesDriver(
            int logisticCompaniesDriverId)
        {
            try
            {
                _logisticCompaniesDriverService
                    .DeleteLogisticCompaniesDriver(logisticCompaniesDriverId);

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
