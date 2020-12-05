

namespace Lab_06v1.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public Nullable<int> vendor_id { get; set; }

        [Column("product_name")]
        [MaxLength(20, ErrorMessage = "Name of product can not be longer than 20 symbols")]
        public string productName { get; set; }
        public Nullable<float> price { get; set; }

        public override string ToString()
        {
            return "Product {id: \"" + id + "\", product_name: \""
                + productName + "\", price: \""
                + price + "\"}";
        }
    }
}
