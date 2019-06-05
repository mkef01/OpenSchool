using OpenSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSchool.Repository
{
    interface CharInform
    {
        void CursoTerm(out List<DataDash> Curso, string id, int IdCurso);
        void ActImport(out List<DataDash> ActImport, string id, int IdCurso);
    }
}