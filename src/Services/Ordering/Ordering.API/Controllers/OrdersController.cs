using Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Commands.CreateOrder;
using Ordering.Application.Features.V1.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.V1.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;
using Shared.SeedWork;
using Shared.Services.Email;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISmtpEmailService _smtpEmailService;

        public OrdersController(IMediator mediator, ISmtpEmailService smtpEmailService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _smtpEmailService = smtpEmailService ?? throw new ArgumentNullException(nameof(smtpEmailService));
        }

        public static class RouteNames { 
            public const string GetOrders = nameof(GetOrders);
            public const string CreateOrder = nameof(CreateOrder);
            public const string DeleteOrder = nameof(DeleteOrder);
            public const string UpdateOrder = nameof(UpdateOrder);
        }

        #region CRUD Order
        [HttpGet("username", Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string userName)
        {
            var query = new GetOrdersQuery(userName);
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpPost(Name = RouteNames.CreateOrder)]
        [ProducesResponseType(typeof(ApiResult<long>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResult<long>>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = RouteNames.UpdateOrder)]
        [ProducesResponseType(typeof(ApiResult<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResult<OrderDto>>> UpdateOrder([Required] long id, [FromBody] UpdateOrderCommand command)
        {
            command.SetId(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:long}",Name = RouteNames.DeleteOrder)]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<NoContentResult>> DeleteOrder([Required] long id)
        {
            DeleteOrderCommand command = new DeleteOrderCommand(id);
            var result = await _mediator.Send(command);
            return NoContent();
        }

        #endregion
        // Test Send Email
        [HttpGet]   
        public async Task<IActionResult> TestEmail()
        {
            var message = new MailRequest
            {
                Body = "Test",
                Subject = "Test",
                ToAddresses = new List<string> { "nguyentienanh1899@gmail.com" }
            };

            await _smtpEmailService.SendEmailAsync(message);
            return Ok();
        }
    }
}
