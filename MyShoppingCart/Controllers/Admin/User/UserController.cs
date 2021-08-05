using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using MyShoppingCart.Services.Users;
using Services;
using System;

namespace api.Admin.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ICacheService _cache;


        public UserController(IUserService service, ICacheService cache)
        {
            _service = service;
            _cache = cache;
        }



        /// <summary>
        /// Get list of users (only admins)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Response GetUsers()
        {
            try
            {
                return _service.GetAllUserRequest(_cache);
            }
            catch (Exception)
            {

                throw new Exception();
            }
          
        }
    }
}
