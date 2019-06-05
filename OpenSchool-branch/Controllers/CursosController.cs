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
    public class CursosController : Controller
    {
        private Model1 db = new Model1();

        // GET: Cursos
        public ActionResult Index()
        {
            var cursos = db.Cursos.Include(c => c.EstadoCurso).Include(c => c.NivelCursos).Include(c => c.SeccionesCursos);
            return View(cursos.ToList());
        }

        // GET: Cursos/Details/5
        public ActionResult Details(int? id)
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

        // GET: Cursos/Create
        public ActionResult Create()
        {
            ViewBag.IdEstCurs = new SelectList(db.EstadoCurso, "IdEstCurs", "Estado");
            ViewBag.IdNivel = new SelectList(db.NivelCursos, "IdNivel", "DetalleNivel");
            ViewBag.IdSeccion = new SelectList(db.SeccionesCursos, "IdSeccion", "DetalleSeccion");
            return View();
        }

        // POST: Cursos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCurso,Nombre,IdNivel,IdSeccion,IdEstCurs,FechaInicio,FechaFin")] Cursos cursos)
        {
            if (ModelState.IsValid)
            {
                db.Cursos.Add(cursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEstCurs = new SelectList(db.EstadoCurso, "IdEstCurs", "Estado", cursos.IdEstCurs);
            ViewBag.IdNivel = new SelectList(db.NivelCursos, "IdNivel", "DetalleNivel", cursos.IdNivel);
            ViewBag.IdSeccion = new SelectList(db.SeccionesCursos, "IdSeccion", "DetalleSeccion", cursos.IdSeccion);
            return View(cursos);
        }

        // GET: Cursos/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.IdEstCurs = new SelectList(db.EstadoCurso, "IdEstCurs", "Estado", cursos.IdEstCurs);
            ViewBag.IdNivel = new SelectList(db.NivelCursos, "IdNivel", "DetalleNivel", cursos.IdNivel);
            ViewBag.IdSeccion = new SelectList(db.SeccionesCursos, "IdSeccion", "DetalleSeccion", cursos.IdSeccion);
            return View(cursos);
        }

        // POST: Cursos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCurso,Nombre,IdNivel,IdSeccion,IdEstCurs,FechaInicio,FechaFin")] Cursos cursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstCurs = new SelectList(db.EstadoCurso, "IdEstCurs", "Estado", cursos.IdEstCurs);
            ViewBag.IdNivel = new SelectList(db.NivelCursos, "IdNivel", "DetalleNivel", cursos.IdNivel);
            ViewBag.IdSeccion = new SelectList(db.SeccionesCursos, "IdSeccion", "DetalleSeccion", cursos.IdSeccion);
            return View(cursos);
        }

        // GET: Cursos/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cursos cursos = db.Cursos.Find(id);
            db.Cursos.Remove(cursos);
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
