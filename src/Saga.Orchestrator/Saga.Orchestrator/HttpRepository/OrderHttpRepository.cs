﻿using Infrastructure.Extensions;
using Saga.Orchestrator.HttpRepository.Interfaces;
using Shared.DTOs.Order;
using Shared.SeedWork;

namespace Saga.Orchestrator.HttpRepository
{
    public class OrderHttpRepository : IOrderHttpRepository
    {
        private readonly HttpClient _httpClient;
        public OrderHttpRepository(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }
        public async Task<long> CreateOrder(CreateOrderDto order)
        {
            var response = await _httpClient.PostAsJsonAsync("orders", order);
            if(!response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                return -1;
            }
            var orderId = await response.ReadContentAs<ApiSuccessResult<long>>();
            return orderId.Data;
        }

        public async Task<bool> DeleteOrder(long id)
        {
            var responseMessage = await _httpClient.DeleteAsync($"orders/{id.ToString()}");
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderByDocumentNo(string documentNo)
        {
            var response = await _httpClient.DeleteAsync($"document-no/{documentNo}");
            return response.IsSuccessStatusCode;
        }

        public async Task<OrderDto> GetOrder(long id)
        {
            var order = await _httpClient.GetFromJsonAsync<ApiSuccessResult<OrderDto>>($"orders/{id.ToString()}");
            return order.Data;
        }
    }
}
