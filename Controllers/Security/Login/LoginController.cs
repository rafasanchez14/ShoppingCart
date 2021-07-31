using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyShoppingCart.Controllers.Security.Login
{
    public class LoginController : Controller
    {
        [Produces("application/json")]
        [Route("api/v{version:apiVersion}/[controller]")]
        [ApiController]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public class BaseController : ControllerBase
        {

        }
    }


}
