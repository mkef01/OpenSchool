using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class ContenidoCurso
    {
        [Key]
        public int IdContenido { get; set; }
        [ForeignKey("Cursos")]
        public int IdCurso { get; set; }
        [ForeignKey("Usuarios")]
        public string Id { get; set; }
        [ForeignKey("TipoContenido")]
        public int IdTipoCont { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(200)]
        public string Contenido { get; set; }

        [Display(Name = "Archivo")]
        [StringLength(255)]
        public string Archivo { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public virtual Cursos Cursos { get; set; }

        public virtual TipoContenido TipoContenido { get; set; }

        public virtual UsuariosAsp Usuarios { get; set; }

        public virtual ICollection<RevisionContenido> RevisionContenido { get; set; }
    }
}