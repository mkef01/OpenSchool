using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenSchool.Models;

namespace OpenSchool.Repository
{
    interface CharProf
    {
        void VisitaCurso(out List<DataDash> CursoHoras,  string id);
        void AlumnAct(out List<DataDash> Alumnos, string id);
        void ActImport(out List<DataDash> ActImport, string id);
    }
}