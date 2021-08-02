using Microsoft.Extensions.Configuration;
using Models;
using MyShoppingCart.Repository.SqlServer;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShoppingCart.Services.ShoppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        public Response GetAllProductRequest(IConfiguration cnf)
        {
            var response = new Response();
            try
            {
                var cnx = cnf.GetConnectionString("ConnectionString");
                var repository = new ProductRepository(cnx);

                var data = repository.GetAll();

                if (data.Count > 0)
                {
                    response.data = data;
                    response.errorCode = 0;
                    response.errorMessage = "";
                    response.warningMessage = "";
                    response.responseCode = 200;
                }
                else
                {
                    response.data = null;
                    response.errorCode = 1;
                    response.errorMessage = "No se encontró información";
                    response.warningMessage = "";
                    response.responseCode = 404;
                }


            }
            catch (Exception ex)
            {

                response.data = null;
                response.errorCode = 1;
                response.errorMessage = ex.Message;
                response.warningMessage = "";
                response.responseCode = 400;

            }

            return response;
        }

        public Response PostShoppingCart(IConfiguration cnf, Models.ShoppingCart shoppingCart)
        {
            var response = new Response();
            var cnx = cnf.GetConnectionString("ConnectionString");
            var repository = new ShoppingCartRepository(cnx);

            try
            {
               var saved_data = repository.Post(shoppingCart);

                if (saved_data.productId!=0)
                {
                    response.data = saved_data;
                    response.errorCode = 0;
                    response.errorMessage = "";
                    response.warningMessage = "";
                    response.responseCode = 200;
                }
                else
                {
                    response.data = saved_data;
                    response.errorCode = 2;
                    response.errorMessage = "Ocurrio un error al insertar informacion";
                    response.warningMessage = "";
                    response.responseCode = 400;
                }

            }
            catch (Exception ex)
            {

                response.data = null;
                response.errorCode = 1;
                response.errorMessage = ex.Message;
                response.warningMessage = "";
                response.responseCode = 400;
            }

            return response;
        }
    }
}
