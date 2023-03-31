using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        public readonly IRateService _rateService;

        public readonly ILogger<RatesController> _logger;

        public RatesController(IRateService rateService, ILogger<RatesController> logger)
        {
            _rateService = rateService;
            _logger = logger;
        }

        [HttpGet("id/{rateId}")]
        public async Task<IActionResult> GetRateById(int rateId)
        {
            try
            {
                var result = _rateService.GetRateById(rateId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("logisticCompanyId/{logisticCompanyId}")]
        public async Task<IActionResult> GetRateByLogisticCompanyId(int logisticCompanyId)
        {
            try
            {
                var result = _rateService.GetRateByLogisticCompanyId(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutRate(Rate rate)
        {
            try
            {
                _rateService.UpdateRate(rate);
                return Ok(rate);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
