using Lab_06v1.App_Start;
using Lab_06v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_06v1.Controllers
{
    public class StoreController : Controller
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
                Store p = new Models.Store { id = id };
                context.Stores.Attach(p);
                context.Stores.Remove(p);
                context.SaveChanges();
                return RedirectToAction("Index", "Store");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
            {
                return RedirectToAction("Error", "Store", new { errorMessage = "Probably you tried to Delete From DB with the wrong id, no one was removed" });
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
                return RedirectToAction("Index", "Store");
            }
            else
            {
                return RedirectToAction("Error", "Store", new { errorMessage = "Please select store(s) to remove first!" });
            }
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var storetName = Request.Form["storeName"];
            var location = Request.Form["location"];

            if (storetName != null && location != null)
            {
                string newStoreName = storetName.ToString();
                string newLocation = location;
                if (newStoreName.Length != 0 && newLocation.Length != 0)
                {
                    AddToDB(newStoreName, newLocation);
                }
                else
                {
                    return RedirectToAction("Error", "Store", new { errorMessage = "Please insert store's name and location to insert" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Store", new { errorMessage = "Please insert store's name and location to insert" });
            }
            return RedirectToAction("Index", "Store");
        }

        private void AddToDB(string storeName, string location)
        {
            Store s = new Models.Store { storeName = storeName, location = location };
            context.Stores.Add(s);
            context.SaveChanges();
        }

        public ActionResult Update(FormCollection form)
        {
            var idString = Request.Form["id"];
            var storeName = Request.Form["storeName"];
            var location = Request.Form["location"];

            if (idString != null && storeName != null && location != null)
            {
                int id = int.Parse(idString);
                string newStoreName = storeName.ToString();
                string newLocation = location.ToString();
                if (newStoreName.Length != 0 && newLocation.Length != 0)
                {
                    try
                    {
                        Update(id, newStoreName, newLocation);
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("Error", "Store", new { errorMessage = e.Message });
                    }

                }
                else
                {
                    return RedirectToAction("Error", "Store", new { errorMessage = "Please insert id, store name and location to update" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Store", new { errorMessage = "Please insert id, store name and location to update" });
            }
            return RedirectToAction("Index", "Store");
        }


        private void Update(int id, string storeName, string location)
        {
            var dbModel = context.Stores.Where(s => s.id == id).First();
            if (dbModel != null)
            {
                dbModel.storeName = storeName;
                dbModel.location = location;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Store with id = " + id + " was not found!");
            }
        }
    }
}