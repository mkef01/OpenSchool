using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class IntentosEvaluacion
    {
        [Key]
        public int IdIntento { get; set; }
        [ForeignKey("Usuarios")]
        public int? IdUsuario { get; set; }
        [ForeignKey("EvaluacionesCursos")]
        public int? IdEvaluacion { get; set; }

        public DateTime FechaIntento { get; set; }

        public virtual EvaluacionesCursos EvaluacionesCursos { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual ICollection<RespuestasIntento> RespuestasIntento { get; set; }
    }
}