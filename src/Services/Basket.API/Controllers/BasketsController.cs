using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories.Interfaces;
using Basket.API.Services.Interfaces;
using EventBus.Messages.IntegrationEvents.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Shared.DTOs.Basket;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEnpoint;
        private readonly IMapper _mapper;
        private readonly StockItemGrpcService _stockItemGrpcService;

        public BasketsController(IBasketRepository repository, IPublishEndpoint publishEnpoint, IMapper mapper, StockItemGrpcService stockItemGrpcService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publishEnpoint = publishEnpoint ?? throw new ArgumentNullException(nameof(publishEnpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _stockItemGrpcService = stockItemGrpcService ?? throw new ArgumentNullException(nameof(stockItemGrpcService));
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(CartDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartDto>> GetBasketByUserName([Required] string username)
        {
            var cartEntity = await _repository.GetBasketByUserName(username);
            var result = _mapper.Map<CartDto>(cartEntity) ?? new CartDto();
            return Ok(result);
        }

        [HttpPost(Name = "UpdateBasket")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartDto>> UpdateBasket([FromBody] CartDto cart)
        {
            // Communicate with Inventory.Grpc and check quantity availabel of products
            foreach (var item in cart.Items)
            {
                var stock = await _stockItemGrpcService.GetStock(item.ItemNo);
                item.SetAvailableQuantity(stock.Quantity);
            }


            var options = new DistributedCacheEntryOptions()
                                .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(12)) //Cache exists in time
                                .SetSlidingExpiration(TimeSpan.FromDays(10));    //Track how long there is no activity
            var cartEntity = _mapper.Map<Cart>(cart);
            var updateCart = await _repository.UpdateBasket(cartEntity, options);
            var result = _mapper.Map<CartDto>(updateCart);
            return Ok(result);
        }

        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasket([Required] string username)
        {
            var result = await _repository.DeleteBasketFromUserName(username);
            return NoContent();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _repository.GetBasketByUserName(basketCheckout.UserName);
            if (basket == null) return NotFound();

            //publish checkout event to Eventbus Message
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEnpoint.Publish(eventMessage);
            //remove basket
            await _repository.DeleteBasketFromUserName(basketCheckout.UserName);
            return Accepted();
        }
    }
}
