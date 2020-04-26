using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class SessionController : Controller
    {
        DatabaseWork db = new DatabaseWork();

        public ActionResult ChooseCinemaToAddSession()
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.SelectAllCinemas());
        }

        public ActionResult AddSession(int idCinema)
        {
            db.ConnectionString = User.Identity.Name;
            ViewBag.Cinema = db.GetCinema(idCinema);
            ViewBag.Films = db.SelectAllFilms();
            return View(db.SelectAllHallsCinema(idCinema));
        }

        [HttpPost]
        public ActionResult AddSession(Session session)
        {
            db.ConnectionString = User.Identity.Name;
            db.AddSession(session);
            return Content("Good!");
        }

        [HttpPost]
        public ActionResult SessionSearch(int HallId)
        {
            db.ConnectionString = User.Identity.Name;
            List<Session> sessions = db.GetSessionsByHallId(HallId);
            return PartialView(sessions);
        }
    }
}