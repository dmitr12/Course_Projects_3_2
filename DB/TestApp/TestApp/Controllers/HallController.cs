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
        DatabaseWork db = new DatabaseWork();

        public ActionResult AddHall()
        {
            db.ConnectionString = User.Identity.Name;
            ViewBag.Cinemas = db.SelectAllCinemas();
            return View();
        }

        [HttpPost]
        public ActionResult AddHall(int idCinema, Hall hall)
        {
            db.ConnectionString = User.Identity.Name;
            db.AddHall(idCinema, hall);
            return Content("good");
        }

        [HttpPost]
        public ActionResult HallSearch(string cinemaName)
        {
            db.ConnectionString = User.Identity.Name;
            List<Hall> halls = db.GetHallsByCinameName(cinemaName);
            return PartialView(halls);
        }
    }
}