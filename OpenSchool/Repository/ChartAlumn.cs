using OpenSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSchool.Repository
{
    interface ChartAlumn
    {
        void CursoTerm(out List<DataDash> Curso, string id);
        void AlumnAct(out List<DataDash> Alumnos, string id);
        void ActImport(out List<DataDash> ActImport, string id);
    }
}