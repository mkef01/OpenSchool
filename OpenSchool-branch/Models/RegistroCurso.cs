using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class RegistroCurso
    {
        [Key]
        public int IdRegistroCurso { get; set; }
        [ForeignKey("Cursos")]
        public int IdCurso { get; set; }
        [ForeignKey("Usuarios")]
        public int IdUsuario { get; set; }
        [ForeignKey("EstadoRegistro")]
        public int IdEstado { get; set; }

        public virtual Cursos Cursos { get; set; }

        public virtual EstadoRegistro EstadoRegistro { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}