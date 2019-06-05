using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class NivelCursos
    {
        [Key]
        [Required]
        [Display(Name = "Código del nivel")]
        public int IdNivel { get; set; }

        [Display(Name = "Nivel")]
        [Required(ErrorMessage = "El detalle del nivel es requerido")]
        [MinLength(1, ErrorMessage = "La longitud minima es de 1 caracteres")]
        public string DetalleNivel { get; set; }
        public virtual ICollection<Cursos> Cursos { get; set; }

    }
}