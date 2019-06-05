using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class RespuestasEvaluacion
    {
        [Key]
        public int IdRespuesta { get; set; }
        [ForeignKey("PreguntasEvaluacion")]
        public int IdPregunta { get; set; }

        [Required]
        [StringLength(200)]
        public string Respuesta { get; set; }

        public decimal Ponderacion { get; set; }

        public bool Correcta { get; set; }

        public virtual PreguntasEvaluacion PreguntasEvaluacion { get; set; }
    }
}