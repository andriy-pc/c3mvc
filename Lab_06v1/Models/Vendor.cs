


namespace Lab_06v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Vendor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public Nullable<int> store_id { get; set; }

        [Column("vendor_name")]
        [MaxLength(20, ErrorMessage = "Vendor name can not be longer than 20 symbols")]
        public string vendorName { get; set; }

        [Column("products_type")]
        public string productsType { get; set; }

        public override string ToString()
        {
            return "Vendor {id: \"" + id + "\", vendor name: \""
                + vendorName + "\", products type: \""
                + productsType + "\"}";
        }
    }
}
