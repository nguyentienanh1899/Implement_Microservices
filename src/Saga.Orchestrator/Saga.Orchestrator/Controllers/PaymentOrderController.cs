using Contracts.Sagas.OrderManager;
using Microsoft.AspNetCore.Mvc;
using Saga.Orchestrator.OrderManager;
using Saga.Orchestrator.Services.Interfaces;
using Shared.DTOs.Basket;
using System.ComponentModel.DataAnnotations;

namespace Saga.Orchestrator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentOrderController : ControllerBase
    {
        private readonly ISagaOrderManager<PaymentOrderDto, OrderResponse> _sagaOrderManager;
        public PaymentOrderController(ISagaOrderManager<PaymentOrderDto, OrderResponse> sagaOrderManager)
        {
            _sagaOrderManager = sagaOrderManager;
        }

        [HttpPost]
        [Route("{username}")]
        public OrderResponse PaymentOrder([Required] string username, [FromBody] PaymentOrderDto model)
        {
            model.UserName = username;
            var result = _sagaOrderManager.CreateOrder(model);
            return result;
        }
        
    }
}
