﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class SolicitudCurso
    {
        [Key]
        public int IdSoliCurso { get; set; }
        [ForeignKey("Cursos")]
        public int IdCurso { get; set; }
        [ForeignKey("Usuarios")]
        public int IdUsuario { get; set; }
        [ForeignKey("EstadoSolicitud")]
        public int IdEstadoSol { get; set; }

        public virtual Cursos Cursos { get; set; }

        public virtual EstadoSolicitud EstadoSolicitud { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}