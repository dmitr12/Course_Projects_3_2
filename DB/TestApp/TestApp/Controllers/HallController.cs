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
            ViewBag.Cinemas = db.SelectAllCinemas();
            return View();
        }

        [HttpPost]
        public ActionResult AddHall(int idCinema, Hall hall)
        {
            db.AddHall(idCinema, hall);
            return Content("good");
        }

        [HttpPost]
        public ActionResult HallSearch(string cinemaName)
        {
            List<Hall> halls = db.GetHallsByCinameName(cinemaName);
            return PartialView(halls);
        }
    }
}