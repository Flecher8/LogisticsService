using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        public readonly ISubscriptionService _subscriptionService;

        public readonly ILogger<SubscriptionsController> _logger;

        public SubscriptionsController(
            ISubscriptionService subscriptionService, 
            ILogger<SubscriptionsController> logger)
        {
            _subscriptionService = subscriptionService;
            _logger = logger;
        }

        [HttpGet("id/{subscriptionId}")]
        public async Task<IActionResult> GetSubscriptionById(int subscriptionId)
        {
            try
            {
                var result = _subscriptionService.GetSubscriptionById(subscriptionId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            try
            {
                var result = _subscriptionService.GetAllSubscriptions();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("logisticCompanyId/{logisticComanyId}")]
        public async Task<IActionResult> GetAllSubscriptions(int logisticComanyId)
        {
            try
            {
                var result = _subscriptionService.GetSubscriptionByLogisticCompanyId(logisticComanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpGet("HasSubscription/{logisticCompanyId}")]
        public async Task<IActionResult> GetHasSubscription(int logisticCompanyId)
        {
            try
            {
                var result = _subscriptionService.HasSubscription(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> PutSubscription(SubscriptionDto subscriptionDto)
        {
            try
            {
                var result = _subscriptionService.UpdateSubscription(subscriptionDto);
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
