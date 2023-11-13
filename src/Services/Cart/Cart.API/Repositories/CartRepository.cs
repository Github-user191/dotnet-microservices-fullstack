using Cart.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Cart.API.Repositories {
    public class CartRepository : ICartRepository {
        private readonly IDistributedCache _redisCache;

        public CartRepository(IDistributedCache redisCache) {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart> GetCart(string userName) {
            var cart = await _redisCache.GetStringAsync(userName);

            if(String.IsNullOrEmpty(cart)) {
                return null;
            }

            var deserializedCart = JsonConvert.DeserializeObject<ShoppingCart>(cart);

            Console.WriteLine("--> Deserialized Cart");
            Console.WriteLine(deserializedCart);

            return deserializedCart;

        }

        public async Task<ShoppingCart> UpdateCart(ShoppingCart cart) {
            var serializedCart = JsonConvert.SerializeObject(cart);

            Console.WriteLine("--> Serialized Cart");
            Console.WriteLine(serializedCart);

            await _redisCache.SetStringAsync(cart.UserName, serializedCart);
            
            return await GetCart(cart.UserName);
        }

        public async Task DeleteCart(string userName) {
            System.Console.WriteLine($"--> Deleting cart for user {userName}");
            await _redisCache.RemoveAsync(userName);
        }
    }
}