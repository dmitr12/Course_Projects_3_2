using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        DatabaseWork db = new DatabaseWork("DefaultConnection");

        [Authorize(Roles ="User")]
        public ActionResult Index()
        {
            return View(db.SelectAllFilms());
        }

        public ActionResult AddFilm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFilm(Film film, HttpPostedFileBase uploadImage)
        {
            if(ModelState.IsValid && uploadImage!=null)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    film.Poster = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                db.AddFilm(film);
                return RedirectToAction("Index");
            }
            return View(film);
        }
    }
}