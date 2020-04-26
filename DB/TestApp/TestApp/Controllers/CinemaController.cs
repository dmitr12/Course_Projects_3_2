using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class CinemaController : Controller
    {
        DatabaseWork db = new DatabaseWork();

        [Authorize(Roles ="Admin")]
        public ActionResult AddCinema()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCinema(Cinema cinema, Address address)
        {
            if(ModelState.IsValid)
            {
                db.ConnectionString = User.Identity.Name;
                db.AddCinema(cinema, address);
                return Content("Good!");
            }
            return View(cinema);
        }

        [HttpPost]
        public ActionResult GetAllCinemas()
        {
            db.ConnectionString = User.Identity.Name;
            return PartialView(db.SelectAllCinemas());
        }
    }
}