using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class TipoPregunta
    {
        [Key]
        public int IdTipoPreg { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo_Pregunta { get; set; }

        public virtual ICollection<PreguntasEvaluacion> PreguntasEvaluacion { get; set; }
    }
}