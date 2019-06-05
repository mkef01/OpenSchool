using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class EstadoCurso
    {
        [Key]
        [Display(Name = "Código del estado del curso")]
        public int IdEstCurs { get; set; }

        [Display(Name = "Estado del curso")]
        [Required(ErrorMessage = "El estado del curso es requerido")]
        [MinLength(3, ErrorMessage = "La longitud minima es de 3 caracteres")]
        public string Estado { get; set; }
        public virtual ICollection<Cursos> Cursos { get; set; }

    }
}