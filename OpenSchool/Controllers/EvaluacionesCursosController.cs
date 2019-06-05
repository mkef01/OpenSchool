using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
    [Authorize(Roles = "Profesor, Alumno")]
    public class EvaluacionesCursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: EvaluacionesCursos
        public ActionResult Index(int? idCurso)
        {
            //var evaluacionesCursos = db.EvaluacionesCursos.Include(e => e.Cursos).Include(e => e.TiposEvaluaciones).Include(e => e.Usuarios);
            //return View(evaluacionesCursos.ToList());
            IQueryable<EvaluacionesCursos> evaluacion = null;
            if (idCurso == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.IsInRole("Alumno"))
            {
                var user = db.UsuariosAsps.FirstOrDefault(a => a.UserName == User.Identity.Name);
                var intentosAct = db.IntentosEvaluacion.Where(a => a.Id == user.Id).Count();
                var tiempo = DateTime.Now;
                evaluacion =
                    db.EvaluacionesCursos.Where(f => f.IdCurso == idCurso && f.FechaInicio <= tiempo && f.FechaFin >= tiempo && f.Intentos >= intentosAct);
                
            }
            else
            {
                evaluacion =
                    db.EvaluacionesCursos.Where(f => f.IdCurso == idCurso);
                if (evaluacion == null)
                {
                    return HttpNotFound();
                }
            }

            if (evaluacion == null)
            {
                return HttpNotFound();
            }
            
            return View(evaluacion);
        }

        // GET: EvaluacionesCursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluacionesCursos evaluacionesCursos = db.EvaluacionesCursos.Find(id);
            if (evaluacionesCursos == null)
            {
                return HttpNotFound();
            }
            return View(evaluacionesCursos);
        }

        // GET: MAESTRO DETALLE
        public async Task<ActionResult> DetalleEvaluacion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EvaluacionesCursos evaluaciones = await db.EvaluacionesCursos.FindAsync(id);

            if (evaluaciones == null)
            {
                return HttpNotFound();
            }

           
            return View(evaluaciones);
        }

        // GET: EvaluacionesCursos/Create
        [Authorize(Roles ="Profesor")]
        public ActionResult Create()
        {
            var user = db.UsuariosAsps.Where(a => a.UserName == User.Identity.Name).ToList();
            var B = user[0].Id;
            var cursosId = db.ProfesoresCursos.Where(a => a.Id == B).Select(a=> a.IdCursos).ToList();
            var cursos = db.Cursos.Where(a => cursosId.Contains(a.IdCurso));
            ViewBag.IdCurso = new SelectList(cursos, "IdCurso", "Nombre");
            ViewBag.IdTipo = new SelectList(db.TiposEvaluaciones, "IdTipo", "TipoEvaluacion");
            ViewBag.Id = new SelectList(user, "Id", "Email");
            return View();
        }

        // POST: EvaluacionesCursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEvaluacion,IdCurso,IdTipo,Id,Detalle,Intentos,Ponderacion,FechaInicio,FechaFin")] EvaluacionesCursos evaluacionesCursos)
        {
            if (ModelState.IsValid)
            {
                db.EvaluacionesCursos.Add(evaluacionesCursos);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Create", "PreguntasEvaluacions", new { IdEvaluacion = evaluacionesCursos.IdEvaluacion });
            }

            var user = db.UsuariosAsps.Where(a => a.UserName == User.Identity.Name).ToList();
            var cursosId = db.ProfesoresCursos.Where(a => a.Id == user[0].Id).Select(a => a.IdCursos).ToList();
            var cursos = db.Cursos.Where(a => cursosId.Contains(a.IdCurso));
            ViewBag.IdCurso = new SelectList(cursos, "IdCurso", "Nombre");
            ViewBag.IdTipo = new SelectList(db.TiposEvaluaciones, "IdTipo", "TipoEvaluacion");
            ViewBag.Id = new SelectList(user, "Id", "Email");
            return View(evaluacionesCursos);
        }

        [Authorize(Roles = "Profesor")]
        // GET: EvaluacionesCursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluacionesCursos evaluacionesCursos = db.EvaluacionesCursos.Find(id);
            if (evaluacionesCursos == null)
            {
                return HttpNotFound();
            }
            var user = db.UsuariosAsps.Where(a => a.UserName == User.Identity.Name).ToList();
            var usua = user[0].Id;
            var cursosId = db.ProfesoresCursos.Where(a => a.Id == usua).Select(a => a.IdCursos).ToList();
            var cursos = db.Cursos.Where(a => cursosId.Contains(a.IdCurso));
            ViewBag.IdCurso = new SelectList(cursos, "IdCurso", "Nombre");
            ViewBag.IdTipo = new SelectList(db.TiposEvaluaciones, "IdTipo", "TipoEvaluacion");
            ViewBag.Id = new SelectList(user, "Id", "Email");
            return View(evaluacionesCursos);
        }

        // POST: EvaluacionesCursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Profesor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEvaluacion,IdCurso,IdTipo,Id,Detalle,Intentos,Ponderacion,FechaInicio,FechaFin")] EvaluacionesCursos evaluacionesCursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluacionesCursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "EvaluacionesCursos", new { idCurso = evaluacionesCursos.IdCurso });
            }

            var user = db.UsuariosAsps.Where(a => a.UserName == User.Identity.Name).ToList();
            var usua = user[0].Id;
            var cursosId = db.ProfesoresCursos.Where(a => a.Id == usua).Select(a => a.IdCursos).ToList();
            var cursos = db.Cursos.Where(a => cursosId.Contains(a.IdCurso));
            ViewBag.IdCurso = new SelectList(cursos, "IdCurso", "Nombre");
            ViewBag.IdTipo = new SelectList(db.TiposEvaluaciones, "IdTipo", "TipoEvaluacion");
            ViewBag.Id = new SelectList(user, "Id", "Email");
            // return View(evaluacionesCursos);
            return RedirectToAction("Index","Cursos");
        }

        // GET: EvaluacionesCursos/Intento/5
        [Authorize(Roles ="Alumno")]
        public ActionResult Intento(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = db.Users.Where(u => u.UserName == User.Identity.Name).ToList();
            IntentosEvaluacion intentosEvaluacion = new IntentosEvaluacion();
            intentosEvaluacion.IdEvaluacion = id;
            intentosEvaluacion.Id = users[0].Id;
            intentosEvaluacion.FechaIntento = DateTime.Now;
            db.IntentosEvaluacion.Add(intentosEvaluacion);
            db.SaveChanges();
            var preguntasEvaluacions = db.PreguntasEvaluacion.Where(w => w.IdEvaluacion == id).ToList();
            preguntasEvaluacions.Shuffle();
            ViewBag.IntentoId = intentosEvaluacion.IdIntento;
            var evaluacion = db.EvaluacionesCursos.FirstOrDefault(f =>f.IdEvaluacion == id);
            var time = evaluacion?.FechaFin - evaluacion?.FechaInicio;
            if (time == null)
            {
                IntentosEvaluacion intento = db.IntentosEvaluacion.Find(intentosEvaluacion.Id);
                db.IntentosEvaluacion.Remove(intento);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Minutos = time.Value.Hours > 0 ? time.Value.Hours * 60 : time.Value.Minutes;
            ViewBag.Segundos = time.Value.Seconds;
            if (ViewBag.Minutos == 0 && ViewBag.Segundos == 0)
            {
                IntentosEvaluacion intento = db.IntentosEvaluacion.Find(intentosEvaluacion.Id);
                db.IntentosEvaluacion.Remove(intento);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(preguntasEvaluacions);
        }

        [Authorize(Roles = "Alumno")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Evaluar(FormCollection collection)
        {
            IntentosEvaluacion intentos = null;
            double nota = 0;
            double notaFinal = 0;
            foreach (var key in collection.Keys)
            {
                var data = collection[key.ToString()];
                if (key.ToString().Equals("id"))
                {
                    var id = int.Parse(data);
                    intentos = db.IntentosEvaluacion.FirstOrDefault(f => f.IdIntento == id);
                    if (intentos == null)
                    {
                        return HttpNotFound();
                    }
                }
                else if (!key.Equals("__RequestVerificationToken"))
                {
                    var idPregunta = int.Parse(key.ToString().Replace("[]", ""));

                    var tipoPregunta = db.PreguntasEvaluacion.FirstOrDefault(f => f.IdPregunta == idPregunta)?.TipoPregunta.Tipo_Pregunta;

                    switch (tipoPregunta)
                    {
                        case "TextArea":
                        case "Text":
                            {
                                nota = 0;
                                var respuestaCorrecta = db.RespuestasEvaluacion.FirstOrDefault(f => f.IdPregunta == idPregunta && f.Correcta);
                                var respuesta = collection[key.ToString()];
                                var notaRespuesta = respuestaCorrecta?.Respuesta.Trim() == respuesta.Trim()
                                    ? respuestaCorrecta.Ponderacion
                                    : 0;
                                nota += (double)notaRespuesta;
                                notaFinal += nota;
                                db.RespuestasIntento.Add(new RespuestasIntento
                                {
                                    IdIntento = intentos.IdIntento,
                                    Respuesta = respuesta,
                                    NotaAsignada = notaRespuesta,
                                    Observacion = "No Asignada"
                                });
                                break;
                            }

                        case "Checkbox":
                            {
                                var respuestas = collection[key.ToString()].Split(',');
                                var idRespuestas = respuestas.Select(int.Parse).ToArray();
                                var correctas = db.RespuestasEvaluacion.Where(w => w.IdPregunta == idPregunta && w.Correcta).ToList().Count;
                                foreach (var idRespuesta in idRespuestas)
                                {
                                    var respuestaCorrecta = db.RespuestasEvaluacion.FirstOrDefault(f => f.IdPregunta == idPregunta && f.Correcta);
                                    var notaRespuesta = respuestaCorrecta?.IdRespuesta == idRespuesta
                                        ? respuestaCorrecta.Ponderacion / correctas
                                        : 0;
                                    nota = 0;
                                    nota += (double)notaRespuesta;
                                    notaFinal += nota;
                                    db.RespuestasIntento.Add(new RespuestasIntento
                                    {
                                        IdIntento = intentos.IdIntento,
                                        Respuesta = data,
                                        NotaAsignada = notaRespuesta,
                                        Observacion = "No asignada"
                                    });
                                }

                                break;
                            }
                        default:
                            {
                                nota = 0;
                                var respuestaCorrecta = db.RespuestasEvaluacion.FirstOrDefault(f => f.IdPregunta == idPregunta && f.Correcta);
                                var idRespuesta = int.Parse(collection[key.ToString()]);
                                var respuesta = db.RespuestasEvaluacion.FirstOrDefault(f => f.IdRespuesta == idRespuesta);
                                var notaRespuesta = respuestaCorrecta?.IdRespuesta == idRespuesta
                                    ? respuestaCorrecta.Ponderacion
                                    : 0;
                                nota += (double)notaRespuesta;
                                notaFinal += nota;
                                db.RespuestasIntento.Add(new RespuestasIntento
                                {
                                    IdIntento = intentos.IdIntento,
                                    Respuesta = respuesta?.Respuesta,
                                    NotaAsignada = notaRespuesta,
                                    Observacion = "No Asignada"
                                });
                                break;
                            }
                    }
                }
            }
            await db.SaveChangesAsync();
            var intentoFinal = db.IntentosEvaluacion.Find(intentos.IdIntento);
            var preguntas = db.PreguntasEvaluacion.Where(a => a.IdEvaluacion == intentos.IdEvaluacion).Select(a=> a.Ponderacion).Sum();
            ViewBag.TotalPuntos = preguntas;
            ViewBag.NotaFinal = notaFinal;
            return View(intentos);
        }



        // GET: EvaluacionesCursos/Delete/5
        [Authorize(Roles = "Profesor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluacionesCursos evaluacionesCursos = db.EvaluacionesCursos.Find(id);
            if (evaluacionesCursos == null)
            {
                return HttpNotFound();
            }
            return View(evaluacionesCursos);
        }

        // POST: EvaluacionesCursos/Delete/5
        [Authorize(Roles = "Profesor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvaluacionesCursos evaluacionesCursos = db.EvaluacionesCursos.Find(id);
            db.EvaluacionesCursos.Remove(evaluacionesCursos);
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
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var provider = new RNGCryptoServiceProvider();
            var n = list.Count;
            while (n > 1)
            {
                var box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                var k = (box[0] % n);
                n--;
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }


}
