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
        public Task<string> CreateSalesOrder(SalesProductDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrderByDocumentNo(string documentNo)
        {
            throw new NotImplementedException();
        }
    }
}
