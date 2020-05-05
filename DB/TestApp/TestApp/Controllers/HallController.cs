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

        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            db.ConnectionString = User.Identity.Name;
            ViewBag.Cinemas = db.SelectAllCinemas();
            return View();
        }

        [Authorize(Roles ="Admin")]
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
            ViewBag.Cinemas = db.SelectAllCinemas();
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> namesSectors = new List<string>();
                    foreach (Sector sector in hall.Sectors.ToList())
                        namesSectors.Add(sector.NameSector);
                    if(namesSectors.Count()>namesSectors.Distinct().Count())
                    {
                        ModelState.AddModelError("", "Названия добавляемых секторов не должны быть одинаковы");
                        return View();
                    }
                    foreach(Sector sector in hall.Sectors)
                    {
                        if (sector.EndRow <= sector.StartRow)
                        {
                            ModelState.AddModelError("", "Конечный ряд сектора меньше либо равен начальному");
                            return View();
                        }
                    }
                    for (int i = 0; i < hall.Sectors.Count; i++)
                    {
                        if (i > 0)
                        {
                            if (hall.Sectors.ToList()[i].StartRow != hall.Sectors.ToList()[i - 1].EndRow+1)
                            {
                                ModelState.AddModelError("", "Первый ряд каждого сектора должен быть больше на 1, чем последний ряд предыдущего");
                                return View();
                            }
                        }
                    }
                    db.AddHall(idCinema, hall);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        [HttpPost]
        public ActionResult HallSearch(int idCinema)
        {
            db.ConnectionString = User.Identity.Name;
            List<Hall> halls = db.SelectAllHallsCinema(idCinema);
            return PartialView(halls);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult DeleteHall(int? idHall)
        {
            ViewBag.IdHall = idHall;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteHall(int idHall)
        {
            db.ConnectionString = User.Identity.Name;
            ViewBag.IdHall = idHall;
            try
            {
                if (db.GetSessionsByHallId(idHall).Count == 0)
                {
                    db.DeleteHall(idHall);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Зал нельзя удалить, в нем уже будет проходить сеанс");
                    return View();
                }

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}