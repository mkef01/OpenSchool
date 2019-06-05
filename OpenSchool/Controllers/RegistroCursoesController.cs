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
    [Authorize(Roles = "Coordinador, Alumno")]
    public class RegistroCursoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegistroCursoes
        public ActionResult Index()
        {
            var registroCurso = db.RegistroCurso.Include(r => r.Cursos).Include(r => r.EstadoRegistro).Include(r => r.Usuarios);
            return View(registroCurso.ToList());
        }

        // GET: RegistroCursoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroCurso registroCurso = db.RegistroCurso.Find(id);
            if (registroCurso == null)
            {
                return HttpNotFound();
            }
            return View(registroCurso);
        }

        // GET: RegistroCursoes/Create
        public ActionResult Create()
        {
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre");
            ViewBag.IdEstado = new SelectList(db.EstadoRegistro, "IdEstado", "Estado");
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email");
            return View();
        }

        // POST: RegistroCursoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRegistroCurso,IdCurso,Id,IdEstado")] RegistroCurso registroCurso)
        {
            if (ModelState.IsValid)
            {
                db.RegistroCurso.Add(registroCurso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre", registroCurso.IdCurso);
            ViewBag.IdEstado = new SelectList(db.EstadoRegistro, "IdEstado", "Estado", registroCurso.IdEstado);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", registroCurso.Id);
            return View(registroCurso);
        }

        // GET: RegistroCursoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroCurso registroCurso = db.RegistroCurso.Find(id);
            if (registroCurso == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre", registroCurso.IdCurso);
            ViewBag.IdEstado = new SelectList(db.EstadoRegistro, "IdEstado", "Estado", registroCurso.IdEstado);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", registroCurso.Id);
            return View(registroCurso);
        }

        // POST: RegistroCursoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRegistroCurso,IdCurso,Id,IdEstado")] RegistroCurso registroCurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroCurso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre", registroCurso.IdCurso);
            ViewBag.IdEstado = new SelectList(db.EstadoRegistro, "IdEstado", "Estado", registroCurso.IdEstado);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", registroCurso.Id);
            return View(registroCurso);
        }

        // GET: RegistroCursoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroCurso registroCurso = db.RegistroCurso.Find(id);
            if (registroCurso == null)
            {
                return HttpNotFound();
            }
            return View(registroCurso);
        }

        // POST: RegistroCursoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegistroCurso registroCurso = db.RegistroCurso.Find(id);
            db.RegistroCurso.Remove(registroCurso);
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
