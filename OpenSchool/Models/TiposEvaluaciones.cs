using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class TiposEvaluaciones
    {
        [Key]
        [Required]
        [Display(Name ="Código del Tipo")]
        public int IdTipo { get; set; }

        [Required]
        [Display(Name ="Nombre del Tipo Evaluación")]
        [StringLength(100)]
        public string TipoEvaluacion { get; set; }

        public virtual ICollection<EvaluacionesCursos> EvaluacionesCursos { get; set; }
    }
}