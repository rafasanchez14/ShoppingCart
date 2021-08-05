using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Models;
using MyShoppingCart.Services.Redis;
using Services;
using System;
using Xunit;

namespace MyShoppingCart.Test
{
    public class ShoppingCartServiceTest
    {
        /// <summary>
        /// Prueba unitaria encargada de probar manejo de excepción por envio de cahce null
        /// </summary>
        [Fact]
        public void ShoppingCartServiceTest_CacheNull()
        {
            var shoppingCartService = new ShoppingCartService();
            var shoppingCart = new ShoppingCart();
            var response = shoppingCartService.PostShoppingCart(shoppingCart, null);
            Assert.Equal("400", response.responseCode.ToString());           
        }

        /// <summary>
        ///  Prueba unitaria encargada de probar manejo de excepción por envio de data  null
        /// </summary>
        [Fact]
        public void ShoppingCartServiceTest_ShoppingCartNull()
        {
            var shoppingCartService = new ShoppingCartService();
            var response = shoppingCartService.PostShoppingCart(null, null);
            Assert.Equal("400", response.responseCode.ToString());
        }

        /// <summary>
        /// Prueba unitaria que comprueba respuesta de producto no encontrado
        /// </summary>
        [Fact]
        public void ShoppingCartServiceTest_NotFoundProduct()
        {
            var opts = Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions());
            IDistributedCache cache1 = new MemoryDistributedCache(opts);

            var cache = new CacheService(cache1);

            var shoppingCart = new ShoppingCart
            {
                creationDate = DateTime.Now,
                productCode = "1005",
                userId = 1,
                quantity = 2

            };
            var shoppingCartService = new ShoppingCartService();
            var response = shoppingCartService.PostShoppingCart(shoppingCart, cache);
            Assert.Equal("4", response.errorCode.ToString());
        }
       
        /// <summary>
        /// Prueba Unitaria que comprueba respuesta de usuario no encontrado
        /// </summary>
        [Fact]
        public void ShoppingCartServiceTest_NotFoundUser()
        {
            var opts = Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions());
            IDistributedCache cache1 = new MemoryDistributedCache(opts);

            var cache = new CacheService(cache1);

            var shoppingCart = new ShoppingCart
            {
                creationDate = DateTime.Now,
                productCode = "10001",
                userId = 500,
                quantity = 2

            };
            var productService = new ProductsService();
            productService.GetAllProductRequest(cache);
            var shoppingCartService = new ShoppingCartService();
            var response = shoppingCartService.PostShoppingCart(shoppingCart, cache);
            Assert.Equal("3", response.errorCode.ToString());
        }

        /// <summary>
        /// Prueba unitaria que comprueba agregado con éxito producto al carrito
        /// </summary>
        [Fact]
        public void ShoppingCartServiceTest_Add()
        {
            var opts = Options.Create(new MemoryDistributedCacheOptions());
            IDistributedCache icache = new MemoryDistributedCache(opts);

            var cache = new CacheService(icache);

            var shoppingCart = new ShoppingCart
            {
                creationDate = DateTime.Now,
                productCode = "10001",
                userId = 1,
                quantity = 2

            };
            var productService = new ProductsService();
            var shoppingCartService = new ShoppingCartService();
            productService.GetAllProductRequest(cache);
            var response = shoppingCartService.PostShoppingCart(shoppingCart, cache);
            Assert.Equal("0", response.errorCode.ToString());
        }


    }
}
