using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShoppingCart.Repository.Redis
{
    public class ShoppingCartRepository
    {
        private readonly ICacheService _cache;
        public ShoppingCartRepository(ICacheService cache)
        {
            _cache = cache;
        }
        //Agrega información de carrito a memoria
        public List<ShoppingCart> Post(ShoppingCart shopping)
        {
          
            try
            {
                //Fecha de creación
                shopping.creationDate = DateTime.Now;
                var cached = _cache.Get<List<ShoppingCart>>(shopping.userId.ToString());
                if (cached!=null && cached.Count>0)
                {
                    
                    cached.Add(shopping);
                }
                else
                {

                    cached = new List<ShoppingCart>();
                    cached.Add(shopping);
                    Console.WriteLine("[BASKET CREATED]: Created[<" + shopping.creationDate.ToString("dd-MM-yyyy") + ">], " +shopping.userId);

                }
                var productRepository = new ProductRepository(_cache);
                var prodInfoDetail = productRepository.GetListProducts().FirstOrDefault(q => q.productCode == shopping.productCode);
                Console.WriteLine("[ITEM ADDED TO SHOPPING CART]: Added[<" + shopping.creationDate.ToString("dd-MM-yyyy") + ">], " + shopping.userId + ", " + shopping.productCode + ", " + shopping.quantity + "[, Price[<€"+ prodInfoDetail .price+ ">]");
                _cache.Set<List<ShoppingCart>>(shopping.userId.ToString(), cached);

                return cached;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
           

            
        }

        //Consulta carrito por usuario
        public List<ShoppingCart> GetShoppingCartByUser(int id)
        {
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
