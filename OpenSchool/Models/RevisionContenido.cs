using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class RevisionContenido
    {
        [Key]
        public int IdVista { get; set; }
        [ForeignKey("ContenidoCurso")]
        public int? IdContenido { get; set; }
        [ForeignKey("Usuarios")]
        public string IdUsuario { get; set; }

        public DateTime FechaRevision { get; set; }

        public virtual ContenidoCurso ContenidoCurso { get; set; }

        public virtual UsuariosAsp Usuarios { get; set; }
    }
}