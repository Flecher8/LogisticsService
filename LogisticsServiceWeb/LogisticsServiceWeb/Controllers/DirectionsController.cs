using LogisticsService.BLL.Helpers.GoogleMapsApi;
using LogisticsService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DirectionsController : ControllerBase
    {
        public readonly IGoogleMapsApiDirectionsService _googleMapsApiDirectionsService;
        public readonly ILogger<DirectionsController> _logger;

        public DirectionsController(
            IGoogleMapsApiDirectionsService googleMapsApiDirectionsService, 
            ILogger<DirectionsController> logger)
        {
            _googleMapsApiDirectionsService = googleMapsApiDirectionsService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetDirectionInfo(GoogleMapsApiDirectionsParam googleMapsApiDirectionsParam)
        {
            try
            {
                var result = await _googleMapsApiDirectionsService
                    .GetGoogleMapsDirectionInfo(googleMapsApiDirectionsParam);
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
