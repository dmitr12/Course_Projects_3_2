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
        DatabaseWork db = new DatabaseWork("DefaultConnection");

        public ActionResult ChooseCinemaToAddSession()
        {
            return View(db.SelectAllCinemas());
        }

        public ActionResult AddSession(int idCinema)
        {
            ViewBag.Cinema = db.GetCinema(idCinema);
            ViewBag.Films = db.SelectAllFilms();
            return View(db.SelectAllHallsCinema(idCinema));
        }

        [HttpPost]
        public ActionResult AddSession(Session session)
        {
            db.AddSession(session);
            return Content("Good!");
        }

        [HttpPost]
        public ActionResult SessionSearch(int HallId)
        {
            List<Session> sessions = db.GetSessionsByHallId(HallId);
            return PartialView(sessions);
        }
    }
}