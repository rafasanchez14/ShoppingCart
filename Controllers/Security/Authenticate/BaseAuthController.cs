using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Security
{
    public class BaseAuthController
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
