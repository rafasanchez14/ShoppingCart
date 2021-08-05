using Models;
using MyShoppingCart.Repository.Redis;
using MyShoppingCart.Services.Users;
using Services;
using System;

namespace MyShoppingCart.Services.User
{
    public class UserService : IUserService
    {
        //Obtiene usuarios registrados
        public Response GetAllUserRequest(ICacheService cache)
        {
            var response = new Response();
            try
            {
                var repository = new UserRepository(cache);

                var data = repository.GetListUsers();

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
