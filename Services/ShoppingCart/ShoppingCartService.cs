using Microsoft.Extensions.Configuration;
using Models;
using MyShoppingCart.Repository.Redis;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShoppingCart.Services.ShoppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        public Response PostShoppingCart(Models.ShoppingCart shoppingCart, ICacheService cache)
        {
            var response = new Response();
           // var cnx = cnf.GetConnectionString("ConnectionString");
            var repository = new ShoppingCartRepository(cache);

            try
            {
               var saved_data = repository.Post(shoppingCart);

                if (saved_data.Count>0)
                {
                    response.data = saved_data;
                    response.errorCode = 0;
                    response.message = "Se ha agregado item al carrito exitósamente";
                    response.warningMessage = "";
                    response.responseCode = 200;
                }
                else
                {
                    response.data = saved_data;
                    response.errorCode = 2;
                    response.message = "Ocurrio un error al insertar informacion";
                    response.warningMessage = "";
                    response.responseCode = 400;
                }

            }
            catch (Exception ex)
            {

                response.data = null;
                response.errorCode = 1;
                response.message = ex.Message;
                response.warningMessage = "";
                response.responseCode = 400;
            }

            return response;
        }

        public Response GetShoppingCart(int id,ICacheService cache)
        {
            var response = new Response();
            try
            {
                var repository = new ShoppingCartRepository(cache);

                var data = repository.GetShoppingCartByUser(id);

                if (data.Count > 0)
                {
                    response.data = data;
                    response.errorCode = 0;
                    response.message = "";
                    response.warningMessage = "";
                    response.responseCode = 200;
                }
                else
                {
                    response.data = null;
                    response.errorCode = 1;
                    response.message = "No se encontró información";
                    response.warningMessage = "El carrito aun no se encuentra creado";
                    response.responseCode = 404;
                }


            }
            catch (Exception ex)
            {

                response.data = null;
                response.errorCode = 1;
                response.message = ex.Message;
                response.warningMessage = "";
                response.responseCode = 400;

            }

            return response;
        }
    }
}
