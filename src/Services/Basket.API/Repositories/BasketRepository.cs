using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Basket.API.Services;
using Basket.API.Services.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Shared.DTOs.ScheduledJobs;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _sericalizeService;
        private readonly ILogger _logger;
        private readonly BackgroundScheduledJobHttpService _backgroundScheduledJobHttpService;
        private readonly IEmailTemplateService _emailTemplateService;
        public BasketRepository(BackgroundScheduledJobHttpService backgroundScheduledJobHttpService, IDistributedCache redisCacheService, ISerializeService sericalizeService, ILogger logger, IEmailTemplateService emailTemplateService)
        {
            _redisCacheService = redisCacheService;
            _emailTemplateService = emailTemplateService;
            _sericalizeService = sericalizeService;
            _backgroundScheduledJobHttpService = backgroundScheduledJobHttpService;
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

            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return await GetBasketByUserName(cart.UserName);
        }

        private async Task TriggerSendEmailReminderPaymentOrder(Cart cart)
        {
            var emailTemplate = _emailTemplateService.GenerateReminderPaymentOrderEmail(cart.UserName);
            var model = new ReminderPaymentOrderDto(cart.EmailAddress, "Payment Due for Your Recent Order", emailTemplate, DateTimeOffset.UtcNow.AddSeconds(30));

        }
    }
}
