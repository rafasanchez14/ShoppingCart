using Microsoft.Extensions.Configuration;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShoppingCart.Services.Users
{
    public interface IUserService
    {
        Response GetAllUserRequest(ICacheService cache);
    }
}
