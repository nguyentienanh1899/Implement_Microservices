using Infrastructure.Common.Models;
using MongoDB.Driver;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class MongoCollectionExtentions
    {
        public static Task<PagedList<TDestinatiom>> PaginatedListAsync<TDestinatiom>(this IMongoCollection<TDestinatiom> collection, FilterDefinition<TDestinatiom> filter, int pageIndex, int pageSize) where TDestinatiom : class
            => PagedList<TDestinatiom>.ToPagedList(collection, filter, pageIndex, pageSize);
    }
}
