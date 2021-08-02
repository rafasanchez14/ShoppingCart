using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Services;

namespace api.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _service;
        private readonly IConfiguration _config;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IConfiguration configuration)
        {
            _service = shoppingCartService;
            _config = configuration;
        }

        [HttpPost]
        public Response GetProducts(ShoppingCart shoppingCart)
        {
            return _service.PostShoppingCart(_config,shoppingCart);
        }
    }
}
