using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
    public class UsuariosRolesController : Controller
    {
        private Model1 db = new Model1();

        // GET: UsuariosRoles
        public ActionResult Index()
        {
            var usuariosRoles = db.UsuariosRoles.Include(u => u.Roles).Include(u => u.Usuarios);
            return View(usuariosRoles.ToList());
        }

        // GET: UsuariosRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuariosRoles usuariosRoles = db.UsuariosRoles.Find(id);
            if (usuariosRoles == null)
            {
                return HttpNotFound();
            }
            return View(usuariosRoles);
        }

        // GET: UsuariosRoles/Create
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "NombreRol");
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre");
            return View();
        }

        // POST: UsuariosRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsersRol,IdRol,IdUsuario")] UsuariosRoles usuariosRoles)
        {
            if (ModelState.IsValid)
            {
                db.UsuariosRoles.Add(usuariosRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "NombreRol", usuariosRoles.IdRol);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", usuariosRoles.IdUsuario);
            return View(usuariosRoles);
        }

        // GET: Usuarios/Create
        public ActionResult CrearAdministrador()
        {
            var user = (from a in db.Roles
                        where a.NombreRol == "Coordinador" || a.NombreRol == "Profesores"
                        select a);
            ViewBag.Roles = new MultiSelectList(user, "IdRol", "NombreRol");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearAdministrador([Bind(Include = "IdUsuario,Nombre,Apellido,Email,Contrasenha,Salt,FechaNacimiento,FechaRegistro")] Usuarios usuarios, int[] IdRol)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (IdRol != null)
            {
                foreach (int elemnto in IdRol)
                {
                    UsuariosRoles nuevoR = new UsuariosRoles();
                    nuevoR.IdRol = elemnto;
                    var detalle = usuarios.IdUsuario;
                    nuevoR.IdUsuario = int.Parse(detalle.ToString());
                    db.UsuariosRoles.Add(nuevoR);
                    db.SaveChanges();
                };
            }
            return View(usuarios);
        }

        // GET: UsuariosRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuariosRoles usuariosRoles = db.UsuariosRoles.Find(id);
            if (usuariosRoles == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "NombreRol", usuariosRoles.IdRol);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", usuariosRoles.IdUsuario);
            return View(usuariosRoles);
        }

        // POST: UsuariosRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsersRol,IdRol,IdUsuario")] UsuariosRoles usuariosRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuariosRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "NombreRol", usuariosRoles.IdRol);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", usuariosRoles.IdUsuario);
            return View(usuariosRoles);
        }

        // GET: UsuariosRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuariosRoles usuariosRoles = db.UsuariosRoles.Find(id);
            if (usuariosRoles == null)
            {
                return HttpNotFound();
            }
            return View(usuariosRoles);
        }

        // POST: UsuariosRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuariosRoles usuariosRoles = db.UsuariosRoles.Find(id);
            db.UsuariosRoles.Remove(usuariosRoles);
            db.SaveChanges();
            return RedirectToAction("Index");
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
