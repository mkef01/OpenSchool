using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
    public class CursoFechaController : Controller
    {
	    private ApplicationDbContext db = new ApplicationDbContext();
		// GET: CursoFecha
		public ActionResult Index()
        {
			var Curso = new List<Cursos>();
	        var query = from curso in db.Cursos
		        join cursose in db.SeccionesCursos on curso.IdSeccion equals cursose.IdSeccion
		        join nivelCursose in db.NivelCursos on curso.IdNivel equals nivelCursose.IdNivel 
		        where curso.FechaInicio == null && curso.FechaFin == null
		        select new
		        {
					id = curso.IdCurso,
					nom = curso.Nombre,
					seccion = cursose.DetalleSeccion,
					nivel = nivelCursose.DetalleNivel
		        };
	        var lista = query.ToList();
	        foreach (var item in lista)
	        {
		        Curso.Add(new Cursos()
		        {
			        IdCurso = item.id,
					Nombre = item.nom + "-" + item.nivel + "-" + item.seccion 
		        });
	        }
	        ViewBag.curso = Curso;
            return View();
        }
    }
}