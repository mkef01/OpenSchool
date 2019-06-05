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
    public class CoordinadorCategoriasController : Controller
    {
        private Model1 db = new Model1();

        // GET: CoordinadorCategorias
        public ActionResult Index()
        {
            var coordinadorCategorias = db.CoordinadorCategorias.Include(c => c.CategoriasCursos).Include(c => c.Usuarios);
            return View(coordinadorCategorias.ToList());
        }

        // GET: CoordinadorCategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoordinadorCategorias coordinadorCategorias = db.CoordinadorCategorias.Find(id);
            if (coordinadorCategorias == null)
            {
                return HttpNotFound();
            }
            return View(coordinadorCategorias);
        }

        // GET: CoordinadorCategorias/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria");
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre");
            return View();
        }

        // POST: CoordinadorCategorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCoordCate,IdCategoria,IdUsuario")] CoordinadorCategorias coordinadorCategorias)
        {
            if (ModelState.IsValid)
            {
                db.CoordinadorCategorias.Add(coordinadorCategorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria", coordinadorCategorias.IdCategoria);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", coordinadorCategorias.IdUsuario);
            return View(coordinadorCategorias);
        }

        // GET: CoordinadorCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoordinadorCategorias coordinadorCategorias = db.CoordinadorCategorias.Find(id);
            if (coordinadorCategorias == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria", coordinadorCategorias.IdCategoria);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", coordinadorCategorias.IdUsuario);
            return View(coordinadorCategorias);
        }

        // POST: CoordinadorCategorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCoordCate,IdCategoria,IdUsuario")] CoordinadorCategorias coordinadorCategorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coordinadorCategorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria", coordinadorCategorias.IdCategoria);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", coordinadorCategorias.IdUsuario);
            return View(coordinadorCategorias);
        }

        // GET: CoordinadorCategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoordinadorCategorias coordinadorCategorias = db.CoordinadorCategorias.Find(id);
            if (coordinadorCategorias == null)
            {
                return HttpNotFound();
            }
            return View(coordinadorCategorias);
        }

        // POST: CoordinadorCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoordinadorCategorias coordinadorCategorias = db.CoordinadorCategorias.Find(id);
            db.CoordinadorCategorias.Remove(coordinadorCategorias);
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
