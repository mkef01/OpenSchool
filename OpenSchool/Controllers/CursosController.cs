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
    [Authorize(Roles = "Administrador, Coordinador,Profesor,Alumno")]
    public class CursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cursos
        [Authorize(Roles = "Administrador, Coordinador,Profesor,Alumno")]
        public ActionResult Index()
        {
            var user = db.UsuariosAsps.FirstOrDefault(a => a.UserName == User.Identity.Name);
            IQueryable<Cursos> cursos;
            if (User.IsInRole("Alumno"))
            {
                var IdCurso = db.RegistroCurso.Where(a => a.Id == user.Id).Select(a => a.IdCurso).ToList();
                cursos = db.Cursos.Include(c => c.EstadoCurso).Include(c => c.NivelCursos).Include(c => c.SeccionesCursos).Where(a=> IdCurso.Contains(a.IdCurso));
            }
            else if (User.IsInRole("Profesor"))
            {
                var IdCurso = db.ProfesoresCursos.Where(a => a.Id == user.Id).Select(a => a.IdCursos).ToList();
                cursos = db.Cursos.Include(c => c.EstadoCurso).Include(c => c.NivelCursos).Include(c => c.SeccionesCursos).Where(a => IdCurso.Contains(a.IdCurso));
            }
            else if (User.IsInRole("Coordinador"))
            {
                var IdCat = db.CoordinadorCategorias.Where(a => a.Id == user.Id).Select(a => a.IdCategoria).ToList();
                var IdCurso = db.CursosCategorias.Where(a=> IdCat.Contains(a.IdCategoria)).Select(a=> a.IdCurso).ToList();
                cursos = db.Cursos.Include(c => c.EstadoCurso).Include(c => c.NivelCursos).Include(c => c.SeccionesCursos).Where(a => IdCurso.Contains(a.IdCurso));
            }
            else
            {
                cursos = db.Cursos.Include(c => c.EstadoCurso).Include(c => c.NivelCursos).Include(c => c.SeccionesCursos);
            }
            return View(cursos.ToList());
        }

        // GET: Cursos/Details/5
        [Authorize(Roles = "Administrador, Coordinador")]
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
        [Authorize(Roles = "Administrador")]
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
        public ActionResult Create([Bind(Include = "IdCurso,Nombre,IdNivel,IdSeccion,IdEstCurs")] Cursos cursos)
        {
            cursos.FechaInicio = DateTime.Now;
            cursos.FechaFin = DateTime.Now;
            cursos.IdEstCurs = 1;
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
        [Authorize(Roles = "Administrador, Coordinador")]
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
            if (User.IsInRole("Coordinador"))
            {
                var curso = db.Cursos.Find(cursos.IdCurso);
                cursos.IdNivel = curso.IdNivel;
                cursos.IdSeccion = curso.IdSeccion;
                cursos.Nombre = curso.Nombre;
                cursos.IdEstCurs = curso.IdEstCurs;
            }
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

        // GET: Cursos/Edit/5
        [Authorize(Roles = "Coordinador")]
        public ActionResult EditCoordinador(int? id)
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
        public ActionResult EditCoordinador([Bind(Include = "IdCurso,Nombre,IdNivel,IdSeccion,IdEstCurs,FechaInicio,FechaFin")] Cursos cursos)
        {
            if (ModelState.IsValid)
            {
                Cursos cursosN = db.Cursos.Find(cursos.IdCurso);
                cursosN.FechaInicio = cursos.FechaInicio;
                cursosN.FechaInicio = cursos.FechaFin;
                db.Entry(cursosN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstCurs = new SelectList(db.EstadoCurso, "IdEstCurs", "Estado", cursos.IdEstCurs);
            ViewBag.IdNivel = new SelectList(db.NivelCursos, "IdNivel", "DetalleNivel", cursos.IdNivel);
            ViewBag.IdSeccion = new SelectList(db.SeccionesCursos, "IdSeccion", "DetalleSeccion", cursos.IdSeccion);
            return View(cursos);
        }

        // GET: Cursos/Delete/5
        [Authorize(Roles = "Administrador")]
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
