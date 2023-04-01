using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeocodeController : ControllerBase
    {
        public readonly IGoogleMapsApiGeocodeService _googleMapsApiGeocodeService;

        public readonly ILogger<GeocodeController> _logger;

        public GeocodeController(
            IGoogleMapsApiGeocodeService googleMapsApiGeocodeService, 
            ILogger<GeocodeController> logger)
        {
            _googleMapsApiGeocodeService = googleMapsApiGeocodeService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetAddressName(string language, Address address)
        {
            try
            {
                var result = _googleMapsApiGeocodeService.GetAddressAsync(address, language).GetAwaiter().GetResult();
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
