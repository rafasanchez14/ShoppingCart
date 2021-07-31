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
             new Product{name="prd1",price=12}
        };

        public IEnumerable<Product> GetProducts()
        {
            return Products.AsEnumerable();
        }
    }
}