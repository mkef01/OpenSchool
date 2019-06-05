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
    public class SeccionesCursosController : Controller
    {
        private Model1 db = new Model1();

        // GET: SeccionesCursos
        public ActionResult Index()
        {
            return View(db.SeccionesCursos.ToList());
        }

        // GET: SeccionesCursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeccionesCursos seccionesCursos = db.SeccionesCursos.Find(id);
            if (seccionesCursos == null)
            {
                return HttpNotFound();
            }
            return View(seccionesCursos);
        }

        // GET: SeccionesCursos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeccionesCursos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSeccion,DetalleSeccion")] SeccionesCursos seccionesCursos)
        {
            if (ModelState.IsValid)
            {
                db.SeccionesCursos.Add(seccionesCursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seccionesCursos);
        }

        // GET: SeccionesCursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeccionesCursos seccionesCursos = db.SeccionesCursos.Find(id);
            if (seccionesCursos == null)
            {
                return HttpNotFound();
            }
            return View(seccionesCursos);
        }

        // POST: SeccionesCursos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSeccion,DetalleSeccion")] SeccionesCursos seccionesCursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seccionesCursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seccionesCursos);
        }

        // GET: SeccionesCursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeccionesCursos seccionesCursos = db.SeccionesCursos.Find(id);
            if (seccionesCursos == null)
            {
                return HttpNotFound();
            }
            return View(seccionesCursos);
        }

        // POST: SeccionesCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SeccionesCursos seccionesCursos = db.SeccionesCursos.Find(id);
            db.SeccionesCursos.Remove(seccionesCursos);
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
