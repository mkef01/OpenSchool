using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class PreguntasEvaluacion
    {
        [Key]
        public int IdPregunta { get; set; }
        [ForeignKey("EvaluacionesCursos")]
        public int IdEvaluacion { get; set; }
        [ForeignKey("TipoPregunta")]
        public int IdTipoPreg { get; set; }

        [Required]
        [StringLength(100)]
        public string Pregunta { get; set; }

        public decimal Ponderacion { get; set; }

        public virtual EvaluacionesCursos EvaluacionesCursos { get; set; }

        public virtual TipoPregunta TipoPregunta { get; set; }

        public virtual ICollection<RespuestasEvaluacion> RespuestasEvaluacion { get; set; }
    }
}