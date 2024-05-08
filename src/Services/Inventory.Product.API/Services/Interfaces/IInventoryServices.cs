using Inventory.Product.API.Entities;
using Inventory.Product.API.Repositories.Abstraction;
using Shared.DTOs.Inventory;

namespace Inventory.Product.API.Services.Interfaces
{
    public interface IInventoryServices : IMongoRepositoryBase<InventoryEntry>
    {
        Task<IEnumerable<InventoryEntryDto>> GetAllByItemNoAsync(string itemNo);
        Task<IEnumerable<InventoryEntryDto>> GetAllByItemNoPagingAsync(InventoryPagingQuery query);
        Task<InventoryEntryDto> GetByIdAsync(string id);
        Task<InventoryEntryDto> PurchaseItemAsync(string itemNo, PurchaseProductDto purchaseProduct);
    }
}
