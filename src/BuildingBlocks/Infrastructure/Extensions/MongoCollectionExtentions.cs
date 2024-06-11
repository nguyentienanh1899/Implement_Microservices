using Infrastructure.Common.Models;
using MongoDB.Driver;

namespace Infrastructure.Extensions
{
    public static class MongoCollectionExtentions
    {
        public static Task<PagedList<TDestinatiom>> PaginatedListAsync<TDestinatiom>(this IMongoCollection<TDestinatiom> collection, FilterDefinition<TDestinatiom> filter, int pageIndex, int pageSize) where TDestinatiom : class
            => PagedList<TDestinatiom>.ToPagedList(collection, filter, pageIndex, pageSize);
    }
}
