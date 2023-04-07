using LogisticsService.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        public readonly IAnalyticsService _analyticsService;
        public readonly ILogger<AnalyticsController> _logger;

        public AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger)
        {
            _analyticsService = analyticsService;
            _logger = logger;
        }

        [HttpGet("AverageDeliveryTimeByPrivateCompany")]
        public async Task<IActionResult> GetAverageDeliveryTimeByPrivateCompany(int privateCompanyId)
        {
            try
            {
                var result = _analyticsService.GetAverageDeliveryTimeByPrivateCompany(privateCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("AverageDeliveryTimeByLogisticCompany")]
        public async Task<IActionResult> GetAverageDeliveryTimeByLogisticCompany(int logisticCompanyId)
        {
            try
            {
                var result = _analyticsService.GetAverageDeliveryTimeByLogisticCompany(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("AverageDeliveryPathLengthByPrivateCompany")]
        public async Task<IActionResult> GetAverageDeliveryPathLengthByPrivateCompany(int privateCompanyId, string metric = "km")
        {
            try
            {
                var result = _analyticsService.GetAverageDeliveryPathLengthByPrivateCompany(privateCompanyId, metric);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("AverageDeliveryPathLengthByLogisticCompany")]
        public async Task<IActionResult> GetAverageDeliveryPathLengthByLogisticCompany(int logisticCompanyId, string metric = "km")
        {
            try
            {
                var result = _analyticsService.GetAverageDeliveryPathLengthByLogisticCompany(logisticCompanyId, metric);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }
       
        [HttpGet("NumberOfDeliveredOrdersByPrivateCompany")]
        public async Task<IActionResult> GetNumberOfDeliveredOrdersByPrivateCompany(int privateCompanyId)
        {
            try
            {
                var result = _analyticsService.GetNumberOfDeliveredOrdersByPrivateCompany(privateCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("NumberOfDeliveredOrdersByLogisticCompany")]
        public async Task<IActionResult> GetNumberOfDeliveredOrdersByLogisticCompany(int logisticCompanyId)
        {
            try
            {
                var result = _analyticsService.GetNumberOfDeliveredOrdersByLogisticCompany(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("NumberOfNotDeliveredOrdersByPrivateCompany")]
        public async Task<IActionResult> GetNumberOfNotDeliveredOrdersByPrivateCompany(int privateCompanyId)
        {
            try
            {
                var result = _analyticsService.GetNumberOfNotDeliveredOrdersByPrivateCompany(privateCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("NumberOfNotDeliveredOrdersByLogisticCompany")]
        public async Task<IActionResult> GetNumberOfNotDeliveredOrdersByLogisticCompany(int logisticCompanyId)
        {
            try
            {
                var result = _analyticsService.GetNumberOfNotDeliveredOrdersByLogisticCompany(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("AverageOrderPriceByPrivateCompany")]
        public async Task<IActionResult> GetAverageOrderPriceByPrivateCompany(int privateCompanyId)
        {
            try
            {
                var result = _analyticsService.GetAverageOrderPriceByPrivateCompany(privateCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("AverageOrderPriceByLogisticCompany")]
        public async Task<IActionResult> GetAverageOrderPriceByLogisticCompany(int logisticCompanyId)
        {
            try
            {
                var result = _analyticsService.GetAverageOrderPriceByLogisticCompany(logisticCompanyId);
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
