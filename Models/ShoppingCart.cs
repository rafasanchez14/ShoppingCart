using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingCart
    {
        public string productCode { get; set; }

        public int productId { get; set; }

        public int userId { get; set; }

        public int quantity { get; set; }
    }

}
