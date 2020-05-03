using Oracle.ManagedDataAccess.Client;
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
        public ActionResult Index()
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.SelectAllCinemas());
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AddCinema()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCinema(Cinema cinema, Address address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ConnectionString = User.Identity.Name;
                    db.AddCinema(cinema, address);
                    return RedirectToAction("Index");
                }
            }
            catch(OracleException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(cinema);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult EditCinema(int idCinema)
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.GetCinema(idCinema));
        }

        [HttpPost]
        public ActionResult EditCinema(Cinema cinema, Address address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ConnectionString = User.Identity.Name;
                    db.EditCinema(cinema, address);
                    return RedirectToAction("Index");
                }
            }
            catch (OracleException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(cinema);
        }

        [HttpPost]
        public ActionResult GetAllCinemas(string nameCinema)
        {
            db.ConnectionString = User.Identity.Name;
            List<Cinema> cinemas = new List<Cinema>();
            if (String.IsNullOrEmpty(nameCinema))
                cinemas = db.SelectAllCinemas();
            else
            {
                Cinema item = db.GetCinemaByName(nameCinema);
                if (item != null)
                    cinemas.Add(item);
                else
                    cinemas = db.SelectAllCinemas();
            }
            return PartialView(cinemas);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult DeleteCinema(int idCinema)
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.GetCinema(idCinema));
        }

        [HttpPost]
        public ActionResult DeleteCinema(Cinema cinema)
        {
            db.ConnectionString = User.Identity.Name;
            try
            {
                foreach(Hall hall in db.GetHallsByCinameName(cinema.NameCinema))
                {
                    if (db.GetSessionsByHallId(hall.IdHall).Count != 0)
                    {
                        ModelState.AddModelError("", "Кинотеатр нельзя удалить, в зале кинотеатра проходят сеансы");
                        return View(cinema);
                    }
                }
                db.DeleteCinema(cinema.IdCinema, cinema.AddressId, db.GetHallsByCinameName(cinema.NameCinema));
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(cinema);
        }
    }
}