using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IProductsService
    {
        Response GetAllProductRequest(IConfiguration cnf);
    }
}