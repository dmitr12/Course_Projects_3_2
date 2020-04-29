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
        public ActionResult GetListFilms()
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.SelectAllFilms());
        }

        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.GetAllFilmsWithoutTrailers());
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
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        film.Poster = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    db.AddFilm(film);
                    return RedirectToAction("GetListFilms");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(film);
            }
            return View(film);
        }

        [HttpPost]
        public ActionResult GetFilmInfo(int idFilm)
        {
            db.ConnectionString = User.Identity.Name;
            return PartialView(db.GetFilm(idFilm));
        }
    }
}