using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab_06v1.App_Start
{
    public class CustomDbInitializer : DropCreateDatabaseAlways<EntitiesContext>
    {

        protected override void Seed(EntitiesContext context)
        {
            context.Salesmens.Add(new Models.Salesman{ firstName = "name1", secondName = "secondName1", gender = "male", store_id = 1 });
            context.Salesmens.Add(new Models.Salesman{ firstName = "name2", secondName = "secondName2", gender = "male", store_id = 2 });
            context.Salesmens.Add(new Models.Salesman{ firstName = "name3", secondName = "secondName3", gender = "male", store_id = 3 });
            context.Salesmens.Add(new Models.Salesman{ firstName = "name4", secondName = "secondName4", gender = "male", store_id = 4 });


            context.Stores.Add(new Models.Store { storeName = "Store#1", location = "location#1", type = "type" });
            context.Stores.Add(new Models.Store { storeName = "Store#2", location = "location#2", type = "type" });
            context.Stores.Add(new Models.Store { storeName = "Store#3", location = "location#3", type = "type" });
            context.Stores.Add(new Models.Store { storeName = "Store#3", location = "location#3", type = "type" });

            context.Vendors.Add(new Models.Vendor { vendorName = "vendor#1", productsType = "type#1", store_id = 1 });
            context.Vendors.Add(new Models.Vendor { vendorName = "vendor#2", productsType = "type#2", store_id = 2 });
            context.Vendors.Add(new Models.Vendor { vendorName = "vendor#3", productsType = "type#3", store_id = 3 });
            context.Vendors.Add(new Models.Vendor { vendorName = "vendor#4", productsType = "type#4", store_id = 4 });

            context.Products.Add(new Models.Product { productName = "product#1", price = 1.99f, vendor_id = 1 });
            context.Products.Add(new Models.Product { productName = "product#1", price = 1.99f, vendor_id = 2 });
            context.Products.Add(new Models.Product { productName = "product#1", price = 1.99f, vendor_id = 3 });
            context.Products.Add(new Models.Product { productName = "product#1", price = 1.99f, vendor_id = 4 });
        }

    }
}