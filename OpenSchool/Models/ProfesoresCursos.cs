using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class ProfesoresCursos
    {
        [Key]
        [Required]
        [Display(Name ="Código del Profesor")]
        public int IdProfCur { get; set; }
        [Required]
        [Display(Name ="Código Curso")]
        [ForeignKey("Cursos")]
        public int IdCursos { get; set; }
        [Required]
        [Display(Name ="Código Usuario")]
        [ForeignKey("Usuarios")]
        public string Id { get; set; }

        public virtual Cursos Cursos { get; set; }

        public virtual UsuariosAsp Usuarios { get; set; }
    }
}