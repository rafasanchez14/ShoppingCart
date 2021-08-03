using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShoppingCart.Repository.Redis
{
    public class ProductRepository
    {
        private readonly ICacheService _cache;
        private string sKey = "product";
        public ProductRepository(ICacheService cache)
        {
            _cache = cache;
        }

        public List<Product> GetListProducts()
        {
          
            try
            {
               
                var cached = _cache.Get<List<Product>>(sKey);

                if (cached == null)
                {
                    _cache.Set(sKey, ListProductData());
                    cached = _cache.Get<List<Product>>(sKey);

                }



                return cached;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }
        
            
        private List<Product> ListProductData()
        {
            var list = new List<Product>();

            list.Add(new Product { name = "Lord of the Rings", price = 10.00m , productCode = "10001", type = 0 });
            list.Add(new Product { name = "The Hobbit", price = 5.00m, productCode = "10002", type = 0 });

            list.Add(new Product { name = "Game of Thrones", price = 5.00m, productCode = "20001", type = 1});
            list.Add(new Product { name = "Breaking Bad", price = 7.00m, productCode = "20002", type = 1 });

            return list;
        }
    }
}
