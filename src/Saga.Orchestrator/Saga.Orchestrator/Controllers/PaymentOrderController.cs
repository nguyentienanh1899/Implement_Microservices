using Microsoft.AspNetCore.Mvc;
using Saga.Orchestrator.Services.Interfaces;
using Shared.DTOs.Basket;
using System.ComponentModel.DataAnnotations;

namespace Saga.Orchestrator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentOrderController : ControllerBase
    {
        private readonly IPaymentOrderService _paymentOrderService;
        public PaymentOrderController(IPaymentOrderService paymentOrderService)
        {
            _paymentOrderService = paymentOrderService;
        }

        [HttpPost]
        [Route("{username}")]
        public async Task<IActionResult> PaymentOrder([Required] string username, [FromBody] PaymentOrderDto model)
        {
            var result = await _paymentOrderService.PaymentOrderProcess(username, model);
            return Accepted(result);
        }
        
    }
}
