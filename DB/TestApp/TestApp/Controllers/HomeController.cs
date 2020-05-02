using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;
using TestApp.Utils;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        DatabaseWork db = new DatabaseWork();

        [Authorize]
        public ActionResult GetListFilms()
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.GetFilmsNames());
        }

        [Authorize(Roles ="User")]
        public ActionResult BuyTicketForFilm(int idHall, int idFilm, int idSession)
        {
            db.ConnectionString = User.Identity.Name;
            List<Seat> seats=db.GetSeatsOfHall(idHall);
            List<Sector> sectors = new List<Sector>();
            List<Ticket> tickets = db.GetAllTickets();
            List<int> ticketsSeats = new List<int>();
            foreach (Ticket t in tickets)
            {
                if(t.SessionId==idSession)
                    ticketsSeats.Add(t.SeatId);
            }
            Sector addedSector = new Sector();
            addedSector.Seats.Add(seats[0]);
            addedSector.IdSector = seats[0].SectorId;
            addedSector.NameSector = seats[0].Sector.NameSector;
            addedSector.CostSeat = seats[0].Sector.CostSeat;
            for (int i=1;i<seats.Count;i++)
            {
                if (addedSector.IdSector!=seats[i].SectorId)
                {
                    sectors.Add(addedSector);
                    addedSector = new Sector();
                    addedSector.IdSector = seats[i].SectorId;
                    addedSector.NameSector = seats[i].Sector.NameSector;
                    addedSector.CostSeat = seats[i].Sector.CostSeat;
                    addedSector.Seats.Add(seats[i]);
                    continue;
                }
                else
                    addedSector.Seats.Add(seats[i]);
            }
            sectors.Add(addedSector);
            ViewBag.TicketsSeats = ticketsSeats;
            ViewBag.IdFilm = idFilm;
            ViewBag.FilmDuration = db.GetFilmWithoutTrailer(idFilm).DurationMinutesFilm;
            ViewBag.IdSession = idSession;
            return View(sectors);
        }

        [HttpPost]
        public ActionResult BuyTicketForFilm(int sessionId, int seatId)
        {
            db.ConnectionString = User.Identity.Name;
            int buyer = db.GetUser(User.Identity.Name).IdUser;
            db.AddTicket(buyer, sessionId, seatId);
            return RedirectToAction("GetListFilms");
        }

        [Authorize(Roles ="User")]
        public ActionResult GetFilmForUser(int idFilm)
        {
            db.ConnectionString = User.Identity.Name;
            ViewBag.IdFilm = idFilm;
            return View(db.GetFilm(idFilm));
        }

        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.GetFilmsNames());
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AdminView()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AddFilm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFilm(ModelAddFilm film, HttpPostedFileBase uploadImage)
        {
            try
            {
                if (ModelState.IsValid && uploadImage != null)
                {
                    if(!System.IO.File.Exists(film.DirectoryTrailer + "\\" + film.FileTrailer))
                    {
                        ModelState.AddModelError("", "Указан неверный путь к файлу с видео");
                        return View(film);
                    }
                    db.ConnectionString = User.Identity.Name;
                    Film checkFilm = db.GetFilmByName(film.NameFilm);
                    if(checkFilm!=null)
                    {
                        ModelState.AddModelError("", "Фильм уже есть в базе данных");
                        return View(film);
                    }
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        film.Poster = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    db.AddFilm(film);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(film);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult DeleteFilm(int idFilm)
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.GetFilmWithoutTrailer(idFilm));
        }

        [HttpPost]
        public ActionResult DeleteFilm(Film film)
        {
            try
            {
                db.ConnectionString = User.Identity.Name;
                if (db.GetSessionsByFilmId(film.IdFilm).Count != 0)
                {
                    ModelState.AddModelError("", "Фильм нельзя удалить, уже имеется сеанс с этим фильмом");
                    return View(db.GetFilmWithoutTrailer(film.IdFilm));
                }
                db.DeleteFilm(film.IdFilm);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(db.GetFilmWithoutTrailer(film.IdFilm));
        }

        [Authorize(Roles ="Admin")]
        public ActionResult EditFilm(int idFilm)
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.GetFilmWithoutTrailer(idFilm));
        }

        [HttpPost]
        public ActionResult EditFilm(ModelAddFilm film, HttpPostedFileBase uploadImage)
        {
            db.ConnectionString = User.Identity.Name;
            try
            {
                if (ModelState.IsValid && uploadImage != null)
                {
                    if (!System.IO.File.Exists(film.DirectoryTrailer + "\\" + film.FileTrailer))
                    {
                        ModelState.AddModelError("", "Указан неверный путь к файлу с видео");
                        return View(db.GetFilmWithoutTrailer(film.IdFilm));
                    }
                    if (db.GetSessionsByFilmId(film.IdFilm).Count != 0)
                    {
                        ModelState.AddModelError("", "Фильм нельзя изменить, уже имеется сеанс с этим фильмом");
                        return View(db.GetFilmWithoutTrailer(film.IdFilm));
                    }
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        film.Poster = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    db.EditFilm(film);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(db.GetFilmWithoutTrailer(film.IdFilm));
        }

        [HttpPost]
        public ActionResult GetFilmInfo(int idFilm)
        {
            db.ConnectionString = User.Identity.Name;
            return PartialView(db.GetFilm(idFilm));
        }

        [HttpPost]
        public ActionResult GetSessionsByStartSession(string startDate)
        {
            db.ConnectionString = User.Identity.Name;
            startDate = TimerUtil.GetDateFormatForOracle(startDate);
            ViewBag.StartDate = startDate;
            List<string> filmNames = new List<string>();
            List<Session> filter = new List<Session>();
            foreach (Session s in db.GetSessionsByStartSession(startDate))
            {
                if (!filmNames.Contains(s.Film.NameFilm))
                {
                    filter.Add(s);
                    filmNames.Add(s.Film.NameFilm);
                }
            }
            return PartialView(filter);
        }

        [HttpPost]
        public ActionResult GetSessionsByFilm(int idFilm)
        {
            db.ConnectionString = User.Identity.Name;
            return PartialView(db.GetSessionsByFilmId(idFilm));
        }

        [HttpPost]
        public ActionResult GetSessionsForFilm(int idFilm)
        {
            db.ConnectionString = User.Identity.Name;
            return PartialView(db.GetSessionsByFilmId(idFilm));
        }
    }
}