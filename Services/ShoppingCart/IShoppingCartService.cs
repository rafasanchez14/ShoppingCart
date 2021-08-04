using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IShoppingCartService
    {

        Response PostShoppingCart(ShoppingCart shoppingCart,ICacheService cache);


        Response GetShoppingCart( int id,ICacheService cache);

        Response GetShoppingCartResume(int id, ICacheService cache);
    }
}