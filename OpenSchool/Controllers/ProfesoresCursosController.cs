using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
    [Authorize(Roles = "Coordinador")]
    public class ProfesoresCursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProfesoresCursos
        public ActionResult Index()
        {
            var user = db.UsuariosAsps.Where(u => u.UserName == User.Identity.Name)
                             .ToList()[0];
            var cate = db.CoordinadorCategorias.Where(a => a.Id == user.Id).Select(a => a.IdCategoria).ToList();

            var cursos = db.CursosCategorias.Where(a => cate.Contains(a.IdCategoria)).Select(a => a.IdCurso).ToList();

            var profesoresCursos = db.ProfesoresCursos.Include(p => p.Cursos).Include(p => p.Usuarios).Where(p=> cursos.Contains(p.IdCursos));
            return View(profesoresCursos.ToList());
        }

        // GET: ProfesoresCursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfesoresCursos profesoresCursos = db.ProfesoresCursos.Find(id);
            if (profesoresCursos == null)
            {
                return HttpNotFound();
            }
            return View(profesoresCursos);
        }

        // GET: ProfesoresCursos/Create
        public ActionResult Create()
        {
            var roleUserIdsQuery = from role in db.Roles
                                   where role.Name == "Profesor"
                                   from user1 in role.Users
                                   select user1.UserId;
            var users = db.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();
            var user = db.UsuariosAsps.Where(u => u.UserName == User.Identity.Name)
                             .ToList()[0];
            var cate = db.CoordinadorCategorias.Where(a => a.Id == user.Id).Select(a => a.IdCategoria).ToList();

            var cursos = db.CursosCategorias.Where(a => cate.Contains(a.IdCategoria)).Select(a=> a.IdCurso).ToList();
            var curso = db.Cursos.Where(a => cursos.Contains(a.IdCurso));
            ViewBag.IdCursos = new SelectList(curso, "IdCurso", "Nombre");
            ViewBag.Id = new SelectList(users, "Id", "Email");
            return View();
        }

        // POST: ProfesoresCursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProfCur,IdCursos,Id")] ProfesoresCursos profesoresCursos)
        {
            if (ModelState.IsValid)
            {
                db.ProfesoresCursos.Add(profesoresCursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCursos = new SelectList(db.Cursos, "IdCurso", "Nombre", profesoresCursos.IdCursos);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", profesoresCursos.Id);
            return View(profesoresCursos);
        }

        // GET: ProfesoresCursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfesoresCursos profesoresCursos = db.ProfesoresCursos.Find(id);
            if (profesoresCursos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCursos = new SelectList(db.Cursos, "IdCurso", "Nombre", profesoresCursos.IdCursos);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", profesoresCursos.Id);
            return View(profesoresCursos);
        }

        // POST: ProfesoresCursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProfCur,IdCursos,Id")] ProfesoresCursos profesoresCursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesoresCursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCursos = new SelectList(db.Cursos, "IdCurso", "Nombre", profesoresCursos.IdCursos);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", profesoresCursos.Id);
            return View(profesoresCursos);
        }

        // GET: ProfesoresCursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfesoresCursos profesoresCursos = db.ProfesoresCursos.Find(id);
            if (profesoresCursos == null)
            {
                return HttpNotFound();
            }
            return View(profesoresCursos);
        }

        // POST: ProfesoresCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfesoresCursos profesoresCursos = db.ProfesoresCursos.Find(id);
            db.ProfesoresCursos.Remove(profesoresCursos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
