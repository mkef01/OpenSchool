using OpenSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSchool.Repository
{
    public class Informe: CharInform
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void CursoTerm(out List<DataDash> Curso, string id,int IdCurso)
        {
            var cursos = db.RegistroCurso.Where(a => a.Id == id && a.IdEstado == 2).Select(a => a.IdCurso).ToList();
            List<DataDash> temp = new List<DataDash>();
            
                var contenido = db.ContenidoCurso.Where(a => a.IdCurso == IdCurso).Select(a => a.IdContenido).ToList();
                int visto = 0;
                foreach (int idCont in contenido)
                {
                    var visitado = db.RevisionContenido.Where(a => a.IdContenido == idCont && a.IdUsuario == id);
                    if (visitado.Count() > 0)
                    {
                        visto += 1;
                    }
                }
                var cursoN = db.Cursos.FirstOrDefault(a => a.IdCurso == IdCurso);
                double totFinal = (double.Parse(visto.ToString()) / double.Parse(contenido.Count().ToString())) * 100.00;
                temp.Add(new DataDash
                {
                    label = cursoN.Nombre,
                    y = totFinal
                });
            
            Curso = temp;
            if (Curso.Count() == 0)
            {
                Curso.Add(new DataDash
                {
                    label = " ",
                    y = 0.0
                });
            }
        }

        public void ActImport(out List<DataDash> ActImport, string id, int IdCurso)
        {
            var cursos = db.RegistroCurso.Where(a => a.Id == id && a.IdEstado == 2).Select(a => a.IdCurso).ToList();
            List<DataDash> temp = new List<DataDash>();
            
                var evaluacionesInfo = db.EvaluacionesCursos.Where(a => a.IdCurso == IdCurso).Select(a => new { Id = a.IdEvaluacion, Intentos = a.Intentos, Ponderacion = a.Ponderacion });
                var evaluaciones = evaluacionesInfo.Select(a => a.Id).ToList();
                double totPorcent = decimal.ToDouble(evaluacionesInfo.Select(a => a.Ponderacion).Sum());
                double visto = 0.0;
                foreach (int idEv in evaluaciones)
                {
                    double nota = 0.0;
                    var intentos = db.IntentosEvaluacion.Where(a => a.IdEvaluacion == idEv && a.Id == id).Select(a => a.IdIntento).ToList();
                    foreach (int intent in intentos)
                    {
                        var intentoFinal = db.RespuestasIntento.Where(a => a.IdIntento == intent).Select(a => a.NotaAsignada).Sum();
                        var preguntas = db.PreguntasEvaluacion.Where(a => a.IdEvaluacion == idEv).Select(a => a.Ponderacion).Sum();
                        nota = decimal.ToDouble(intentoFinal) / decimal.ToDouble(preguntas);
                    }
                    double porcentaje = decimal.ToDouble(evaluacionesInfo.FirstOrDefault(a => a.Id == idEv).Ponderacion);
                    if (intentos.Count() != 0)
                    {
                        nota = nota / intentos.Count() * porcentaje;
                    }
                    visto += nota;
                }
                var cursoN = db.Cursos.FirstOrDefault(a => a.IdCurso == IdCurso);
                visto = visto / totPorcent * 100.00;
                temp.Add(new DataDash
                {
                    label = cursoN.Nombre,
                    y = visto
                });
            
            ActImport = temp;
            if (ActImport.Count() == 0)
            {
                ActImport.Add(new DataDash
                {
                    label = " ",
                    y = 0.0
                });
            }
        }

    }
}