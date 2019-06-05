using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class EvaluacionesCursos
    {
        [Key]
        public int IdEvaluacion { get; set; }

        [ForeignKey("Cursos")]
        public int IdCurso { get; set; }
        [ForeignKey("TiposEvaluaciones")]
        public int IdTipo { get; set; }
        [ForeignKey("Usuarios")]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Detalle { get; set; }

        public int Intentos { get; set; }

        public decimal Ponderacion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public virtual Cursos Cursos { get; set; }

        public virtual TiposEvaluaciones TiposEvaluaciones { get; set; }

        public virtual UsuariosAsp Usuarios { get; set; }

        public virtual ICollection<IntentosEvaluacion> IntentosEvaluacion { get; set; }

        public virtual ICollection<PreguntasEvaluacion> PreguntasEvaluacion { get; set; }
    }
}