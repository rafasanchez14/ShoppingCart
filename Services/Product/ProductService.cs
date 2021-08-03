using Microsoft.Extensions.Configuration;
using Models;
using MyShoppingCart.Repository.SqlServer;
using System;

using System.Linq;

namespace Services
{

    public class ProductsService : IProductsService
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
                    response.message = "";
                    response.warningMessage = "";
                    response.responseCode = 200;
                }
                else
                {
                    response.data = null;
                    response.errorCode = 1;
                    response.message = "No se encontró información";
                    response.warningMessage = "";
                    response.responseCode = 404;
                }


            }
            catch (Exception ex)
            {

                response.data = null;
                response.errorCode = 1;
                response.message = ex.Message;
                response.warningMessage = "";
                response.responseCode = 404;

            }

            return response;
        }
    }
}