using Microsoft.Extensions.Configuration;
using Models;
using MyShoppingCart.Repository.Redis;
using MyShoppingCart.Services.User;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        public Response PostShoppingCart(ShoppingCart shoppingCart, ICacheService cache)
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


        public Response GetShoppingCartResume(int id, ICacheService cache)
        {
            var response = new Response();
            try
            {
                var repository = new ShoppingCartRepository(cache);

                var shopping = repository.GetShoppingCartByUser(id);

                var data = CreateResumeShoppingCart(shopping, cache);

                if (data.userId!= 0)
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


        private ShoppingCartResumeHeader CreateResumeShoppingCart(List<ShoppingCart> list, ICacheService cache)
        {

            try
            {
                var shoppingCartResume = new ShoppingCartResumeHeader();

                if (list!=null && list.Count > 0)
                {
                    shoppingCartResume.creationDate = list.OrderBy(x => x.creationDate)?.FirstOrDefault()?.creationDate.ToString("dd/MM/yyyy");
                    shoppingCartResume.userId = list.FirstOrDefault().userId;
                    var userService = new UserRepository(cache);
                    var userInfoDetail = userService.GetListUsers().FirstOrDefault(q => q.userId == shoppingCartResume.userId);
                    shoppingCartResume.customer = userInfoDetail.name;
                    List<ShoppingCartResumeDetail> result = list
                                                    .GroupBy(l => l.productCode)
                                                    .Select(cl => new ShoppingCartResumeDetail
                                                    {
                                                        productCode = cl.First().productCode,
                                                        name = "",
                                                        quantity = Convert.ToInt32(cl.Sum(c => c.quantity)),
                                                        total = 0,
                                                    }).ToList();
                    result.ForEach(x => {
                        var productRepository = new ProductRepository(cache);
                        var prodInfoDetail = productRepository.GetListProducts().FirstOrDefault(q => q.productCode == x.productCode);
                        x.name = prodInfoDetail.name;
                        x.unitPrice = prodInfoDetail.price;
                        x.total = x.unitPrice * x.quantity;
                    });
                    shoppingCartResume.detail = result;
                    shoppingCartResume.total = result.Sum(x => x.total);
                }

                return shoppingCartResume;

            }
            catch (Exception)
            {

                throw;
            }

        }
       
    }


}
