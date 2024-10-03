using Infrastructure.Extensions;
using Saga.Orchestrator.HttpRepository.Interfaces;
using Shared.DTOs.Inventory;

namespace Saga.Orchestrator.HttpRepository
{
    public class InventoryHttpRepository : IInventoryHttpRepository
    {
        private readonly HttpClient _httpClient;
        public InventoryHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> CreateOrderSale(string orderNo, SalesOrderDto model)
        {
            var respone = await _httpClient.PostAsJsonAsync($"inventory/sales/order-no/{orderNo}", model);
            if(!respone.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                throw new Exception($"Create sale order for Order No: {orderNo} failed");
            }

            var result = await respone.ReadContentAs<CreatedSalesOrderSuccessDto>();
            return result.DocumentNo;
        }

        public async Task<string> CreateSalesOrder(SalesProductDto model)
        {
            var response = await _httpClient.PostAsJsonAsync($"inventory/sales/{model.ItemNo}", model);
            if(!response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                throw new Exception($"Create sale order for item: {model.ItemNo} failed");
            }
            
            var inventory = await response.ReadContentAs<InventoryEntryDto>();
            return inventory.DocumentNo;
        }

        public async Task<bool> DeleteOrderByDocumentNo(string documentNo)
        {
            var response = await _httpClient.DeleteAsync($"inventory/document-no/{documentNo}");
            if (!response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                throw new Exception($"Delete order for documentno: {documentNo} failed");
            }

            var result = await response.ReadContentAs<bool>();
            return result;
        }
    }
}
