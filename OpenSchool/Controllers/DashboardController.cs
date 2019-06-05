using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OpenSchool.Models;
using OpenSchool.Models.Modelo_Proyecto;
using OpenSchool.Repository;

namespace OpenSchool.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        CharProf CharProf;
        ChartAlumn ChartAlumn;
        ChartsAdmin ChartsAdmin;
        CharInform charInform;
        private ApplicationDbContext bd = new ApplicationDbContext();

        public DashboardController()
        {
            CharProf = new  Prof();
            ChartAlumn = new Alumn();
            ChartsAdmin = new Admin();
            charInform = new Informe();
        }
        // GET: Dashboard
        [Authorize(Roles ="Administrador, Coordinador")]
        public ActionResult IndexAdmin()
        {
            var currentUser = bd.UsuariosAsps.FirstOrDefault(u => u.UserName == User.Identity.Name);
            try
            {
                List<DataDash> temp1 = new List<DataDash>();
                List<DataDash> temp2 = new List<DataDash>();
                List<DataDash> temp3 = new List<DataDash>();
                double a = 0.0;
                double b = 0.0;
                double c = 0.0;
                double d = 0.0;
                ChartsAdmin.Alumn(out temp1, out a);
                ChartsAdmin.CursoMen(out temp2, out b);
                ChartsAdmin.TipUsuario(out temp3, out c, out d);
                ViewBag.AlumnCurso = JsonConvert.SerializeObject(temp1);
                ViewBag.TipoUsuario = JsonConvert.SerializeObject(temp3);
                ViewBag.CatCurso = JsonConvert.SerializeObject(temp2);
                ViewBag.TotAlumn = a;
            }
            catch (Exception e)
            {
                throw;
            }
            return View();
        }

        // GET: Dashboard
        [Authorize(Roles ="Profesor")]
        public ActionResult IndexProf()
        {
            var currentUser = bd.UsuariosAsps.FirstOrDefault(u => u.UserName == User.Identity.Name);
            try
            {
                List<DataDash> temp1 = new List<DataDash>();
                List<DataDash> temp2 = new List<DataDash>();
                List<DataDash> temp3 = new List<DataDash>();
                CharProf.ActImport(out temp1, currentUser.Id);
                CharProf.AlumnAct(out temp2, currentUser.Id);
                CharProf.VisitaCurso(out temp3, currentUser.Id);
                ViewBag.AlumnCurso = JsonConvert.SerializeObject(temp1);
                ViewBag.TipoUsuario = JsonConvert.SerializeObject(temp3);
                ViewBag.CatCurso = JsonConvert.SerializeObject(temp2);
                
            }
            catch (Exception e)
            {
                throw;
            }
            return View();
        }

        [Authorize(Roles ="Alumno")]
        public ActionResult IndexAlumn()
        {
            var currentUser = bd.UsuariosAsps.FirstOrDefault(u => u.UserName == User.Identity.Name);
            try
            {
                List<DataDash> temp1 = new List<DataDash>();
                List<DataDash> temp2 = new List<DataDash>();
                List<DataDash> temp3 = new List<DataDash>();
                ChartAlumn.ActImport(out temp1, currentUser.Id);
                ChartAlumn.AlumnAct(out temp2, currentUser.Id);
                ChartAlumn.CursoTerm(out temp3, currentUser.Id);
                ViewBag.AlumnCurso = JsonConvert.SerializeObject(temp3);
                ViewBag.AlumnNot = JsonConvert.SerializeObject(temp1);
                ViewBag.CatCurso = JsonConvert.SerializeObject(temp2);

            }
            catch (Exception e)
            {
                throw;
            }
            return View();
        }

        public ActionResult Informe()
        {
            try
            {
	            Reporte objReporte = Session["curso"] as Reporte;
	            ViewBag.alum = objReporte.IdAlumno;
	            ViewBag.cur = objReporte.IdCurso;
				List<DataDash> temp1 = new List<DataDash>();
                List<DataDash> temp2 = new List<DataDash>();
                List<DataDash> temp3 = new List<DataDash>();
                charInform.CursoTerm(out temp1, objReporte.IdAlumno, objReporte.IdCurso);
                charInform.ActImport(out temp2, objReporte.IdAlumno, objReporte.IdCurso);
                ViewBag.AlumnCurso = JsonConvert.SerializeObject(temp1);
                ViewBag.AlumnNot = JsonConvert.SerializeObject(temp2);

            }
            catch (Exception e)
            {
                throw;
            }
            return View();
        }

	    public ActionResult Aplicar(int id,string alum)
	    {
			RegistroCurso objRegistroCurso = new RegistroCurso();
		    var query = from registro in bd.RegistroCurso
			    where registro.IdCurso == id && registro.Id.Equals(alum)
			    select new
			    {
					idreg = registro.IdRegistroCurso,
					idcur = registro.IdCurso,
					idalum = registro.Id,
					idest = registro.IdEstado
			    };
		    var lista = query.ToList();
		    foreach (var item in lista)
		    {
			    objRegistroCurso.IdRegistroCurso = item.idreg;
			    objRegistroCurso.IdCurso = item.idcur;
			    objRegistroCurso.Id = item.idalum;
			    objRegistroCurso.IdEstado = 5;
		    }
		    bd.Entry(objRegistroCurso).State = EntityState.Modified;
		    bd.SaveChanges();
			//***************************************************************//
			SolicitudCurso objSolicitudCurso = new SolicitudCurso();
		    var query2 = from solicitud in bd.SolicitudCurso
			    where solicitud.IdCurso == id && solicitud.Id.Equals(alum)
			    select new
			    {
					idsoli = solicitud.IdSoliCurso, idcurso = solicitud.IdCurso,
					idestu = solicitud.Id, idestado = solicitud.IdEstadoSol
			    };
		    var lista2 = query2.ToList();
		    foreach (var item in lista2)
		    {
			    objSolicitudCurso.IdSoliCurso = item.idsoli;
			    objSolicitudCurso.IdCurso = item.idcurso;
			    objSolicitudCurso.Id = item.idestu;
			    objSolicitudCurso.IdEstadoSol = 3;
		    }
		    bd.Entry(objSolicitudCurso).State = EntityState.Modified;
		    bd.SaveChanges();
		    ViewBag.correcto = "SI";
			return View("~/Views/Aprobacion/Index.cshtml");
	    }
    }
}