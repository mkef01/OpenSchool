using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;
using OpenSchool.Models.Modelo_Proyecto;

namespace OpenSchool.Controllers
{
    public class AdministracionController : Controller
    {
	    public void Tipo()
	    {
			ViewBag.cargo = "admin";
		}
        public ActionResult Index()
        {
	        Tipo();
			return View();
        }
	    public ActionResult CrearNivel()
	    {
			CrearNivel modeloCrearNivel = new CrearNivel();
			Tipo();
		    return View(modeloCrearNivel);
	    }

	    [HttpPost]
	    public ActionResult CrearNivel(CrearNivel modeloDatos)
	    {
		    Tipo();
		    return View("Index");
	    }

	    public ActionResult CrearSeccion()
	    {
			CrearSeccion modeloCrearSeccion = new CrearSeccion();
			Tipo();
		    return View(modeloCrearSeccion);
	    }

	    [HttpPost]
	    public ActionResult CrearSeccion(CrearSeccion modeloSeccion)
	    {
		    Tipo();
		    return View("Index");
	    }

	    public ActionResult CrearProfesor()
	    {
		    CrearProfesor modeloCrearProfesor = new CrearProfesor();
			Tipo();
		    return View(modeloCrearProfesor);
	    }

	    [HttpPost]
	    public ActionResult CrearProfesor(CrearProfesor modeloProfesor)
	    {
			Tipo();
		    return View("Index");
	    }

	    public ActionResult CrearCoordinador()
	    {
		    Tipo();
			CrearCoordinador modeloCrearCoordinador = new CrearCoordinador();
		    return View(modeloCrearCoordinador);
	    }

	    [HttpPost]
	    public ActionResult CrearCoordinador(CrearCoordinador modeloCoordinador)
	    {
		    Tipo();
		    return View("Index");
	    }

	    public ActionResult CrearCurso()
	    {
			Tipo();

		    var nivel = new List<NivelCursos>();
			//Consulta LINQ XD
			nivel.Add(new NivelCursos()
			{
				DetalleNivel = "Curso Matematica Avanzada",
				IdNivel = 1
			});
		    ViewBag.nivel = nivel;

			var seccion = new List<SeccionesCursos>();
			//Consulta LINQ XD
			seccion.Add(new SeccionesCursos()
			{
				DetalleSeccion = "Primer año",
				IdSeccion = 1
			});
		    ViewBag.seccion = seccion;

			Cursos modeloCursos = new Cursos();
		    return View(modeloCursos);
	    }

	    [HttpPost]
	    public ActionResult CrearCurso(Cursos modeloCursos)
	    {
			Tipo();
		    return View("Index");
	    }

	    public ActionResult AsignarCoordinador()
	    {
		    Tipo();
			var coordinador = new List<Usuarios>();
		    //Consulta LINQ XD
			coordinador.Add(new Usuarios()
			{
				IdUsuario = 1,
				Nombre = "Kelly Villeda"
			});
		    ViewBag.coordinador = coordinador;

			var categoria = new List<CategoriasCursos>();
		    //Consulta LINQ XD
			categoria.Add(new CategoriasCursos()
			{
				IdCategoria = 1,
				DetalleCategoria = "Nuevo Ingreso"
			});
		    ViewBag.categoria = categoria;

			CoordinadorCategorias modeloCoordinadorCategorias = new CoordinadorCategorias();
		    return View(modeloCoordinadorCategorias);
	    }

	    [HttpPost]
	    public ActionResult AsignarCoordinador(CoordinadorCategorias modeloCategorias)
	    {
		    Tipo();
		    return View("Index");
	    }
	}
}