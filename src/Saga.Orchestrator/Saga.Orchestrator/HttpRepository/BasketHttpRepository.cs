using Saga.Orchestrator.HttpRepository.Interfaces;
using Shared.DTOs.Basket;
using System.Net.Http.Json;

namespace Saga.Orchestrator.HttpRepository
{
    public class BasketHttpRepository : IBasketHttpRepository
    {
        private readonly HttpClient _httpClient;
        public BasketHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> DeleteBasket(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDto> GetBasket(string username)
        {
            var cart = await _httpClient.GetFromJsonAsync<CartDto>($"baskets/{username}");
            if(cart == null || !cart.Items.Any()) { return null; }
            return cart;
        }
    }
}
