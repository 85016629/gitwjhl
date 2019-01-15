using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fx.Application.Order.Interfaces;
using fx.Domain.OrderContext;
using fx.Order.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace fx.Order.WebApi.Controllers
{
    
    [Route("OderService/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private IOrderService _orderService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderService"></param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(IOrderService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Orders")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderViewModel order)
        {

            //_logger.Info("Test Nlog Info, Owner:{0}", order.Owner);
            //_logger.Error(new ArgumentNullException(nameof(OrderViewModel)).StackTrace);
            //_logger.Warn("Test Nlog Warn, Owner:{0}", order.Owner);
            //_logger.Error("Test Nlog Error, Owner:{0}", order.Owner);
            _logger.Debug(new ArgumentNullException(nameof(OrderViewModel)).StackTrace);

            //var newOrder = new fx.Domain.OrderContext.Order
            //{
            //    CreateTime = DateTime.Now,
            //    Id = "1111111",
            //    Owner = order.Owner,
            //    State = 0
            //};

            //var result = await _orderService.CreateOrder(newOrder);

            return Ok();
        }
    }
}