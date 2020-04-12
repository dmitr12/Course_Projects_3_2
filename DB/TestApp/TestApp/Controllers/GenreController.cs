using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class GenreController : Controller
    {
        DatabaseWork db = new DatabaseWork("DefaultConnection");

        public ActionResult Index()
        {
            return View(db.SelectAllGenre());
        }

        public ActionResult ShowGenre(int idGenre)
        {
            return View(db.GetGenre(idGenre));
        }
    }
}