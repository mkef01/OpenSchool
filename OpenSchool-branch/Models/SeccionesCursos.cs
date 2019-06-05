using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class SeccionesCursos
    {
        [Key]
        [Required]
        [Display(Name = "Código de sección")]
        public int IdSeccion { get; set; }

        [Display(Name = "Sección")]
        [Required(ErrorMessage = "La sección es requerida")]
        public string DetalleSeccion { get; set; }
        public virtual ICollection<Cursos> Cursos { get; set; }

    }
}