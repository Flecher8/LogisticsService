using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CargosController : ControllerBase
    {
        private readonly ICargoService _cargoService;

        private readonly ILogger<CargosController> _logger;

        public CargosController(
            ICargoService cargoService, 
            ILogger<CargosController> logger)
        {
            _cargoService = cargoService;
            _logger = logger;
        }

        [HttpGet("id/{cargoId}")]
        public async Task<IActionResult> GetCargoById(int cargoId, string cargoSizeType = "cm", string cargoWeightType = "kg")
        {
            try
            {
                var result = _cargoService.GetCargoById(cargoId, cargoSizeType, cargoWeightType);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCargos(string cargoSizeType = "cm", string cargoWeightType = "kg")
        {
            try
            {
                var result = _cargoService.GetAllCargos(cargoSizeType, cargoWeightType);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCargo(CargoDto cargoDto)
        {
            try
            {
                var result = _cargoService.CreateCargo(cargoDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutCargo(CargoDto cargoDto)
        {
            try
            {
                var result = _cargoService.UpdateCargo(cargoDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id/{cargoId}")]
        public async Task<IActionResult> DeleteCargo(int cargoId)
        {
            try
            {
                _cargoService.DeleteCargo(cargoId);
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
