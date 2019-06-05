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
    public class Prof: CharProf
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void VisitaCurso(out List<DataDash> CursoHoras, string id) {
            string query = "select a.Nombre as 'label', Count(c.IdContenido) as 'y' from Cursos";
            query += " as a inner join ContenidoCursoes as b on a.IdCurso = b.IdCurso";
            query += " inner join RevisionContenidoes as c on b.IdContenido = c.IdContenido ";
            query += "inner join ProfesoresCursos as d on a.IdCurso = d.IdCursos ";
            query += "where d.Id = '"+id+"' group by a.Nombre";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {

                var data = con.Query<DataDash>(query).ToList();

                
                CursoHoras = data;
            }
        }
        public void AlumnAct(out List<DataDash> Alumnos, string id) {
            string query = "select	a.Nombre as 'label', Count(c.Id) as 'y' from Cursos as a";
            query += " inner join RegistroCursoes as b on a.IdCurso = b.IdCurso";
            query += " inner join UsuariosAsps as c on b.Id = c.Id ";
            query += "inner join ProfesoresCursos as d on a.IdCurso = d.IdCursos ";
            query += "where d.Id = '" + id + "' group by a.Nombre";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {

                var data = con.Query<DataDash>(query).ToList();


                Alumnos= data;
            }
        }
        public void ActImport(out List<DataDash> ActImport, string id) {
            string query = "select b.Descripcion as 'label', Count(c.IdContenido) as 'y' from Cursos";
            query += " as a inner join ContenidoCursoes as b on a.IdCurso = b.IdCurso";
            query += " inner join RevisionContenidoes as c on b.IdContenido = c.IdContenido ";
            query += "inner join ProfesoresCursos as d on a.IdCurso = d.IdCursos ";
            query += "where d.Id = '"+id+"' group by b.Descripcion";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {

                var data = con.Query<DataDash>(query).ToList();


                ActImport = data;
            }
        }
    }
}