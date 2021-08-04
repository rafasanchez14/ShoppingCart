using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingCart
    {

        public int userId { get; set; }
        public string productCode { get; set; }
        public int quantity { get; set; }

        [JsonIgnore]
        public DateTime creationDate { get; set; }
    }

}
