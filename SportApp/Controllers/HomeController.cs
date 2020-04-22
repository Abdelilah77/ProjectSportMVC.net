using SportApp.Context;
using SportApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DbCont db = new DbCont();
        public ActionResult MainPage()
        {
            return View(db.Admins.ToList());
        }
        public ActionResult LogeAsAadmin()
        { 
            ViewBag.Check = "ok";
            return View();
        }
        [HttpPost]
        public ActionResult LogeAsAadmin(Admin admin)
        {
           
            var ifNotNull = db.Admins.SingleOrDefault(C => C.UserName.Equals(admin.UserName) && C.Password.Equals(admin.Password));
            if (ifNotNull!=null)
            {
               
                Session["name"] = ifNotNull.Name;
                return RedirectToAction("Index", "Admins");
            }
            return View();
        }
        public ActionResult LogeAsAentre()
        {
            ViewBag.Check = "ok";
            return View();
        }
        [HttpPost]
        public ActionResult LogeAsAentre(Entraineur entraineur)
        {
            var ifNotNull = db.Entraineurs.SingleOrDefault(C => C.User_name.Equals(entraineur.User_name) && C.Password.Equals(entraineur.Password));
            if (ifNotNull != null)
            {

                Session["name"] = ifNotNull.Name;
                return RedirectToAction("Indexx", "Entraineurs");
            }
            return View();
        }
    }
}