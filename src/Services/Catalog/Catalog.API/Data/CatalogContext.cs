using Catalog.API.Dtos;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data {
    public class CatalogContext : ICatalogContext {

        public CatalogContext(IConfiguration configuration) {

            var connectionString = configuration["DatabaseSettings:ConnectionString"];
            var databaseName = configuration["DatabaseSettings:DatabaseName"];
            var collectionName = configuration["DatabaseSettings:CollectionName"];


            Console.WriteLine($"connectionString : {connectionString}");
            Console.WriteLine($"databaseName : {databaseName}");
            Console.WriteLine($"collectionName : {collectionName}");

            // Connect with MongoDB 
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            Products = database.GetCollection<Product>(collectionName);

            // Seed products
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
