using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly ISubscriptionTypeService _subscriptionTypeService;

        private readonly ILogger<SubscriptionTypeController> _logger;
        
        public SubscriptionTypeController(ISubscriptionTypeService subscriptionTypeService, 
            ILogger<SubscriptionTypeController> logger)
        {
            _subscriptionTypeService = subscriptionTypeService;
            _logger = logger;
        }

        [HttpGet("{subscriptionTypeId}")]
        public async Task<IActionResult> GetSubscriptionTypeById(int subscriptionTypeId)
        {
            try
            {
                SubscriptionType? subscriptionType = _subscriptionTypeService.GetSubscriptionTypeById(subscriptionTypeId);

                if(subscriptionType == null)
                {
                    return NotFound($"No subscription type with index {subscriptionTypeId}");
                }
                return Ok(subscriptionType);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e.Message);
                return ValidationProblem(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubscriptionTypes()
        {
            try
            {
                var subscriptionTypes = _subscriptionTypeService.GetAllSubscriptionTypes();
                return Ok(subscriptionTypes);
            }
            catch (Exception e)
            {
                _logger?.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostSubscriptionType(SubscriptionType subscriptionType)
        {
            try
            {
                _subscriptionTypeService.CreateSubscriptionType(subscriptionType);
                return Ok(subscriptionType);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{subscriptionTypeId}")]
        public async Task<IActionResult> DeleteSubscriptionType(int subscriptionTypeId)
        {
            try
            {
                _subscriptionTypeService.DeleteSubscriptionType(subscriptionTypeId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutSubscriptionType(SubscriptionType subscriptionType)
        {
            try
            {
                _subscriptionTypeService.UpdateSubscriptionType(subscriptionType);
                return Ok(subscriptionType);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
