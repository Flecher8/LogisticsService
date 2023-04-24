using LogisticsService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        public readonly IPaymentService _paymentService;

        public readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("orderId/{orderId}")]
        public async Task<IActionResult> PostOrderPayment(int orderId)
        {
            try
            {
                var result = _paymentService.OrderPayment(orderId);
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
