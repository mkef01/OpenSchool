using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
			Login modeloLogin = new Login();
            return View(modeloLogin);
        }
	    public ActionResult LogOut()
	    {
		   return View("Index");
	    }
	    [HttpPost]
	    public ActionResult Index(Login modeloDatos)
	    {
		    return RedirectToAction("Index", "Profesor");
	    }

	    [HttpPost]
	    public ActionResult Visita(Login modeloDatos)
	    {
		    Session["aloha"] = "estop";
		   return RedirectToAction("Index", "Coordinador");
	    }
    }
}