using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
    public class VerificarInscripcionController : Controller
    {
	    private ApplicationDbContext db = new ApplicationDbContext();
		// GET: VerificarInscripcion
		public ActionResult Index()
        {
			var CursosLista = new List<Cursos>();
	        var query = from CursosDisponibles in db.Cursos
		        join solicitudCurso in db.SolicitudCurso on CursosDisponibles.IdCurso equals solicitudCurso.IdCurso
		        where solicitudCurso.IdEstadoSol == 2
		        group new {CursosDisponibles} by new {CursosDisponibles.IdCurso,CursosDisponibles.Nombre}
		        into grupo
		        select new
		        {
			        id = grupo.Key.IdCurso,
					curso = grupo.Key.Nombre
		        };
	        var lista = query.ToList();
	        foreach (var item in lista)
	        {
		        CursosLista.Add(new Cursos()
		        {
					IdCurso = item.id,
					Nombre = item.curso
		        });
	        }
	        ViewBag.cursos = CursosLista;
            return View();
        }

	    [HttpPost]
	    public ActionResult Index(Cursos objCursos)
	    {
		    if (objCursos.IdCurso != 0)
		    {
				ViewBag.curso = objCursos.IdCurso;
			    return View("Asignar");
			}
		    return RedirectToAction("Index");
	    }

	    public ActionResult Borrar()
	    {
			var CursosLista = new List<Cursos>();
			var query = from CursosDisponibles in db.Cursos
				join solicitudCurso in db.SolicitudCurso on CursosDisponibles.IdCurso equals solicitudCurso.IdCurso
				where solicitudCurso.IdEstadoSol == 1
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
			    CursosLista.Add(new Cursos()
			    {
				    IdCurso = item.id,
				    Nombre = item.curso
			    });
		    }
		    ViewBag.borrar = CursosLista;
		    return View();
		}

	    [HttpPost]
	    public ActionResult Borrar(Cursos objCursos)
	    {
		    if (objCursos.IdCurso != 0)
		    {
				ViewBag.curso = objCursos.IdCurso;
			    return View("Eliminar");
			}
		    return RedirectToAction("Borrar");
		}

	    public ActionResult Asignar()
	    {
		    return View();
	    }

	    public ActionResult Eliminar()
	    {
		    return View();
	    }
    }
}