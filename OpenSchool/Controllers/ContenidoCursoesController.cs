using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;
using System.IO;
using System.Web.WebPages;
using OpenSchool.Models.Modelo_Proyecto;
using System.Threading.Tasks;

namespace OpenSchool.Controllers
{
    [Authorize(Roles = "Profesor, Alumno")]
    public class ContenidoCursoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContenidoCursoes
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Cursos");
            }
            else
            {
                var contenidoCurso = db.ContenidoCurso.Include(c => c.Cursos).Include(c => c.TipoContenido).Include(c => c.Usuarios).Where(c => c.IdCurso == id);
                return View(contenidoCurso.ToList());
            }
        }

        // GET: ContenidoCursoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContenidoCurso contenidoCurso = db.ContenidoCurso.Find(id);
            if (User.IsInRole("Alumno"))
            {
                var user = db.UsuariosAsps.FirstOrDefault(a=> a.UserName==User.Identity.Name);
                RevisionContenido revisionContenido = new RevisionContenido
                {
                    FechaRevision = DateTime.Now,
                    IdContenido = id,
                    IdUsuario = user.Id
                };
                db.RevisionContenido.Add(revisionContenido);
                db.SaveChanges();

            }
            if (contenidoCurso == null)
            {
                return HttpNotFound();
            }
            if(contenidoCurso.TipoContenido.Tipo == "Documento")
            {
                var fileContents = Server.MapPath("~"+ contenidoCurso.Archivo);
                string[] values = contenidoCurso.Archivo.Split('/');
                var filename = values[values.Count()-1];
                byte[] fileBytes = System.IO.File.ReadAllBytes(fileContents);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet,filename);
            }
            return View(contenidoCurso);
        }

	    [Authorize(Roles = "Profesor")]
		// GET: ContenidoCursoes/Create
		public ActionResult Create()
        {
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre");
            ViewBag.IdTipoCont = new SelectList(db.TipoContenido, "IdTipoCont", "Tipo");
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email");
	        ViewBag.min = System.DateTime.Now.ToString("yyyy-MM-dd");
	        ViewBag.max = System.DateTime.Today.AddMonths(5).ToString("yyyy-MM-dd");
            return View();
        }

		// POST: ContenidoCursoes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Profesor")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AgregarContenido contenidoCurso, HttpPostedFileBase file)
        {
	        if (contenidoCurso.IdCurso > 0 && contenidoCurso.FechaPublicacion != null &&
	            contenidoCurso.Contenido != null && contenidoCurso.Descripcion != null)
	        {
		        var usuariosAsp = db.UsuariosAsps.Single(x => x.Email == User.Identity.Name);
		        ContenidoCurso contenido = new ContenidoCurso();
		        contenido.IdCurso = contenidoCurso.IdCurso;
		        contenido.Id = usuariosAsp.Id;
		        contenido.Descripcion = contenidoCurso.Descripcion;
		        contenido.FechaPublicacion = contenidoCurso.FechaPublicacion.AsDateTime();
		        contenido.Contenido = contenidoCurso.Contenido;
		        if (contenidoCurso.Hipervinculo != null)
		        {
			        if (contenidoCurso.video)
			        {
				        contenido.IdTipoCont = 3;
				        contenido.Archivo = contenidoCurso.Hipervinculo;
				        db.ContenidoCurso.Add(contenido);
				        db.SaveChanges();
			        }
			        else
			        {
				        contenido.IdTipoCont = 4;
				        contenido.Archivo = contenidoCurso.Hipervinculo;
				        db.ContenidoCurso.Add(contenido);
				        db.SaveChanges();
			        }
		        }
		        if (contenidoCurso.Texto != null)
		        {
			        contenido.IdTipoCont = 2;
			        contenido.Archivo = contenidoCurso.Texto;
			        db.ContenidoCurso.Add(contenido);
			        db.SaveChanges();
		        }
		        if (file != null)
		        {
			        contenido.IdTipoCont = 1;
					string archivo = (DateTime.Now.ToString("yyyy-MM-dd")+"-"+file.FileName).ToLower().Trim();
			        file.SaveAs(Server.MapPath("/Archivos/"+archivo));
			        string path = "/Archivos/" + archivo;
			        contenido.Archivo = path.Trim();
					db.ContenidoCurso.Add(contenido);
			        db.SaveChanges();
		        }
				return RedirectToAction("Index", new { id= contenido.IdCurso});
			}
			ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre");
	        ViewBag.IdTipoCont = new SelectList(db.TipoContenido, "IdTipoCont", "Tipo");
	        ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email");
	        return View();
        }

        // GET: ContenidoCursoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContenidoCurso contenidoCurso = db.ContenidoCurso.Find(id);
            if (contenidoCurso == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre", contenidoCurso.IdCurso);
            ViewBag.IdTipoCont = new SelectList(db.TipoContenido, "IdTipoCont", "Tipo", contenidoCurso.IdTipoCont);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", contenidoCurso.Id);
            return View(contenidoCurso);
        }

        // POST: ContenidoCursoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdContenido,IdCurso,Id,IdTipoCont,Descripcion,Contenido,Archivo,FechaPublicacion")] ContenidoCurso contenidoCurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contenidoCurso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = contenidoCurso.IdCurso });
            }
            ViewBag.IdCurso = new SelectList(db.Cursos, "IdCurso", "Nombre", contenidoCurso.IdCurso);
            ViewBag.IdTipoCont = new SelectList(db.TipoContenido, "IdTipoCont", "Tipo", contenidoCurso.IdTipoCont);
            ViewBag.Id = new SelectList(db.UsuariosAsps, "Id", "Email", contenidoCurso.Id);
            return View(contenidoCurso);
        }

        // GET: ContenidoCursoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContenidoCurso contenidoCurso = db.ContenidoCurso.Find(id);
            if (contenidoCurso == null)
            {
                return HttpNotFound();
            }
            return View(contenidoCurso);
        }

        // POST: ContenidoCursoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContenidoCurso contenidoCurso = db.ContenidoCurso.Find(id);
	        if (System.IO.File.Exists(contenidoCurso.Archivo))
	        {
		        System.IO.File.Delete(contenidoCurso.Archivo);
	        }
	        db.ContenidoCurso.Remove(contenidoCurso);
			db.SaveChanges();
            return RedirectToAction("Index", new { id = contenidoCurso.IdCurso });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
