using Lab_06v1.App_Start;
using Lab_06v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_06v1.Controllers
{
    public class VendorController : Controller
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
                    Vendor p = new Models.Vendor { id = id };
                    context.Vendors.Attach(p);
                    context.Vendors.Remove(p);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Vendor");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
                {
                    return RedirectToAction("Error", "Vendor", new { errorMessage = "Probably you tried to Delete From DB with the wrong id, no one was removed" });
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
                    return RedirectToAction("Index", "Vendor");
                }
                else
                {
                    return RedirectToAction("Error", "Vendor", new { errorMessage = "Please select Vendor(s) to remove first!" });
                }
            }

            [HttpPost]
            public ActionResult Add(FormCollection form)
            {
                var vendorName = Request.Form["vendorName"];
                var productsType = Request.Form["productsType"];

                if (vendorName != null && productsType != null)
                {
                    string newVendorName = vendorName.ToString();
                    string newProductsType = productsType;
                    if (newVendorName.Length != 0 && newProductsType.Length != 0)
                    {
                        AddToDB(newVendorName, newProductsType);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Vendor", new { errorMessage = "Please insert vendor's name and products type to insert" });
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Vendor", new { errorMessage = "Please insert vendor's name and products type to insert" });
                }
                return RedirectToAction("Index", "Vendor");
            }

            private void AddToDB(string vendorName, string productsType)
            {
                Vendor v = new Models.Vendor { vendorName = vendorName, productsType = productsType };
                context.Vendors.Add(v);
                context.SaveChanges();
            }

            public ActionResult Update(FormCollection form)
            {
                var idString = Request.Form["id"];
                var vendorName = Request.Form["vendorName"];
                var productsType = Request.Form["productsType"];

                if (idString != null && vendorName != null && productsType != null)
                {
                    int id = int.Parse(idString);
                    string newVendorName = vendorName.ToString();
                    string newProductsType = productsType.ToString();
                    if (newVendorName.Length != 0 && newProductsType.Length != 0)
                    {
                        try
                        {
                            Update(id, newVendorName, newProductsType);
                        }
                        catch (Exception e)
                        {
                            return RedirectToAction("Error", "Vendor", new { errorMessage = e.Message });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Error", "Vendor", new { errorMessage = "Please insert id, vendor's name and products type to update" });
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Vendor", new { errorMessage = "Please insert id, vendor's name and products type to update" });
                }
                return RedirectToAction("Index", "Vendor");
            }


            private void Update(int id, string vendorName, string productsType)
            {
                var dbModel = context.Vendors.Where(s => s.id == id).First();
                if (dbModel != null)
                {
                    dbModel.vendorName = vendorName;
                    dbModel.productsType = productsType;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Vendor with id = " + id + " was not found!");
                }
            }
        }
    }
}