
namespace Lab_06v1.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;


    public partial class Store
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("store_name")]
        [MaxLength(20, ErrorMessage = "Store name can not be longer than 20 symbols")]
        public string storeName { get; set; }
        public string location { get; set; }
        public string type { get; set; }

        public override string ToString()
        {
            return "Store {id: \"" + id + "\", store name: \""
                + storeName + ", \"location: \"" + location
                + "type: \"" + type + "\"}";
        }
    }
}
