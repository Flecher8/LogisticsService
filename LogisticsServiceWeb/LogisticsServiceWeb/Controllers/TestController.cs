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
        [HttpGet]
        public async Task<IActionResult> Get(SmartDevice smartDevice)
        {
            return Ok(smartDevice);
        }
    }
}
