using Models;
using MyShoppingCart.Repository.Redis;
using System;

namespace Services
{

    public class ProductsService : IProductsService
    {
        //obtiene listado de productos
        public Response GetAllProductRequest(ICacheService cache)
        {
            var response = new Response();
            try
            {
                var repository = new ProductRepository(cache);

                var data = repository.GetListProducts();

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
                response.responseCode = 400;

            }

            return response;
        }
    }
}