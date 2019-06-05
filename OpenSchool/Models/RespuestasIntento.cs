using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class RespuestasIntento
    {
        [Key]
        public int IdRespInt { get; set; }
        [ForeignKey("IntentosEvaluacion")]
        public int IdIntento { get; set; }

        [Required]
        [StringLength(255)]
        public string Respuesta { get; set; }

        [Required]
        [StringLength(255)]
        public string Observacion { get; set; }

        public decimal NotaAsignada { get; set; }

        public virtual IntentosEvaluacion IntentosEvaluacion { get; set; }
    }
}