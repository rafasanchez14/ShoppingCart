using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class ShoppingCart
    {
        [Required]
        public int userId { get; set; }

        [Required]
        public string productCode { get; set; }

        [Required]
        public int quantity { get; set; }

        [JsonIgnore]
        public DateTime creationDate { get; set; }
    }

}
