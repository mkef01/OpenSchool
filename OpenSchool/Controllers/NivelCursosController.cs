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
    [Authorize(Roles = "Administrador")]
    public class NivelCursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NivelCursos
        public ActionResult Index()
        {
            return View(db.NivelCursos.ToList());
        }

        // GET: NivelCursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelCursos nivelCursos = db.NivelCursos.Find(id);
            if (nivelCursos == null)
            {
                return HttpNotFound();
            }
            return View(nivelCursos);
        }

        // GET: NivelCursos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NivelCursos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNivel,DetalleNivel")] NivelCursos nivelCursos)
        {
            if (ModelState.IsValid)
            {
                db.NivelCursos.Add(nivelCursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nivelCursos);
        }

        // GET: NivelCursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelCursos nivelCursos = db.NivelCursos.Find(id);
            if (nivelCursos == null)
            {
                return HttpNotFound();
            }
            return View(nivelCursos);
        }

        // POST: NivelCursos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNivel,DetalleNivel")] NivelCursos nivelCursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nivelCursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nivelCursos);
        }

        // GET: NivelCursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelCursos nivelCursos = db.NivelCursos.Find(id);
            if (nivelCursos == null)
            {
                return HttpNotFound();
            }
            return View(nivelCursos);
        }

        // POST: NivelCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NivelCursos nivelCursos = db.NivelCursos.Find(id);
            db.NivelCursos.Remove(nivelCursos);
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
