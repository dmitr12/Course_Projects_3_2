using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestApp.Models;
using TestApp.Utils;

namespace TestApp.Controllers
{
    public class AccountController : Controller
    {
        DatabaseWork db = new DatabaseWork("DefaultConnection");
        HashAlgorithm md5 = new HashAlgorithm();

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
                    model.Password = md5.GetHash(model.Password);
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
                    if (!md5.CheckHash(model.Password, user.Password))
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