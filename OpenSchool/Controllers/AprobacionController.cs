using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;
using OpenSchool.Models.Modelo_Proyecto;

namespace OpenSchool.Controllers
{
    public class AprobacionController : Controller
    {
	    private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Aprobacion

	    [Authorize(Roles = "Profesor")]
		public ActionResult Index()
        {
	        Reporte objReporte = new Reporte();
			if (objReporte.Ingresado)
			{
				ViewBag.correcto = "SI";
			}
			else
			{
				ViewBag.correcto = "NO";
			}
	        Session["curso"] = objReporte;
			var usuariosAsp = db.UsuariosAsps.Single(x => x.Email == User.Identity.Name);
			var cursos = new List<Cursos>();
	        var query = from CursosDisponibles in db.Cursos
		        join profesor in db.ProfesoresCursos on CursosDisponibles.IdCurso equals profesor.IdCursos
		        join usuario in db.UsuariosAsps on profesor.Id equals usuario.Id 
		        where usuario.Id.Equals(usuariosAsp.Id)
		        group new { CursosDisponibles } by new { CursosDisponibles.IdCurso, CursosDisponibles.Nombre }
		        into grupo
		        select new
		        {
			        id = grupo.Key.IdCurso,
			        curso = grupo.Key.Nombre
		        };
	        var lista = query.ToList();
	        foreach (var item in lista)
	        {
		        cursos.Add(new Cursos()
		        {
			        IdCurso = item.id,
					Nombre = item.curso
		        });
	        }
	        ViewBag.curso = cursos;
			return View();
        }

	    [HttpPost]
	    public ActionResult Index(Cursos objCursos)
	    {
		    Reporte objReporte = Session["curso"] as Reporte;
			objReporte.IdCurso = objCursos.IdCurso;
			Session["curso"] = objReporte;
		    return RedirectToAction("Alumnos");
	    }

	    [Authorize(Roles = "Profesor")]
		public ActionResult Alumnos()
	    {
		    Reporte objReporte = Session["curso"] as Reporte;
			IList<Reporte> listalumnos = new List<Reporte>();
		    var query = from usuarios in db.UsuariosAsps
			    join registro in db.RegistroCurso on usuarios.Id equals registro.Id
			    join cursose in db.Cursos on registro.IdCurso equals cursose.IdCurso 
			    where cursose.IdCurso == objReporte.IdCurso && registro.IdEstado == 2
				select new
			    {
				    idusuario = usuarios.Id,
				    correo = usuarios.Email
			    };
		    var lista = query.ToList();
		    foreach (var item in lista)
		    {
			    listalumnos.Add(new Reporte()
			    {
				    IdAlumno = item.idusuario,
				    Email = item.correo
			    });
		    }
		    Session["curso"] = objReporte;
			return View(listalumnos);
	    }

	    [Authorize(Roles = "Profesor")]
		public ActionResult Redirect(int? id,string usuario)
	    {
		    if (usuario != null)
		    {
				Reporte objReporte = Session["curso"] as Reporte;
			    objReporte.IdAlumno = usuario;
			    Session["curso"] = objReporte;
				return RedirectToAction("Informe", "Dashboard");
			}
		    return RedirectToAction("Alumnos");
	    }

    }
}