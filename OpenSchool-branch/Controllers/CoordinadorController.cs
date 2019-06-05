using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
    public class CoordinadorController : Controller
    {
	    private void Tipo()
	    {
			ViewBag.cargo = "coord";
		}
        // GET: Coordinador
        public ActionResult Index()
        {
			Tipo();
            return View();
        }

	    public ActionResult AsignarProfesor()
	    {
		    Tipo();
			ProfesoresCursos modeloProfesoresCursos = new ProfesoresCursos();

			var cursos = new List<Cursos>();
		    //Consulta LINQ XD
		    cursos.Add(new Cursos()
		    {
			    Nombre = "Curso Matematica Avanzada",
			    IdCurso = 1
		    });
		    ViewBag.cursos = cursos;

			var usuarios = new List<Usuarios>();
		    //Consulta LINQ XD
			usuarios.Add(new Usuarios()
			{
				Nombre = "Estefany Villanueva",
				IdUsuario = 1
			});
		    ViewBag.usuarios = usuarios;
		    return View(modeloProfesoresCursos);
	    }

	    [HttpPost]
	    public ActionResult AsignarProfesor(ProfesoresCursos modeloProfesoresCursos)
	    {
		    Tipo();
		    return View("Index");
	    }

	    public ActionResult CrearAlumno()
	    {
			Tipo();
		    Usuarios modeloUsuarios = new Usuarios();
		    return View(modeloUsuarios);
	    }

	    [HttpPost]
	    public ActionResult CrearAlumno(Usuarios modeloUsuarios)
	    {
		    Tipo();
		    return View("Index");
	    }

	    public ActionResult DuracionCurso()
	    {
		    Tipo();
			Cursos modeloCursos = new Cursos();

			var curso = new List<Cursos>();
		    //Consulta LINQ XD
			curso.Add(new Cursos()
			{
				IdCurso = 1,
				Nombre = "Matematica avanzada"
			});
		    ViewBag.curso = curso;

		    DateTime dia = DateTime.Today;
		    ViewBag.inicio = dia.ToString("yyyy-MM-dd");
		    ViewBag.fin = dia.AddDays(+1).ToString("yyyy-MM-dd");

		    return View(modeloCursos);
	    }

	    [HttpPost]
	    public ActionResult DuracionCurso(Cursos modeloCursos)
	    {
		    Tipo();
		    return View("Index");
	    }

	    public ActionResult InscripcioAlumnos()
	    {
		    return View();
	    }
	}
}