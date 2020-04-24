using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class AccountController : Controller
    {
        DatabaseWork db = new DatabaseWork("DefaultConnection");

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.GetUser(model.Login);
                if (user == null)
                {
                    db.AddUser(model);
                    user = db.GetUser(model.Login);
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.GetUser(model.Login);
                if (user != null)
                {
                    if (user.Password != model.Password)
                    {
                        ModelState.AddModelError("", "Неправильный пароль");
                        return View(model);
                    }
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Пользователь не зарегистрирован");
            }
            return View(model);
        }
    }
}