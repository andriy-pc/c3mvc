using Lab_06v1.App_Start;
using Lab_06v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_06v1.Controllers
{
    public class FirstLessonController : Controller
    {

        private EntitiesContext context = new EntitiesContext();

        // GET: FirstLesson
        public ActionResult Index()
        {
            return View(context);
        }

        /*[HttpPost]*/
        public ActionResult RemoveFromQueue(int possitionInQueue)
        {
            try
            {
                RemoveStudentFromDB(possitionInQueue);
                context.SaveChanges();
                return RedirectToAction("Index", "FirstLesson");
            } catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
            {
                return RedirectToAction("Error", "FirstLesson", new { errorMessage = "Probably you tried to delete from queue the student with the wrong id, no one was removed from queue" });
            }   
        }

        public string Error(string errorMessage)
        {
            return errorMessage;
        }

        private void RemoveStudentFromDB(int possitionInQueue)
        {
            Student s = new Models.Student(possitionInQueue);
            context.Students.Attach(s);
            context.Students.Remove(s);
        }

        [HttpPost]
        public ActionResult RemoveFromTheQueue(FormCollection form)
        {
            var allCheck = Request.Form["check"];
            if(allCheck != null)
            {
                string selected = allCheck.ToString();
                string[] selectedStudents = selected.Split(',');
                foreach (var selectedStudent in selectedStudents)
                {
                    RemoveStudentFromDB(int.Parse(selectedStudent));
                }
                context.SaveChanges();
                return RedirectToAction("Index", "FirstLesson");
            } 
            else
            {
                return RedirectToAction("Error", "FirstLesson", new { errorMessage = "Please select student to remove from queue first!"});
            }
        }

        [HttpPost]
        public ActionResult AddToTheQueue(FormCollection form)
        {
            var newStudentFirstName = Request.Form["firstName"];
            var newStudentSecondName = Request.Form["secondName"];

            if(newStudentFirstName != null && newStudentSecondName != null)
            {
                AddNewStudentToTheQueue(newStudentFirstName.ToString(), newStudentSecondName.ToString());
            }
            else
            {
                return RedirectToAction("Error", "FirstLesson", new { errorMessage = "Please insert first and second name of the student" });
            }
            return RedirectToAction("Index", "FirstLesson");
        }

        private void AddNewStudentToTheQueue(string firstName, string secondName)
        {
            Student s = new Models.Student(firstName, secondName);
            context.Students.Add(s);
            context.SaveChanges();
        }
    }
}