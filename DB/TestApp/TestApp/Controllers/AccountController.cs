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
        DatabaseWork db = new DatabaseWork();
        HashAlgorithm md5 = new HashAlgorithm();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                db.ConnectionString = "";
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
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                db.ConnectionString = "";
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

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");      
        }

        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditPassword(EditUserPassword model)
        {
            if (ModelState.IsValid)
            {
                db.ConnectionString = "";
                User user = db.GetUser(model.Login);
                if (user != null)
                {
                    if(!md5.CheckHash(model.OldPassword, user.Password))
                    {
                        ModelState.AddModelError("", "Неверный старый пароль");
                        return View(model);
                    }
                    model.NewPassword = md5.GetHash(model.NewPassword);
                    db.EditUserPassword(model);
                    return RedirectToAction("Login");
                }
                else
                    ModelState.AddModelError("", "Пользователь не зарегистрирован");
            }
            return View(model);
        }
    }
}