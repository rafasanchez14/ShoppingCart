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
        private readonly ICacheService _cache;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IConfiguration configuration, ICacheService cache)
        {
            _service = shoppingCartService;
            _config = configuration;
            _cache = cache;
        }

        [HttpPost]
        public Response PostShopping(ShoppingCart shoppingCart)
        {
          
            return _service.PostShoppingCart(shoppingCart,_cache);
        }


        [HttpGet]
        [Route("{id}")]
        public Response GetShopping(int id)
        {

            return _service.GetShoppingCart(id, _cache);
        }
    }
}
