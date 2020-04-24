﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class CinemaController : Controller
    {
        DatabaseWork db = new DatabaseWork("DefaultConnection");

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
                db.AddCinema(cinema, address);
                return Content("Good!");
            }
            return View(cinema);
        }

        [HttpPost]
        public ActionResult GetAllCinemas()
        {
            return PartialView(db.SelectAllCinemas());
        }
    }
}