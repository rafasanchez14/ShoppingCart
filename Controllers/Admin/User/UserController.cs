using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using MyShoppingCart.Services.Users;

namespace api.Admin.Controllers
{
    [ApiController]
   // [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IConfiguration _config;

        public UserController(IUserService service,IConfiguration configuration)
        {
            _service = service;
            _config = configuration;
        }

        [HttpGet]
        public Response GetUsers()
        {
            try
            {
                return _service.GetAllUserRequest(_config);
            }
            catch (Exception)
            {

                throw new Exception();
            }
          
        }
    }
}
