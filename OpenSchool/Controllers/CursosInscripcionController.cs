using OpenSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OpenSchool.Controllers
{
    [Authorize(Roles = "Alumno")]
    public class CursosInscripcionController : Controller
    {
        // GET: CursosInscripcion
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var Model = (from a in db.Cursos
                         join b in db.EstadoCurso on a.IdEstCurs equals b.IdEstCurs
                         join c in db.SeccionesCursos on a.IdSeccion equals c.IdSeccion
                         join d in db.SolicitudCurso on a.IdCurso equals d.IdCurso
                         join e in db.EstadoSolicitud on d.IdEstadoSol equals e.IdEstadoSol
                         select new SolicitudesInscripcion
                         {
                             Id = a.IdCurso,
                             Nombre = a.Nombre,
                             Seccion = c.DetalleSeccion,
                             EstCurs = b.Estado,
                             EstadoSoli = e.NEstadoSolicitud
                         }).ToList();

            return View(Model);
        }
        public ActionResult Enviado(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Solicitud = (from a in db.Cursos
                             join b in db.EstadoCurso on a.IdEstCurs equals b.IdEstCurs
                             join c in db.SeccionesCursos on a.IdSeccion equals c.IdSeccion
                             join d in db.SolicitudCurso on a.IdCurso equals d.IdCurso
                             join e in db.EstadoSolicitud on d.IdEstadoSol equals e.IdEstadoSol
                             where a.IdCurso == id
                             select new SolicitudesInscripcion
                             {
                                 Id = a.IdCurso,
                                 Nombre = a.Nombre,
                                 Seccion = c.DetalleSeccion,
                                 EstCurs = b.Estado,
                                 EstadoSoli = e.NEstadoSolicitud
                             }).ToList();
            if (Solicitud == null)
            {
                return HttpNotFound();
            }
            SolicitudesInscripcion sol = Solicitud[0];
            return View(sol);
        }

        //[HttpPost, ActionName("Aceptado")]
        //public ActionResult AceptadoConfirmed(int? id)
        //{

        //    return ();
           
        //}

        }
    }




