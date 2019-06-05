using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;
using OpenSchool.Models.Modelo_Proyecto;

namespace OpenSchool.Controllers
{
    public class AlumnoController : Controller
    {
	    private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Alumno
		[Authorize(Roles = "Alumno")]
		public ActionResult Cursos()
		{
			var usuariosAsp = db.UsuariosAsps.Single(x => x.Email == User.Identity.Name);
			IList<Cursos> detalleCursos = new List<Cursos>();
			var query = from cur in db.Cursos
						where cur.FechaInicio <= DateTime.Now && cur.FechaFin >= DateTime.Now
						select new
						{
							idcursos = cur.IdCurso,
							nombre = cur.Nombre,
							fechafin = cur.FechaFin,
							fechainicio = cur.FechaInicio
						};
			var lista = query.ToList();
			foreach (var item in lista)
			{
				detalleCursos.Add(new Cursos()
				{
					IdCurso = item.idcursos,
					Nombre = item.nombre,
					FechaInicio = item.fechainicio,
					FechaFin = item.fechafin
				});
			}
			var query2 = from tot in db.RegistroCurso
				where tot.IdEstado == 2 && usuariosAsp.Id == tot.Id
				select tot;
			int lista2 = query2.Count();
			if (lista2 > 3)
			{
				ViewBag.resultado = "Excedido";
				return View();
			}
			return View(detalleCursos);
        }

	    [HttpPost]
	    public ActionResult Cursos(string usuario,int curso)
	    {
		    var usuariosAsp = db.UsuariosAsps.Single(x => x.Email == usuario);
			var query = from solicitud in db.SolicitudCurso
			    join cur in db.Cursos on solicitud.IdCurso equals cur.IdCurso 
			    where solicitud.Id.Equals(usuariosAsp.Id) && solicitud.IdCurso.Equals(curso)
			    select new
			    {
				     nombre = cur.Nombre
			    };
		    var lista = query.ToList();
		    foreach (var item in lista)
		    {
				ViewBag.resultado = "Repetido";
			    ViewBag.curso = item.nombre;
			    return View();
		    }
			SolicitudCurso objSolicitudCurso = new SolicitudCurso();
		    objSolicitudCurso.IdCurso = Convert.ToInt32(curso);
		    objSolicitudCurso.Id = usuariosAsp.Id ;
		    objSolicitudCurso.IdEstadoSol = 2;
		    db.SolicitudCurso.Add(objSolicitudCurso);
			db.SaveChanges();
			ViewBag.resultado = "Exito";
		    return View();
	    }

	    [Authorize(Roles = "Alumno")]
		public ActionResult EstadoCurso()
	    {
		    var usuariosAsp = db.UsuariosAsps.Single(x => x.Email == User.Identity.Name);
			IList<Estado> detalleEstados = new List<Estado>();
		    var query = from cursose in db.Cursos
			    join solicitudCurso in db.SolicitudCurso on cursose.IdCurso equals solicitudCurso.IdCurso
			    join estadoSolicitud in db.EstadoSolicitud on solicitudCurso.IdEstadoSol equals estadoSolicitud.IdEstadoSol
			    where solicitudCurso.Id == usuariosAsp.Id
			    select new
			    {
					nombre = cursose.Nombre,
					estado = estadoSolicitud.NEstadoSolicitud,
					id = cursose.IdCurso
			    };
		    var lista = query.ToList();
		    foreach (var item in lista)
		    {
			    detalleEstados.Add(new Estado()
			    {
				    EstadoSolicitud = item.estado,
					IdCurso = item.id,
					NombreCurso = item.nombre
			    });
		    }	
		    return View(detalleEstados);
	    }

    }
}