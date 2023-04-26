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
    public class LogisticCompaniesAdministratorsController : ControllerBase
    {
        public readonly ILogisticCompaniesAdministratorService _logisticCompaniesAdministratorService;

        public readonly ILogger<LogisticCompaniesAdministratorsController> _logger;

        public LogisticCompaniesAdministratorsController(
            ILogisticCompaniesAdministratorService logisticCompaniesAdministratorService, 
            ILogger<LogisticCompaniesAdministratorsController> logger)
        {
            _logisticCompaniesAdministratorService = logisticCompaniesAdministratorService;
            _logger = logger;
        }

        [HttpGet("id/{logisticCompaniesAdministratorId}")]
        public async Task<IActionResult> GetLogisticCompaniesAdministratorById(int logisticCompaniesAdministratorId)
        {
            try
            {
                var result = _logisticCompaniesAdministratorService
                    .GetLogisticCompaniesAdministratorById(logisticCompaniesAdministratorId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("email/{logisticCompaniesAdministratorEmail}")]
        public async Task<IActionResult> GetLogisticCompaniesAdministratorByEmail(string logisticCompaniesAdministratorEmail)
        {
            try
            {
                var result = _logisticCompaniesAdministratorService
                    .GetLogisticCompaniesAdministratorByEmail(logisticCompaniesAdministratorEmail);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogisticCompaniesAdministrators()
        {
            try
            {
                var result = _logisticCompaniesAdministratorService
                    .GetAllLogisticCompaniesAdministrators();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("logisticCompanyId/{logisticCompanyId}")]
        public async Task<IActionResult> GetAllLogisticCompaniesAdministratorsByLogisticCompanyId(int logisticCompanyId)
        {
            try
            {
                var result = _logisticCompaniesAdministratorService
                    .GetAllLogisticCompaniesAdministratorsByLogisticCompanyId(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostLogisticCompaniesAdministrator(PersonDto person)
        {
            try
            {
                _logger.LogInformation("Ok");
                var result = _logisticCompaniesAdministratorService
                    .CreateLogisticCompaniesAdministrator(person);
                _logger.LogInformation("Ok2");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutLogisticCompaniesAdministrator(
            PersonDto logisticCompaniesAdministrator)
        {
            try
            {

                var result = _logisticCompaniesAdministratorService
                    .UpdateLogisticCompaniesAdministrator(logisticCompaniesAdministrator);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id/{logisticCompaniesAdministratorId}")]
        public async Task<IActionResult> DeleteLogisticCompaniesAdministrator(
            int logisticCompaniesAdministratorId)
        {
            try
            {
                _logisticCompaniesAdministratorService
                    .DeleteLogisticCompaniesAdministrator(logisticCompaniesAdministratorId);

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
