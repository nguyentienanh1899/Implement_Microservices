﻿using MongoDB.Driver;

namespace Contracts.Domains.Interfaces
{
    public interface IMongoRepositoryBase<T> where T : MongoEntity
    {
        IMongoCollection<T> FindAll(ReadPreference? readPreference = null);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);

    }
}
