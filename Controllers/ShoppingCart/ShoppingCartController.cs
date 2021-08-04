using Microsoft.AspNetCore.Mvc;
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
        private readonly ICacheService _cache;
        public ShoppingCartController(IShoppingCartService shoppingCartService, ICacheService cache)
        {
            _service = shoppingCartService;
            _cache = cache;
        }

        /// <summary>
        ///  Add item to basket/cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns> General response</returns>
        [HttpPost]
        public Response PostShopping(ShoppingCart shoppingCart)
        {

            return _service.PostShoppingCart(shoppingCart,_cache);
        }

        /// <summary>
        /// Get information about the items on the users basket
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>General response</returns>
        [HttpGet]
        [Route("{id}")]
        public Response GetShopping(int id)
        {

            return _service.GetShoppingCartResume(id, _cache);
        }
    }
}
