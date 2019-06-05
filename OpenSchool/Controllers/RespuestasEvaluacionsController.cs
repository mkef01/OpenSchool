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
    [Authorize(Roles ="Profesor")]
    public class RespuestasEvaluacionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RespuestasEvaluacions
        public ActionResult Index()
        {
            var respuestasEvaluacion = db.RespuestasEvaluacion.Include(r => r.PreguntasEvaluacion);
            return View(respuestasEvaluacion.ToList());
        }

        // GET: RespuestasEvaluacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RespuestasEvaluacion respuestasEvaluacion = db.RespuestasEvaluacion.Find(id);
            if (respuestasEvaluacion == null)
            {
                return HttpNotFound();
            }
            return View(respuestasEvaluacion);
        }

        // GET: RespuestasEvaluacions/Create
        public ActionResult Create(int? IdPregunta)
        {
            ViewBag.IdPregunta = new SelectList(db.PreguntasEvaluacion, "IdPregunta", "IdPregunta");
            return View();
        }

        // POST: RespuestasEvaluacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRespuesta,IdPregunta,Respuesta,Ponderacion,Correcta")] RespuestasEvaluacion respuestasEvaluacion)
        {
            if (ModelState.IsValid)
            {
                var ponderacion = db.PreguntasEvaluacion.Find(respuestasEvaluacion.IdPregunta);
                if (respuestasEvaluacion.Correcta)
                {
                    if (respuestasEvaluacion.Ponderacion > ponderacion.Ponderacion)
                    {
                        ModelState.AddModelError("Ponderacion", "La ponderacion de la respuesta no puede ser mayor que la de la pregunta.");
                        ViewBag.IdPregunta = new SelectList(db.PreguntasEvaluacion, "IdPregunta", "IdPregunta", respuestasEvaluacion.IdPregunta);
                        return View(respuestasEvaluacion);
                    }
                    else
                    {
                        var respuestas = db.RespuestasEvaluacion.Where(a => a.IdPregunta == ponderacion.IdPregunta).Select(a => a.Ponderacion);
                        decimal total = 0;
                        if (respuestas!= null)
                        {
                             total = respuestas.Sum() + respuestasEvaluacion.Ponderacion;
                        }
                        if(total> ponderacion.Ponderacion)
                        {
                            ModelState.AddModelError("Ponderacion", "En el conjunto de respuestas, su ponderación sobrepasa el nivel especificado en la pregunta.");
                            ViewBag.IdPregunta = new SelectList(db.PreguntasEvaluacion, "IdPregunta", "IdPregunta", respuestasEvaluacion.IdPregunta);
                            return View(respuestasEvaluacion);
                        }else if (total <= 0)
                        {
                            ModelState.AddModelError("Ponderacion", "La nota de una respuesta correcta no puede ser menor o igual a 0.");
                            ViewBag.IdPregunta = new SelectList(db.PreguntasEvaluacion, "IdPregunta", "IdPregunta", respuestasEvaluacion.IdPregunta);
                            return View(respuestasEvaluacion);
                        }
                        else
                        {
                            db.RespuestasEvaluacion.Add(respuestasEvaluacion);
                            db.SaveChanges();
                            return RedirectToAction("DetalleRespuestas", "PreguntasEvaluacions", new { id = respuestasEvaluacion.IdPregunta });
                        }
                    }
                }
                else
                {
                    if (respuestasEvaluacion.Ponderacion != 0)
                    {
                        ModelState.AddModelError("Ponderacion", "La ponderacion una respuesta incorrecta no puede ser mayor a 0.");
                        ViewBag.IdPregunta = new SelectList(db.PreguntasEvaluacion, "IdPregunta", "IdPregunta", respuestasEvaluacion.IdPregunta);
                        return View(respuestasEvaluacion);
                    }
                    else
                    {
                        db.RespuestasEvaluacion.Add(respuestasEvaluacion);
                        db.SaveChanges();
                        return RedirectToAction("DetalleRespuestas", "PreguntasEvaluacions", new { id = respuestasEvaluacion.IdPregunta });
                        
                    }
                }
                
            }
            else
            {
                ViewBag.IdPregunta = new SelectList(db.PreguntasEvaluacion, "IdPregunta", "IdPregunta", respuestasEvaluacion.IdPregunta);
                return View(respuestasEvaluacion);
            }
        }

        // GET: RespuestasEvaluacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RespuestasEvaluacion respuestasEvaluacion = db.RespuestasEvaluacion.Find(id);
            if (respuestasEvaluacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPregunta = new SelectList(db.PreguntasEvaluacion, "IdPregunta", "IdPregunta", respuestasEvaluacion.IdPregunta);
            return View(respuestasEvaluacion);
        }

        // POST: RespuestasEvaluacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRespuesta,IdPregunta,Respuesta,Ponderacion,Correcta")] RespuestasEvaluacion respuestasEvaluacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuestasEvaluacion).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("DetalleRespuestas", "PreguntasEvaluacions", new { id = respuestasEvaluacion.IdPregunta });

            }
            ViewBag.IdPregunta = new SelectList(db.PreguntasEvaluacion, "IdPregunta", "IdPregunta", respuestasEvaluacion.IdPregunta);
            return View(respuestasEvaluacion);
        }

        // GET: RespuestasEvaluacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RespuestasEvaluacion respuestasEvaluacion = db.RespuestasEvaluacion.Find(id);
            if (respuestasEvaluacion == null)
            {
                return HttpNotFound();
            }
            return View(respuestasEvaluacion);
        }

        // POST: RespuestasEvaluacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RespuestasEvaluacion respuestasEvaluacion = db.RespuestasEvaluacion.Find(id);
            db.RespuestasEvaluacion.Remove(respuestasEvaluacion);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("DetalleRespuestas", "PreguntasEvaluacions", new { id = respuestasEvaluacion.IdPregunta });

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
