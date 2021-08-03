using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ICacheService _cache;
        public ProductsController(IProductsService productsService, ICacheService cache)
        {
            _productsService = productsService;
            _cache = cache;
        }

        [HttpGet]
        public Response GetProducts()
        {
            return _productsService.GetAllProductRequest(_cache);
        }
    }
}