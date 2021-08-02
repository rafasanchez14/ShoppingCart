using Microsoft.Extensions.Configuration;
using Models;
using MyShoppingCart.Repository.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShoppingCart.Services.Users
{
    public class UserService : IUserService
    {
        public Response GetAllUserRequest(IConfiguration cnf)
        {
            var response = new Response();
            try
            {
               var cnx = cnf.GetConnectionString("ConnectionString");
                var repository = new UserRepository(cnx);

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
                response.responseCode = 404;

            }

            return response;
        }
    }
}
