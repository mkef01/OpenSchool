using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
   // [Authorize(Roles = "Profesor")]
    public class PreguntasEvaluacionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PreguntasEvaluacions
        public ActionResult Index()
        {
            var preguntasEvaluacion = db.PreguntasEvaluacion.Include(p => p.EvaluacionesCursos).Include(p => p.TipoPregunta);
            return View(preguntasEvaluacion.ToList());
        }

        // GET: PreguntasEvaluacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntasEvaluacion preguntasEvaluacion = db.PreguntasEvaluacion.Find(id);
            if (preguntasEvaluacion == null)
            {
                return HttpNotFound();
            }
            return View(preguntasEvaluacion);
        }

        // GET: MAESTRO DETALLE
        public async Task<ActionResult> DetalleRespuestas(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PreguntasEvaluacion preguntasEvaluacion = await db.PreguntasEvaluacion.FindAsync(id);

            if (preguntasEvaluacion == null)
            {
                return HttpNotFound();
            }


            return View(preguntasEvaluacion);
        }
        // GET: PreguntasEvaluacions/Create
        public ActionResult Create(int? IdEvaluacion)
        {
            ViewBag.IdEvaluacion = new SelectList(db.EvaluacionesCursos, "IdEvaluacion", "IdEvaluacion");
            ViewBag.IdTipoPreg = new SelectList(db.TipoPregunta, "IdTipoPreg", "Tipo_Pregunta");
            return View();
        }

        // POST: PreguntasEvaluacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPregunta,IdEvaluacion,IdTipoPreg,Pregunta,Ponderacion")] PreguntasEvaluacion preguntasEvaluacion)
        {
            if (ModelState.IsValid)
            {
                db.PreguntasEvaluacion.Add(preguntasEvaluacion);
                db.SaveChanges();
                //return RedirectToAction("Index");
                //return RedirectToAction("DetalleEvaluacion", "EvaluacionesCursos", new { id = preguntasEvaluacion.IdEvaluacion });
                return RedirectToAction("Create", "RespuestasEvaluacions", new { IdPregunta = preguntasEvaluacion.IdPregunta });
            }

            ViewBag.IdEvaluacion = new SelectList(db.EvaluacionesCursos, "IdEvaluacion", "IdEvaluacion", preguntasEvaluacion.IdEvaluacion);
            ViewBag.IdTipoPreg = new SelectList(db.TipoPregunta, "IdTipoPreg", "Tipo_Pregunta", preguntasEvaluacion.IdTipoPreg);
            return View(preguntasEvaluacion);
        }

        // GET: PreguntasEvaluacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntasEvaluacion preguntasEvaluacion = db.PreguntasEvaluacion.Find(id);
            if (preguntasEvaluacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEvaluacion = new SelectList(db.EvaluacionesCursos, "IdEvaluacion", "IdEvaluacion", preguntasEvaluacion.IdEvaluacion);
            ViewBag.IdTipoPreg = new SelectList(db.TipoPregunta, "IdTipoPreg", "Tipo_Pregunta", preguntasEvaluacion.IdTipoPreg);
            return View(preguntasEvaluacion);
        }

        // POST: PreguntasEvaluacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPregunta,IdEvaluacion,IdTipoPreg,Pregunta,Ponderacion")] PreguntasEvaluacion preguntasEvaluacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preguntasEvaluacion).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("DetalleEvaluacion", "EvaluacionesCursos", new { id = preguntasEvaluacion.IdEvaluacion });
            }
            ViewBag.IdEvaluacion = new SelectList(db.EvaluacionesCursos, "IdEvaluacion", "IdEvaluacion", preguntasEvaluacion.IdEvaluacion);
            ViewBag.IdTipoPreg = new SelectList(db.TipoPregunta, "IdTipoPreg", "Tipo_Pregunta", preguntasEvaluacion.IdTipoPreg);
            return View(preguntasEvaluacion);
        }

        // GET: PreguntasEvaluacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntasEvaluacion preguntasEvaluacion = db.PreguntasEvaluacion.Find(id);
            if (preguntasEvaluacion == null)
            {
                return HttpNotFound();
            }
            return View(preguntasEvaluacion);
        }

        // POST: PreguntasEvaluacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PreguntasEvaluacion preguntasEvaluacion = db.PreguntasEvaluacion.Find(id);
            db.PreguntasEvaluacion.Remove(preguntasEvaluacion);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("DetalleEvaluacion", "EvaluacionesCursos", new { id = preguntasEvaluacion.IdEvaluacion });
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
