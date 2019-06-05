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
    public class TiposEvaluacionesController : Controller
    {
        private Model1 db = new Model1();

        // GET: TiposEvaluaciones
        public ActionResult Index()
        {
            return View(db.TiposEvaluaciones.ToList());
        }

        // GET: TiposEvaluaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposEvaluaciones tiposEvaluaciones = db.TiposEvaluaciones.Find(id);
            if (tiposEvaluaciones == null)
            {
                return HttpNotFound();
            }
            return View(tiposEvaluaciones);
        }

        // GET: TiposEvaluaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposEvaluaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipo,TipoEvaluacion")] TiposEvaluaciones tiposEvaluaciones)
        {
            if (ModelState.IsValid)
            {
                db.TiposEvaluaciones.Add(tiposEvaluaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiposEvaluaciones);
        }

        // GET: TiposEvaluaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposEvaluaciones tiposEvaluaciones = db.TiposEvaluaciones.Find(id);
            if (tiposEvaluaciones == null)
            {
                return HttpNotFound();
            }
            return View(tiposEvaluaciones);
        }

        // POST: TiposEvaluaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipo,TipoEvaluacion")] TiposEvaluaciones tiposEvaluaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposEvaluaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiposEvaluaciones);
        }

        // GET: TiposEvaluaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposEvaluaciones tiposEvaluaciones = db.TiposEvaluaciones.Find(id);
            if (tiposEvaluaciones == null)
            {
                return HttpNotFound();
            }
            return View(tiposEvaluaciones);
        }

        // POST: TiposEvaluaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiposEvaluaciones tiposEvaluaciones = db.TiposEvaluaciones.Find(id);
            db.TiposEvaluaciones.Remove(tiposEvaluaciones);
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
