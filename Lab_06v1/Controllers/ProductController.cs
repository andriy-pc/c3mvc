using Lab_06v1.App_Start;
using Lab_06v1.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Lab_06v1.Controllers
{
    public class ProductController : Controller
    {
        private EntitiesContext context = new EntitiesContext();

        public ActionResult Index()
        {
            return View(context);
        }

        public ActionResult DeleteFromDB(int id)
        {
            try
            {
                Product p = new Models.Product { id = id };
                context.Products.Attach(p);
                context.Products.Remove(p);
                context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
            {
                return RedirectToAction("Error", "Product", new { errorMessage = "Probably you tried to Delete From DB with the wrong id, no one was removed" });
            }
        }

        public string Error(string errorMessage)
        {
            return errorMessage;
        }

        [HttpPost]
        public ActionResult Remove(FormCollection form)
        {
            var allCheck = Request.Form["check"];
            if (allCheck != null)
            {
                string selected = allCheck.ToString();
                string[] selectedEntities = selected.Split(',');
                foreach (var selectedEntity in selectedEntities)
                {
                    DeleteFromDB(int.Parse(selectedEntity));
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return RedirectToAction("Error", "Product", new { errorMessage = "Please select product to remove first!" });
            }
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var productName = Request.Form["productName"];
            var price = Request.Form["price"];

            if (productName != null && price != null)
            {
                string newProductName = productName.ToString();
                float newPrice = float.Parse(price);
                if (newProductName.Length != 0)
                {
                    AddToDB(newProductName, newPrice);
                }
                else
                {
                    return RedirectToAction("Error", "Product", new { errorMessage = "Please insert product name and price to insert" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Product", new { errorMessage = "Please insert product name and price to insert" });
            }
            return RedirectToAction("Index", "Product");
        }

        private void AddToDB(string productName, float price)
        {
            Product p = new Models.Product { productName = productName, price = price };
            context.Products.Add(p);
            context.SaveChanges();
        }

        public ActionResult Update(FormCollection form)
        {
            var idString = Request.Form["id"];
            var productName = Request.Form["productName"];
            var price = Request.Form["price"];

                if (idString != null && productName != null && price != null)
            {
                int id = int.Parse(idString);
                string newProductName = productName.ToString();
                float newPrice = float.Parse(price);
                if (newProductName.Length != 0)
                {
                    try
                    {
                        Update(id, newProductName, newPrice);
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("Error", "Product", new { errorMessage = e.Message });
                    }

                }
                else
                {
                    return RedirectToAction("Error", "Product", new { errorMessage = "Please insert id, product name and price to update" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Product", new { errorMessage = "Please insert id, product name and price to update" });
            }
            return RedirectToAction("Index", "Product");
        }


        private void Update(int id, string productName, float price)
        {
            Product dbModel = context.Products.Where(s => s.id == id).First();
            if (dbModel != null)
            {
                dbModel.productName = productName;
                dbModel.price = price;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Product with id = " + id + " was not found!");
            }
        }
    }
}