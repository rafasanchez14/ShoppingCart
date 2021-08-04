using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingCartResumeHeader
    {
        public string creationDate { get; set; }

        public int userId { get; set; }

        public string customer { get; set; }

        public List<ShoppingCartResumeDetail> detail{ get; set; }

        public decimal total { get; set; }


    }

    public class ShoppingCartResumeDetail
    {
        public string productCode { get; set; }

        public string name { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal total { get; set; }

    }
}
