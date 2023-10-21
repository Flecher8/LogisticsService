using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrivateCompaniesController : ControllerBase
    {
        public readonly IPrivateCompanyService _privateCompanyService;

        public readonly ILogger<LogisticCompaniesController> _logger;

        public PrivateCompaniesController(
            IPrivateCompanyService privateCompanyService, 
            ILogger<LogisticCompaniesController> logger)
        {
            _privateCompanyService = privateCompanyService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> GetAllPrivateCompanies()
        {
            try
            {
                var result = _privateCompanyService.GetAllPrivateCompanies();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("id/{privateCompanyId}")]
        public async Task<IActionResult> GetPrivateCompanyById(int privateCompanyId)
        {
            try
            {
                var result = _privateCompanyService.GetPrivateCompanyById(privateCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("email/{privateCompanyEmail}")]
        public async Task<IActionResult> GetPrivateCompaniesByEmail(string privateCompanyEmail)
        {
            try
            {
                var result = _privateCompanyService.GetPrivateCompanyByEmail(privateCompanyEmail);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
