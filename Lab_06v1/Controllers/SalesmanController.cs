using Lab_06v1.App_Start;
using Lab_06v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_06v1.Controllers
{
    public class SalesmanController : Controller
    {

        private EntitiesContext context = new EntitiesContext();

        public ActionResult Index()
        {
            return View(context);
        }

        public ActionResult DeleteFromDB(int possitionInQueue)
        {
            try
            {
                Salesman s = new Models.Salesman(possitionInQueue);
                context.Salesmens.Attach(s);
                context.Salesmens.Remove(s);
                context.SaveChanges();
                return RedirectToAction("Index", "Salesman");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
            {
                return RedirectToAction("Error", "Salesman", new { errorMessage = "Probably you tried to DeleteFromDB from queue the salesman with the wrong id, no one was removed" });
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
                string[] selectedStudents = selected.Split(',');
                foreach (var selectedStudent in selectedStudents)
                {
                    DeleteFromDB(int.Parse(selectedStudent));
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Salesman");
            }
            else
            {
                return RedirectToAction("Error", "Salesman", new { errorMessage = "Please select salesman to remove first!" });
            }
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var newStudentFirstName = Request.Form["firstName"];
            var newStudentSecondName = Request.Form["secondName"];

            if (newStudentFirstName != null && newStudentSecondName != null)
            {
                string firstName = newStudentFirstName.ToString();
                string secondName = newStudentSecondName.ToString();
                if (firstName.Length != 0 && secondName.Length != 0)
                {
                    AddToDB(firstName, secondName);
                }
                else
                {
                    return RedirectToAction("Error", "Salesman", new { errorMessage = "Please insert first and second name of the student" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Salesman", new { errorMessage = "Please insert first and second name of the student" });
            }
            return RedirectToAction("Index", "Salesman");
        }

        private void AddToDB(string firstName, string secondName)
        {
            Salesman s = new Models.Salesman(firstName, secondName);
            context.Salesmens.Add(s);
            context.SaveChanges();
        }

        public ActionResult Update(FormCollection form)
        {
            var idString = Request.Form["id"];
            var newFirstName = Request.Form["firstName"];
            var newSecondName = Request.Form["secondName"];

            if (idString != null && newFirstName != null && newSecondName != null)
            {
                int id = int.Parse(idString);
                string firstName = newFirstName.ToString();
                string secondName = newSecondName.ToString();
                if (firstName.Length != 0 && secondName.Length != 0)
                {
                    try
                    {
                        Update(id, firstName, secondName);
                    } 
                    catch (Exception e)
                    {
                        return RedirectToAction("Error", "Salesman", new { errorMessage = e.Message });
                    }
                    
                }
                else
                {
                    return RedirectToAction("Error", "Salesman", new { errorMessage = "Please insert first and second name of the salesman" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Salesman", new { errorMessage = "Please insert id, first and second name of the salesman to update" });
            }
            return RedirectToAction("Index", "Salesman");
        }


        private void Update(int id, string firstName, string secondName)
        {
            Salesman dbModel = context.Salesmens.Where(s => s.id == id).First();
            if (dbModel != null)
            {
                dbModel.firstName = firstName;
                dbModel.secondName = secondName;
                context.SaveChanges();
            } else
            {
                throw new Exception("Salsman with id = "+id+" was not found!");
            }
        }
    }
}
