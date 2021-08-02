using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IShoppingCartService
    {
        Response GetAllProductRequest(IConfiguration cnf);

        Response PostShoppingCart(IConfiguration cnf,ShoppingCart shoppingCart);
    }
}