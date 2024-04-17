using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _sericalizeService;
        private readonly ILogger _logger;
        public BasketRepository(IDistributedCache redisCacheService, ISerializeService sericalizeService, ILogger logger)
        {
            _redisCacheService = redisCacheService;
            _sericalizeService = sericalizeService;
            _logger = logger;
        }
        public async Task<bool> DeleteBasketFromUserName(string userName)
        {
            try
            {
                await _redisCacheService.RemoveAsync(userName);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error("Error DeleteBasketFromUserName:" + e.Message);
                throw;
            }
        }

        public async Task<Cart?> GetBasketByUserName(string userName)
        {
            _logger.Information($"Start GetBasketByUserName {userName}");
            var basket = await _redisCacheService.GetStringAsync(userName);
            _logger.Information($"Complete GetBasketByUserName {userName}");
            return string.IsNullOrEmpty(basket) ? null : _sericalizeService.Deserialize<Cart>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {
            _logger.Information($"Start UpdateBasket {cart.UserName}");
            if (options != null)
            {
                await _redisCacheService.SetStringAsync(cart.UserName, _sericalizeService.Serialize(cart), options);
            }
            else
            {
                await _redisCacheService.SetStringAsync(cart.UserName, _sericalizeService.Serialize(cart));
            }
            _logger.Information($"Complete UpdateBasket {cart.UserName}");
            return await GetBasketByUserName(cart.UserName);
        }
    }
}
