﻿using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IProductsService
    {
        IEnumerable<Product> GetProducts();
    }
}