using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;
using TestApp.Utils;

namespace TestApp.Controllers
{
    public class SessionController : Controller
    {
        DatabaseWork db = new DatabaseWork();

        [Authorize(Roles ="Admin")]
        public ActionResult ChooseCinemaToAddSession()
        {
            try
            {
                db.ConnectionString = User.Identity.Name;
                List<Cinema> cinemas = db.SelectAllCinemas();
                for(int i=0;i<cinemas.Count;i++)
                {
                    List<Hall> halls = db.GetHallsByCinameName(cinemas[i].NameCinema);
                    if (halls.Count == 0)
                        cinemas.RemoveAt(i);
                }
                return View(cinemas);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
            
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AddSession(int idCinema)
        {
            db.ConnectionString = User.Identity.Name;
            ViewBag.Cinema = db.GetCinema(idCinema);
            ViewBag.Films = db.GetFilmsNames();
            return View(db.SelectAllHallsCinema(idCinema));
        }

        [HttpPost]
        public ActionResult AddSession(Session session, int idCinema)
        {
            db.ConnectionString = User.Identity.Name;
            ViewBag.Cinema = db.GetCinema(idCinema);
            ViewBag.Films = db.GetFilmsNames();
            try
            {
                if (ModelState.IsValid)
                {
                    List<TestApp.Models.Session> listSessions = db.GetSessionsByHallId((int)session.HallId);
                    Film filmSession = db.GetFilmWithoutTrailer((int)session.FilmId);
                    Session checkEndSession = session;
                    if((session.StartSession.Hour>22 || session.StartSession.Hour<9)
                        || (checkEndSession.StartSession.AddMinutes(filmSession.DurationMinutesFilm).Hour>22 ||
                        checkEndSession.StartSession.AddMinutes(filmSession.DurationMinutesFilm).Hour <9))
                    {
                        ModelState.AddModelError("", "Сеанс не укладывается в рабочий день: 09:00-22:00");
                        return View(db.SelectAllHallsCinema(idCinema));
                    }
                    foreach (var s in listSessions)
                    {
                        Session checkSession = session;
                        if (s.StartSession.Day==session.StartSession.Day)
                        {
                            if(s.FilmId==session.FilmId)
                            {
                                ModelState.AddModelError("", $"{session.StartSession.ToShortDateString()}" +
                                    " в данном зале уже будет проходить показ выбранного фильма");
                                return View(db.SelectAllHallsCinema(idCinema));

                            }
                        }
                        if(s.StartSession.Day==checkSession.StartSession.Day || s.StartSession.Day == checkSession.StartSession.AddDays(1).Day)
                        {
                            if(session.StartSession>s.StartSession && session.StartSession<s.StartSession.AddMinutes(s.Film.DurationMinutesFilm))
                            {
                                ModelState.AddModelError("", "В указанное время уже будет проходить сеанс, проверьте рассписание");
                                return View(db.SelectAllHallsCinema(idCinema));
                            }
                            if (checkSession.StartSession.AddMinutes(filmSession.DurationMinutesFilm) > s.StartSession 
                                && checkSession.StartSession.AddMinutes(filmSession.DurationMinutesFilm) < s.StartSession.AddMinutes(s.Film.DurationMinutesFilm))
                            {
                                ModelState.AddModelError("", "Фильм закончится во время сеанса, который уже записан, проверьте рассписание");
                                return View(db.SelectAllHallsCinema(idCinema));
                            }
                        }
                    }
                    db.AddSession(session);
                    return RedirectToAction("ChooseCinemaToAddSession");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(db.SelectAllHallsCinema(idCinema));
            }
            return View(db.SelectAllHallsCinema(idCinema));
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