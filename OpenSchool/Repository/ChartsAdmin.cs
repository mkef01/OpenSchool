using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenSchool.Models;

namespace OpenSchool.Repository
{
    interface ChartsAdmin
    {
            void CursoMen(out List<DataDash> CursosMensual, out double anual);
            void Alumn(out List<DataDash> Alumnos, out double ord);
            void TipUsuario(out List<DataDash> TiposUsuarios, out double visitantes, out double profesores);
        
    }
}