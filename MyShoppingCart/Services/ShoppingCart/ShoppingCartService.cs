using Models;
using MyShoppingCart.Repository.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        //Guarda información de carrito de compra
        public Response PostShoppingCart(ShoppingCart shoppingCart, ICacheService cache)
        {
            var response = new Response();
           // var cnx = cnf.GetConnectionString("ConnectionString");
            var repository = new ShoppingCartRepository(cache);

            try
            {
                var userService = new UserRepository(cache);
                var userInfoDetail = userService.GetListUsers().FirstOrDefault(q => q.userId == shoppingCart.userId);
                if (userInfoDetail == null)
                {
                    response.data = shoppingCart.userId;
                    response.errorCode = 3;
                    response.message = "Usuario no encontrado";
                    response.warningMessage = "Por favor verifíque lista de usuarios";
                    response.responseCode = 404;

                    return response;
                }
                var productRepository = new ProductRepository(cache);
                var prodInfoDetail = productRepository.GetListProducts().FirstOrDefault(q => q.productCode == shoppingCart.productCode);
                if (prodInfoDetail == null)
                {
                    response.data = shoppingCart.productCode;
                    response.errorCode = 4;
                    response.message = "Producto no encontrado";
                    response.warningMessage = "Por favor verifíque lista de productos";
                    response.responseCode = 404;

                    return response;
                }
                else
                {
                    var saved_data = repository.Post(shoppingCart);

                    if (saved_data.Count > 0)
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

        //Obtiene información del carrito de compra
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

        //Obtiene resúmen de agregados
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

        //Crear resumen de agregados al carrito
        private ShoppingCartResumeHeader CreateResumeShoppingCart(List<ShoppingCart> list, ICacheService cache)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            var sCurrency = "€";
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
                                                        dtotal = 0,
                                                    }).ToList();
                    result.ForEach(x => {
                        var productRepository = new ProductRepository(cache);
                        var prodInfoDetail = productRepository.GetListProducts().FirstOrDefault(q => q.productCode == x.productCode);
                        x.name = prodInfoDetail.name;
                        x.dunitPrice = prodInfoDetail.price;
                        x.unitPrice = sCurrency + x.dunitPrice.ToString("F");
                        x.dtotal = x.dunitPrice * x.quantity;
                        x.total = sCurrency + x.dtotal.ToString("F");
                    });
                    shoppingCartResume.detail = result;
                    shoppingCartResume.dtotal = result.Sum(x => x.dtotal);
                    shoppingCartResume.total = sCurrency+ shoppingCartResume.dtotal.ToString("F");
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
