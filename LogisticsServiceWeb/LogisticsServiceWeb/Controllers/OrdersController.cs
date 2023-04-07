using LogisticsService.BLL.Interfaces;
using LogisticsService.BLL.Services;
using LogisticsService.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public readonly IOrderService _orderService;

        public readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var result = _orderService.GetAllOrders();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("logisticCompaniesDriverId/{logisticCompaniesDriverId}")]
        public async Task<IActionResult> GetAllOrdersByLogisticCompaniesDriver(int logisticCompaniesDriverId)
        {
            try
            {
                var result = _orderService
                    .GetAllOrdersByLogisticCompaniesDriverId(logisticCompaniesDriverId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("logisticCompanyId/{logisticCompanyId}")]
        public async Task<IActionResult> GetAllOrdersByLogisticCompany(int logisticCompanyId)
        {
            try
            {
                var result = _orderService
                    .GetAllOrdersByLogisticCompanyId(logisticCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("privateCompanyId/{privateCompanyId}")]
        public async Task<IActionResult> GetAllOrdersByPrivateCompany(int privateCompanyId)
        {
            try
            {
                var result = _orderService
                    .GetAllOrdersByPrivateCompanyId(privateCompanyId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var result = _orderService
                    .GetOrderById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderDto orderDto)
        {
            try
            {
                var result = await _orderService.CreateOrder(orderDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutOrder(OrderDto orderDto)
        {
            try
            {
                var result = _orderService.UpdateOrder(orderDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateOrderStatus/id/{id}")]
        public async Task<IActionResult> PutOrderStatus(int id)
        {
            try
            {
                _orderService.UpdateOrderStatus(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        //[HttpDelete("id/{id}")]
        //public async Task<IActionResult> DeleteOrder(int id)
        //{
        //    try
        //    {
        //        _orderService.DeleteOrder(id);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
