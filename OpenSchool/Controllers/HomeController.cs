using OpenSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenSchool.Controllers
{
    [RequireHttps]
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var roleUserIdsQuery = from role in db.Roles
                                   where role.Name == "Alumno"
                                   from user in role.Users
                                   select user.UserId;
            var users = from role in db.Roles
                        where role.Name == "Profesor"
                        from user in role.Users
                        select user.UserId;
            ViewBag.Alumnos = db.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).Count();
            ViewBag.Profesores = db.Users.Where(u => users.Contains(u.Id)).Count(); ;
            ViewBag.Visitantes = db.Visitante.Count();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}