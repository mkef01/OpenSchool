using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using OpenSchool.Models;
using OpenSchool.Models.Modelo_Proyecto;

namespace OpenSchool.Controllers
{
    public class AgregarAlumnosController : ApiController
    {
	    private ApplicationDbContext db = new ApplicationDbContext();

	    public IEnumerable<ApiAgregar> Get(int? id)
	    {
		    List<ApiAgregar> result = new List<ApiAgregar>();
		    var query = from alum in db.Cursos
			    join solicitudCurso in db.SolicitudCurso on alum.IdCurso equals solicitudCurso.IdCurso
			    join usuariosAsp in db.UsuariosAsps on solicitudCurso.Id equals usuariosAsp.Id
			    join estadoDbSolicitud in db.EstadoSolicitud on solicitudCurso.IdEstadoSol equals estadoDbSolicitud.IdEstadoSol
			    where estadoDbSolicitud.IdEstadoSol == 2 && alum.IdCurso == id
			    select new
			    {
					 correo = usuariosAsp.Email
			    };
		    var lista = query.ToList();
		    foreach (var item in lista)
		    {
				var usuariosAsp = db.UsuariosAsps.Single(x => x.Email == item.correo);
			    var query2 = from usu in db.UsuariosAsps
				    join curso in db.RegistroCurso on usu.Id equals curso.Id
				    join registro in db.EstadoRegistro on curso.IdEstado equals registro.IdEstado
				    where registro.IdEstado == 2 && usu.Id == usuariosAsp.Id
				    select usu;
			    var total = query2.Count();
			    if (total < 3)
			    {
				    result.Add(new ApiAgregar()
				    {
					    Usuario = item.correo,
						TotalCursos = total
				    });
			    }
		    }
		    return result.AsEnumerable();
	    }

		[HttpGet]
		[Route("api/agregaralumnos/eliminar/{id}")]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		public IEnumerable<ApiAgregar> eliminar(int? id)
		{
			List<ApiAgregar> result = new List<ApiAgregar>();
			var query = from alum in db.Cursos
				join solicitudCurso in db.SolicitudCurso on alum.IdCurso equals solicitudCurso.IdCurso
				join usuariosAsp in db.UsuariosAsps on solicitudCurso.Id equals usuariosAsp.Id
				join estadoDbSolicitud in db.EstadoSolicitud on solicitudCurso.IdEstadoSol equals estadoDbSolicitud.IdEstadoSol
				where estadoDbSolicitud.IdEstadoSol == 1 && alum.IdCurso == id
				select new
				{
					correo = usuariosAsp.Email
				};
			var lista = query.ToList();
			foreach (var item in lista)
			{
				var usuariosAsp = db.UsuariosAsps.Single(x => x.Email == item.correo);
				var query2 = from usu in db.UsuariosAsps
					join curso in db.RegistroCurso on usu.Id equals curso.Id
					join registro in db.EstadoRegistro on curso.IdEstado equals registro.IdEstado
					where registro.IdEstado == 2 && usu.Id == usuariosAsp.Id
					select usu;
				var total = query2.Count();
				if (1 == 1)
				{
					result.Add(new ApiAgregar()
					{
						Usuario = item.correo,
						TotalCursos = total
					});
				}
			}
			return result.AsEnumerable();
		}

		[HttpPost]
	    [Route("api/agregaralumnos/post")]
	    [EnableCors(origins: "*", headers: "*", methods: "*")]
		public string Post(IngresoAPI obj)
	    {
			var usuariosAsp = db.UsuariosAsps.Single(x => x.Email == obj.Email);
		    SolicitudCurso objSolicitudCurso = new SolicitudCurso();
		    var query = from solicitud in db.SolicitudCurso
			    where solicitud.IdCurso == obj.Curso && solicitud.Id == usuariosAsp.Id
			    select new
			    {
					id = solicitud.IdSoliCurso,
					curso = solicitud.IdCurso,
					usuario = solicitud.Id,
					estado = solicitud.IdEstadoSol
			    };
		    var lista = query.ToList();
		    foreach (var item in lista)
		    {
			    objSolicitudCurso.IdSoliCurso = item.id;
			    objSolicitudCurso.IdCurso = item.curso;
			    objSolicitudCurso.Id = item.usuario;
			    objSolicitudCurso.IdEstadoSol = 1;
		    }
		    db.Entry(objSolicitudCurso).State = EntityState.Modified;
		    db.SaveChanges();
			//*********************************************************************************//
			RegistroCurso objRegistroCurso = new RegistroCurso();
		    objRegistroCurso.IdCurso = obj.Curso;
		    objRegistroCurso.Id = usuariosAsp.Id;
		    objRegistroCurso.IdEstado = 2;
		    db.RegistroCurso.Add(objRegistroCurso);
		    db.SaveChanges();
		    return "OK";
	    }

		[HttpPost]
		[Route("api/agregaralumnos/delete")]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		public string Delete(IngresoAPI obj)
		{
			var usuariosAsp = db.UsuariosAsps.Single(x => x.Email == obj.Email);
			SolicitudCurso objSolicitudCurso = new SolicitudCurso();
			var query = from solicitud in db.SolicitudCurso
				where solicitud.IdCurso == obj.Curso && solicitud.Id == usuariosAsp.Id
				select new
				{
					id = solicitud.IdSoliCurso,
					curso = solicitud.IdCurso,
					usuario = solicitud.Id,
					estado = solicitud.IdEstadoSol
				};
			var lista = query.ToList();
			foreach (var item in lista)
			{
				objSolicitudCurso.IdSoliCurso = item.id;
				objSolicitudCurso.IdCurso = item.curso;
				objSolicitudCurso.Id = item.usuario;
				objSolicitudCurso.IdEstadoSol = 2;
			}
			db.Entry(objSolicitudCurso).State = EntityState.Modified;
			db.SaveChanges();
			//*********************************************************************************//
			RegistroCurso objRegistroCurso = new RegistroCurso();
			var query2 = from registro in db.RegistroCurso
				where registro.IdCurso == obj.Curso && registro.Id == usuariosAsp.Id && registro.IdEstado == 2
				select new
				{
					idregistro = registro.IdRegistroCurso,
					idcuros = registro.IdCurso,
					usuarioid = registro.Id,
					estado = registro.IdEstado
				};
			var lista2 = query2.ToList();
			foreach (var item in lista2)
			{
				objRegistroCurso.IdRegistroCurso = item.idregistro;
				objRegistroCurso.IdCurso = item.idcuros;
				objRegistroCurso.Id = item.usuarioid;
				objRegistroCurso.IdEstado = item.estado;
			}
			db.Entry(objRegistroCurso).State = EntityState.Deleted;
			db.SaveChanges();
			return "OK";
		}
	}
}
