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
    [Authorize(Roles ="Administrador")]
    public class CoordinadorCategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            var roleUserIdsQuery = from role in db.Roles
                                   where role.Name == "Coordinador"
                                   from user in role.Users
                                   select user.UserId;
            var users = db.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();

            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria");
            ViewBag.Id = new SelectList(users, "Id", "Email");
            return  View();
        }

        // POST: CoordinadorCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCoordCate,IdCategoria,Id")] CoordinadorCategorias coordinadorCategorias)
        {
            if (ModelState.IsValid)
            {
                db.CoordinadorCategorias.Add(coordinadorCategorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria", coordinadorCategorias.IdCategoria);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", coordinadorCategorias.Id);
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
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", coordinadorCategorias.Id);
            return View(coordinadorCategorias);
        }

        // POST: CoordinadorCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCoordCate,IdCategoria,Id")] CoordinadorCategorias coordinadorCategorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coordinadorCategorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria", coordinadorCategorias.IdCategoria);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", coordinadorCategorias.Id);
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
