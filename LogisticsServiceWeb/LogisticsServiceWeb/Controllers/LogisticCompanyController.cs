﻿using LogisticsService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogisticCompanyController : ControllerBase
    {
        public readonly ILogisticCompanyService _logisticCompanyService;

        public readonly ILogger<LogisticCompanyController> _logger;

        public LogisticCompanyController(
            ILogisticCompanyService logisticCompanyService, 
            ILogger<LogisticCompanyController> logger)
        {
            _logisticCompanyService = logisticCompanyService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> GetAllLogisticCompanies()
        {
            try
            {
                var result = _logisticCompanyService.GetAllLogisticCompanies();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/id/{logisticCompanyId}")]
        public async Task<IActionResult> GetLogisticCompanyById(int logisticCompanyId)
        {
            try
            {
                var result = _logisticCompanyService.GetLogisticCompanyById(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/email/{logisticCompanyEmail}")]
        public async Task<IActionResult> GetLogisticCompaniesByEmail(string logisticCompanyEmail)
        {
            try
            {
                var result = _logisticCompanyService.GetLogisticCompanyByEmail(logisticCompanyEmail);
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
