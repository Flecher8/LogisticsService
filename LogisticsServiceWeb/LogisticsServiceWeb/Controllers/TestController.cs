using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public readonly IGoogleMapsApiGeocodeService _googleMapsApiGeocodeService;

        public readonly ILogger<TestController> _logger;

        public TestController(
            IGoogleMapsApiGeocodeService googleMapsApiGeocodeService, 
            ILogger<TestController> logger)
        {
            _googleMapsApiGeocodeService = googleMapsApiGeocodeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
