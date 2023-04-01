using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SystemAdmin")]
    public class SystemAdminsController : ControllerBase
    {
        private readonly ISystemAdminService _systemAdminService;

        private readonly ILogger<SystemAdminsController> _logger;

        public SystemAdminsController(ISystemAdminService systemAdminService, ILogger<SystemAdminsController> logger)
        {
            _systemAdminService = systemAdminService;
            _logger = logger;
        }

        [HttpGet("{systemAdminId}")]
        public async Task<IActionResult> GetSystemAdminById(int systemAdminId)
        {
            try
            {
                SystemAdmin? systemAdmin = _systemAdminService.GetSystemAdminById(systemAdminId);

                if (systemAdmin == null)
                {
                    return NotFound($"No subscription type with index {systemAdminId}");
                }
                return Ok(systemAdmin);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSystemAdmins()
        {
            try
            {
                var systemAdmins = _systemAdminService.GetAllSystemAdmins();
                return Ok(systemAdmins);
            }
            catch (Exception e)
            {
                _logger?.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostSystemAdmin(PersonDto person)
        {
            try
            {
                _systemAdminService.CreateSystemAdmin(person);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{systemAdminId}")]
        public async Task<IActionResult> DeleteSystemAdmin(int systemAdminId)
        {
            try
            {
                _systemAdminService.DeleteSystemAdmin(systemAdminId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutSystemAdmin(SystemAdmin systemAdmin)
        {
            try
            {
                _systemAdminService.UpdateSystemAdmin(systemAdmin);
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
