using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CancelledOrdersController : ControllerBase
    {
        private readonly ICancelledOrderService _cancelledOrderService;

        private readonly ILogger<CancelledOrdersController> _logger;

        public CancelledOrdersController(
            ICancelledOrderService cancelledOrderService, 
            ILogger<CancelledOrdersController> logger)
        {
            _cancelledOrderService = cancelledOrderService;
            _logger = logger;
        }

        [HttpGet("id/{cancelledOrderId}")]
        public async Task<IActionResult> GetCancelledOrderById(int cancelledOrderId)
        {
            try
            {
                var result = _cancelledOrderService.GetCancelledOrderById(cancelledOrderId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("orderId/{orderId}")]
        public async Task<IActionResult> GetCancelledOrderByOrderId(int orderId)
        {
            try
            {
                var result = _cancelledOrderService.GetCancelledOrderByOrderId(orderId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCancelledOrders()
        {
            try
            {
                var result = _cancelledOrderService.GetAllCancelledOrders();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCancelledOrder(CancelledOrderDto cancelledOrderDto)
        {
            try
            {
                var result = _cancelledOrderService.CreateCancelledOrder(cancelledOrderDto);
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
