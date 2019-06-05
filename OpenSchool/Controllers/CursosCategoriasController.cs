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
    [Authorize]
    public class CursosCategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CursosCategorias
        [Authorize]
        public ActionResult Index()
        {
            var cursosCategorias = db.CursosCategorias.Include(c => c.CategoriasCursos).Include(c => c.Cursos);
            
                var curso = cursosCategorias.Where(a => a.IdCurCate == 1);
            return View(cursosCategorias.ToList());
        }

        [Authorize(Roles ="Coordinador")]
        public ActionResult IndexCoord()
        {
            var cursosCategorias = db.CursosCategorias.Include(c => c.CategoriasCursos).Include(c => c.Cursos);
            if (User.IsInRole("Coordinador"))
            {
                var user = db.UsuariosAsps.Where(u => u.UserName == User.Identity.Name)
                              .ToList()[0];
                var cate = db.CoordinadorCategorias.Where(a => a.Id == user.Id).Select(a=> a.IdCategoria).ToList();
                
                var curso = cursosCategorias.Where(a => cate.Contains(a.IdCategoria));
                cursosCategorias = curso;
            }
            return View(cursosCategorias.ToList());
        }

        // GET: CursosCategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursosCategorias cursosCategorias = db.CursosCategorias.Find(id);
            if (cursosCategorias == null)
            {
                return HttpNotFound();
            }
            return View(cursosCategorias);
        }

        // GET: CursosCategorias/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria");
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre");
            return View();
        }

        // POST: CursosCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCurCate,IdCategoria,IdCurso")] CursosCategorias cursosCategorias)
        {
            if (ModelState.IsValid)
            {
                db.CursosCategorias.Add(cursosCategorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria", cursosCategorias.IdCategoria);
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre", cursosCategorias.IdCurso);
            return View(cursosCategorias);
        }

        // GET: CursosCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursosCategorias cursosCategorias = db.CursosCategorias.Find(id);
            if (cursosCategorias == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria", cursosCategorias.IdCategoria);
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre", cursosCategorias.IdCurso);
            return View(cursosCategorias);
        }

        // POST: CursosCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCurCate,IdCategoria,IdCurso")] CursosCategorias cursosCategorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursosCategorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.CategoriasCursos, "IdCategoria", "DetalleCategoria", cursosCategorias.IdCategoria);
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre", cursosCategorias.IdCurso);
            return View(cursosCategorias);
        }

        // GET: CursosCategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursosCategorias cursosCategorias = db.CursosCategorias.Find(id);
            if (cursosCategorias == null)
            {
                return HttpNotFound();
            }
            return View(cursosCategorias);
        }

        // POST: CursosCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CursosCategorias cursosCategorias = db.CursosCategorias.Find(id);
            db.CursosCategorias.Remove(cursosCategorias);
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
