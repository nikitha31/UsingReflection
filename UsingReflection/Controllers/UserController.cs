using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UsingReflection.Models;

namespace UsingReflection.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            Userss user = new Userss();
            ReflectionsEntities db = new ReflectionsEntities();

            user = db.Usersses.FirstOrDefault(us => us.UserName.Equals(loginModel.UserName) && us.Password.Equals(loginModel.Password));

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                Session["User"] = user;
                return RedirectToAction("List", "Employee");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserName"] = null;
            return RedirectToAction("Login");
        }
    }
}
