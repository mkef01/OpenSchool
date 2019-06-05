using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;
using OpenSchool.Models.Modelo_Proyecto;

namespace OpenSchool.Controllers
{
    public class ProfesorController : Controller
    {
	    private void Tipo()
	    {
		    ViewBag.cargo = "Prof";

	    }
        // GET: Profesor
        public ActionResult Index()
        {
			Tipo();
            return View();
        }

	    public ActionResult CrearContenido()
	    {
		    Tipo();
		    var curso = new List<Cursos>();
		    //Consulta LINQ XD
			curso.Add(new Cursos()
			{
				IdCurso = 1,
				Nombre = "Matematica para Ingeneria"
			});
		    ViewBag.curso = curso;

			Contenido modelContenido = new Contenido();
		    return View(modelContenido);
	    }
	}
}