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
    public class ProfesoresCursosController : Controller
    {
        private Model1 db = new Model1();

        // GET: ProfesoresCursos
        public ActionResult Index()
        {
            var profesoresCursos = db.ProfesoresCursos.Include(p => p.Cursos).Include(p => p.Usuarios);
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
            ViewBag.IdCursos = new SelectList(db.Cursos, "IdCurso", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre");
            return View();
        }

        // POST: ProfesoresCursos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProfCur,IdCursos,IdUsuario")] ProfesoresCursos profesoresCursos)
        {
            if (ModelState.IsValid)
            {
                db.ProfesoresCursos.Add(profesoresCursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCursos = new SelectList(db.Cursos, "IdCurso", "Nombre", profesoresCursos.IdCursos);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", profesoresCursos.IdUsuario);
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
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", profesoresCursos.IdUsuario);
            return View(profesoresCursos);
        }

        // POST: ProfesoresCursos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProfCur,IdCursos,IdUsuario")] ProfesoresCursos profesoresCursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesoresCursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCursos = new SelectList(db.Cursos, "IdCurso", "Nombre", profesoresCursos.IdCursos);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", profesoresCursos.IdUsuario);
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
