using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class ShoppingCartResumeHeader
    {
        public string creationDate { get; set; }

        public int userId { get; set; }

        public string customer { get; set; }

        public List<ShoppingCartResumeDetail> detail{ get; set; }

        [JsonIgnore]
        public decimal dtotal { get; set; }

        public string total { get; set; }
    }

    public class ShoppingCartResumeDetail
    {
        public string productCode { get; set; }

        public string name { get; set; }
        public int quantity { get; set; }

        [JsonIgnore]
        public decimal dunitPrice { get; set; }
        [JsonIgnore]
        public decimal dtotal { get; set; }

        public string unitPrice { get; set; }
        public string total { get; set; }


    }
}
