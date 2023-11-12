using Catalog.API.Dtos;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data {
    public interface ICatalogContext {

        IMongoCollection<Product> Products { get; }
    }
}
