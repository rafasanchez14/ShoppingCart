using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class ProductsService : IProductsService
    {
        private readonly List<Product> Products = new List<Product>
    {
      new Product { Title= "DVD player" },
      new Product { Title= "TV" },
      new Product { Title= "Projector" }
    };

        public IEnumerable<Product> GetProducts()
        {
            return Products.AsEnumerable();
        }
    }
}