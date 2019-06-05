using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class SolicitudesInscripcion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Seccion { get; set; }
        public string EstCurs { get; set; }
        public string EstadoSoli { get; set; }

    }
}