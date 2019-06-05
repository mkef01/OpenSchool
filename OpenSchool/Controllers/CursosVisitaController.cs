using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
    [AllowAnonymous]
    public class CursosVisitaController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // GET: Cursos
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }else if (Session["User"]!= null)
            {
                var cursos = db.Cursos.Include(c => c.EstadoCurso).Include(c => c.NivelCursos).Include(c => c.SeccionesCursos);
                return View(cursos.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Cursos/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }else if (Session["User"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cursos cursos = db.Cursos.Find(id);
                if (cursos == null)
                {
                    return HttpNotFound();
                }
                return View(cursos);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}