using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            this._redisCache = redisCache;
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redisCache.GetStringAsync(username);
            if(String.IsNullOrEmpty(basket))            
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }


        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await _redisCache.SetStringAsync(shoppingCart.Username, JsonConvert.SerializeObject(shoppingCart));
            return await GetBasket(shoppingCart.Username);
        }

        public async Task DeleteBasket(string username)
        {
            await _redisCache.RemoveAsync(username);
        }

      
      
    }
}
