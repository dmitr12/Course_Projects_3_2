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

        [Authorize(Roles="Admin")]
        public ActionResult EditHall(int idHall)
        {
            db.ConnectionString = User.Identity.Name;
            ViewBag.Sectors = db.GetSectorsByHall(idHall).OrderBy(s=>s.StartRow).ToList();
            return View(db.GetHallById(idHall));
        }

        [HttpPost]
        public ActionResult EditHall(Hall hall)
        {
            db.ConnectionString = User.Identity.Name;
            ViewBag.Sectors = db.GetSectorsByHall(hall.IdHall).OrderBy(s => s.StartRow).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.GetSessionsByHallId(hall.IdHall).Count != 0)
                    {
                        ModelState.AddModelError("", "Зал нельзя изменить, в нем уже проходят сеансы");
                        return View(db.GetHallById(hall.IdHall));
                    }
                    List<string> namesSectors = new List<string>();
                    foreach (Sector sector in hall.Sectors.ToList())
                        namesSectors.Add(sector.NameSector);
                    if (namesSectors.Count() > namesSectors.Distinct().Count())
                    {
                        ModelState.AddModelError("", "Названия секторов не должны быть одинаковы");
                        return View(db.GetHallById(hall.IdHall));
                    }
                    foreach (Sector sector in hall.Sectors)
                    {
                        if (sector.EndRow <= sector.StartRow)
                        {
                            ModelState.AddModelError("", "Конечный ряд сектора меньше либо равен начальному");
                            return View(db.GetHallById(hall.IdHall));
                        }
                    }
                    for (int i = 0; i < hall.Sectors.Count; i++)
                    {
                        if (i > 0)
                        {
                            if (hall.Sectors.ToList()[i].StartRow != hall.Sectors.ToList()[i - 1].EndRow + 1)
                            {
                                ModelState.AddModelError("", "Первый ряд каждого сектора должен быть больше на 1, чем последний ряд предыдущего");
                                return View(db.GetHallById(hall.IdHall));
                            }
                        }
                    }
                    List<Sector> sc = db.GetSectorsByHall(hall.IdHall);
                    List<int> idSectors = new List<int>();
                    foreach (Sector s in sc)
                        idSectors.Add(s.IdSector);
                    foreach(Sector sctr in hall.Sectors)
                    {
                        if (sctr.IdSector == 0)
                            db.AddSector(hall.IdHall, sctr);
                        else if (idSectors.Contains(sctr.IdSector))
                        {
                            db.EditSector(hall.IdHall, sctr);
                            idSectors.Remove(sctr.IdSector);
                        }
                    }
                    if (idSectors.Count != 0)
                    {
                        foreach (int id in idSectors)
                            db.DeleteSector(id);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(db.GetHallById(hall.IdHall));
        }
    }
}