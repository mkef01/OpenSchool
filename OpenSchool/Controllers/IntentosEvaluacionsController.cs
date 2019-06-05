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
    [Authorize(Roles =("Profesor, Alumno"))]
    public class IntentosEvaluacionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IntentosEvaluacions
        
        public ActionResult Index(int? Id)
        {
            var user = db.UsuariosAsps.FirstOrDefault(a=> a.UserName== User.Identity.Name);
            IQueryable<IntentosEvaluacion> intentosEvaluacion;
            if (User.IsInRole("Profesor"))
            {
                var evaluaciones = db.EvaluacionesCursos.Where(i => i.IdCurso == Id).Select(i => i.IdEvaluacion).ToList();
                intentosEvaluacion = db.IntentosEvaluacion.Include(i => i.EvaluacionesCursos).Include(i => i.Usuarios).Where(i=> evaluaciones.Contains(int.Parse(i.IdEvaluacion.ToString())));
            }
            else
            {
                var evaluaciones = db.EvaluacionesCursos.Where(i => i.IdCurso == Id).Select(i => i.IdEvaluacion).ToList();
                intentosEvaluacion = db.IntentosEvaluacion.Include(i => i.EvaluacionesCursos).Include(i => i.Usuarios).Where(i=>i.Id == user.Id && evaluaciones.Contains(int.Parse(i.IdEvaluacion.ToString())));

            }
            return View(intentosEvaluacion.ToList());
        }

        // GET: IntentosEvaluacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntentosEvaluacion intentosEvaluacion = db.IntentosEvaluacion.Find(id);
            if (intentosEvaluacion == null)
            {
                return HttpNotFound();
            }
            var preguntas = db.PreguntasEvaluacion.Where(a => a.IdEvaluacion == intentosEvaluacion.IdEvaluacion).Select(a => a.Ponderacion).Sum();
            var notaFinal = db.RespuestasIntento.Where(a => a.IdIntento == id).Select(a => a.NotaAsignada).Sum();
            ViewBag.TotalPuntos = preguntas;
            ViewBag.NotaFinal = notaFinal;
            return View(intentosEvaluacion);
        }

        // GET: IntentosEvaluacions/Create
        [Authorize(Roles ="Profesor")]
        public ActionResult Create()
        {
            ViewBag.IdEvaluacion = new SelectList(db.EvaluacionesCursos, "IdEvaluacion", "Id");
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email");
            return View();
        }

        // POST: IntentosEvaluacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Profesor")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdIntento,Id,IdEvaluacion,FechaIntento")] IntentosEvaluacion intentosEvaluacion)
        {
            if (ModelState.IsValid)
            {
                db.IntentosEvaluacion.Add(intentosEvaluacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEvaluacion = new SelectList(db.EvaluacionesCursos, "IdEvaluacion", "Id", intentosEvaluacion.IdEvaluacion);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", intentosEvaluacion.Id);
            return View(intentosEvaluacion);
        }

        // GET: IntentosEvaluacions/Edit/5
        [Authorize(Roles = "Profesor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntentosEvaluacion intentosEvaluacion = db.IntentosEvaluacion.Find(id);
            if (intentosEvaluacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEvaluacion = new SelectList(db.EvaluacionesCursos, "IdEvaluacion", "Id", intentosEvaluacion.IdEvaluacion);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", intentosEvaluacion.Id);
            return View(intentosEvaluacion);
        }

        // POST: IntentosEvaluacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Profesor")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdIntento,Id,IdEvaluacion,FechaIntento")] IntentosEvaluacion intentosEvaluacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intentosEvaluacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEvaluacion = new SelectList(db.EvaluacionesCursos, "IdEvaluacion", "Id", intentosEvaluacion.IdEvaluacion);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", intentosEvaluacion.Id);
            return View(intentosEvaluacion);
        }

        // GET: IntentosEvaluacions/Delete/5
        [Authorize(Roles = "Profesor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntentosEvaluacion intentosEvaluacion = db.IntentosEvaluacion.Find(id);
            if (intentosEvaluacion == null)
            {
                return HttpNotFound();
            }
            return View(intentosEvaluacion);
        }

        // POST: IntentosEvaluacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Profesor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IntentosEvaluacion intentosEvaluacion = db.IntentosEvaluacion.Find(id);
            db.IntentosEvaluacion.Remove(intentosEvaluacion);
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
