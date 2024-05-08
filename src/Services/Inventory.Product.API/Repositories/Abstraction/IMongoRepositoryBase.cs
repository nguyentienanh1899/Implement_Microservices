using Inventory.Product.API.Entities.Abstraction;
using MongoDB.Driver;

namespace Inventory.Product.API.Repositories.Abstraction
{
    public interface IMongoRepositoryBase<T> where T : MongoEntity
    {
        IMongoCollection<T> FindAll(ReadPreference? readPreference = null);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);

    }
}
