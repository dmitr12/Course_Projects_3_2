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
        DatabaseWork db = new DatabaseWork();

        public ActionResult Index()
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.SelectAllGenre());
        }

        public ActionResult ShowGenre(int idGenre)
        {
            db.ConnectionString = User.Identity.Name;
            return View(db.GetGenre(idGenre));
        }
    }
}