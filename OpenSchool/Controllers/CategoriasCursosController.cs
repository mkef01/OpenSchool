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
    public class CategoriasCursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CategoriasCursos
        public ActionResult Index()
        {
            return View(db.CategoriasCursos.ToList());
        }

        // GET: CategoriasCursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriasCursos categoriasCursos = db.CategoriasCursos.Find(id);
            if (categoriasCursos == null)
            {
                return HttpNotFound();
            }
            return View(categoriasCursos);
        }

        // GET: CategoriasCursos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriasCursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategoria,DetalleCategoria")] CategoriasCursos categoriasCursos)
        {
            if (ModelState.IsValid)
            {
                db.CategoriasCursos.Add(categoriasCursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriasCursos);
        }

        // GET: CategoriasCursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriasCursos categoriasCursos = db.CategoriasCursos.Find(id);
            if (categoriasCursos == null)
            {
                return HttpNotFound();
            }
            return View(categoriasCursos);
        }

        // POST: CategoriasCursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategoria,DetalleCategoria")] CategoriasCursos categoriasCursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriasCursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriasCursos);
        }

        // GET: CategoriasCursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriasCursos categoriasCursos = db.CategoriasCursos.Find(id);
            if (categoriasCursos == null)
            {
                return HttpNotFound();
            }
            return View(categoriasCursos);
        }

        // POST: CategoriasCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriasCursos categoriasCursos = db.CategoriasCursos.Find(id);
            db.CategoriasCursos.Remove(categoriasCursos);
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
