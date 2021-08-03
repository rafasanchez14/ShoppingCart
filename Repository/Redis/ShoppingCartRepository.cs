using Models;
using Services;
using System;
using System.Collections.Generic;

namespace MyShoppingCart.Repository.Redis
{
    public class ShoppingCartRepository
    {
        private readonly ICacheService _cache;
        public ShoppingCartRepository(ICacheService cache)
        {
            _cache = cache;
        }

        public List<ShoppingCart> Post(ShoppingCart shopping)
        {
          
            try
            {
                var cached = _cache.Get<List<ShoppingCart>>(shopping.userId.ToString());

                if (cached!=null && cached.Count>0)
                {
                    cached.Add(shopping);
                }
                else
                {
                    cached = new List<ShoppingCart>();
                    cached.Add(shopping);


                }

                _cache.Set<List<ShoppingCart>>(shopping.userId.ToString(), cached);

                return cached;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
           

            
        }

        public List<ShoppingCart> GetShoppingCartByUser(int id)
        {
            var shoppingList = new List<ShoppingCart>();
            try
            {
                var cached = _cache.Get<List<ShoppingCart>>(id.ToString());
                
                if (cached!=null)
                {
                    return cached;
                }
                else
                {
                    return new List<ShoppingCart>();
                }

               
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
