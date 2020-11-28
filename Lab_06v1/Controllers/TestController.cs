using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_06v1.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            ViewBag.Message = "Hello, MVC!";
            Console.WriteLine("Hello, MVC!");
            return View();
        }

        //action method
        [NonAction] //to prevent usage if the methd from browser
        public int Addint(int x, int y)
        {
            return x + y;
        }

    }
}