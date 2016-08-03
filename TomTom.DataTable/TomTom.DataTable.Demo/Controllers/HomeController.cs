using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomTom.DataTable.Demo.Models;

namespace TomTom.DataTable.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new List<HomeModel>()
            {
                new HomeModel()
                {
                    Date = DateTime.Now,
                    Id = 3,
                    Name = "Some Name"
                }
            });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}