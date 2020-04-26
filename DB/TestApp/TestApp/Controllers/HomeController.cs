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
        DatabaseWork db = new DatabaseWork();

        [Authorize]
        public ActionResult Index()
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.SelectAllFilms());
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AddFilm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFilm(Film film, HttpPostedFileBase uploadImage)
        {
            if(ModelState.IsValid && uploadImage!=null)
            {
                db.ConnectionString = User.Identity.Name;
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