﻿using Catalog.API.Data;
using Catalog.API.Dtos;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repositories {
    public class ProductRepository : IProductRepository {

        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts() {
            return await _context
                .Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(string id) {
            return await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name) {

            // Create a filter query for an IMongoCollection< T > collection
            var filter = Builders<Product>.Filter.Eq(
                p => p.Name, name);

            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category) {
            // Create a filter query for an IMongoCollection< T > collection
            var filter = Builders<Product>.Filter.Eq(
                p => p.Category, category);

            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task CreateProduct(Product product) {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product) {
            Console.WriteLine($"--> Trying to update product {product}");

            var filter = Builders<Product>.Filter
                .Eq(p => p.Id, product.Id);

            var updateResult = await _context
                .Products
                .ReplaceOneAsync(filter: filter, replacement: product);

            Console.WriteLine($"--> Upserted ID: {updateResult}");

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id) {
            var filter = Builders<Product>.Filter.Eq(
                p => p.Id, id);


            var deleteResult = await _context
                .Products
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
