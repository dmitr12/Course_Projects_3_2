using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class HallController : Controller
    {
        DatabaseWork db = new DatabaseWork("DefaultConnection");

        public ActionResult AddHall()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HallSearch(string cinemaName)
        {
            List<Hall> halls = db.GetHallsByCinameName(cinemaName);
            return PartialView(halls);
        }
    }
}