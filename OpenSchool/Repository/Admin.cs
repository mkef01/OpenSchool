using Dapper;
using OpenSchool.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace OpenSchool.Repository
{
    public class Admin: ChartsAdmin
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void CursoMen(out List<DataDash> CursosMensual, out double anual)
        {
            var s = db.CursosCategorias.Include(a => a.CategoriasCursos).Include(a => a.Cursos);
            var b = (from a in s group a.IdCurso by a.CategoriasCursos into c
                    select  new  DataDash { label = c.Key.DetalleCategoria, y = c.Count()}).ToList();
            CursosMensual = b;
            anual = 0;
            foreach(DataDash element in b)
            {
                anual += element.y;
            }
            
        }

        public void Alumn(out List<DataDash> Alumnos, out double ord) {
            
            var s = db.RegistroCurso.Include(a => a.Cursos).Where(a => a.EstadoRegistro.Estado != "Pendiente" && a.EstadoRegistro.Estado != "Rechazado"); ;
            var b = (from a in s
                     group a.IdRegistroCurso by a.Cursos into c
                     select new DataDash { label = c.Key.Nombre, y = c.Count() }).ToList();
            Alumnos = b;
            ord = 0;
            foreach (DataDash element in b)
            {
                ord += element.y;
            }
        }

        public void TipUsuario(out List<DataDash> TiposUsuarios, out double visitantes, out double profesores) {
            string query = "select a.Name as 'label' , Count(b.UserId) as 'y' from AspNetRoles as a";
            query += " inner join AspNetUserRoles as b on a.Id = b.RoleId group by a.Name";
            double contador = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                
                var data = con.Query<DataDash>(query).ToList();

                foreach (var item in data)
                {
                    if(item.label == "Profesor")
                    {
                        contador += item.y;
                    }
                }
                TiposUsuarios = data;
            }

            var b = db.Visitante.ToList();
            visitantes = b.Count();
            profesores = contador;
            
        }
    }
}