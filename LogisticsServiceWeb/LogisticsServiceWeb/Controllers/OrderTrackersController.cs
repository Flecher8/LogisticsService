using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using LogisticsService.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderTrackersController : ControllerBase
    {
        private readonly IOrderTrackerService _orderTrackerService;

        private readonly ILogger<OrderTrackersController> _logger;

        public OrderTrackersController(
            IOrderTrackerService orderTrackerService, 
            ILogger<OrderTrackersController> logger)
        {
            _orderTrackerService = orderTrackerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderTrackers()
        {
            try
            {
                var result = _orderTrackerService.GetAllOrderTrackers();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("orderId/{orderId}")]
        public async Task<IActionResult> GetOrderTrackersByOrderId(int orderId)
        {
            try
            {
                var result = _orderTrackerService.GetOrderTrackersByOrderId(orderId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("CurrentOrderTracker/orderId/{orderId}")]
        public async Task<IActionResult> GetCurrentOrderTrackersByOrderId(int orderId)
        {
            try
            {
                var result = _orderTrackerService.GetCurrentOrderTrackerByOrderId(orderId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostOrderTracker(OrderTrackerDto orderTrackerDto)
        {
            try
            {
                _orderTrackerService.CreateOrderTracker(orderTrackerDto);
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
