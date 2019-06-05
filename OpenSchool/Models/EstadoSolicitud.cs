using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class EstadoSolicitud
    {
        [Key]
        [Display(Name = "Código del estado de la solicitud")]
        public int IdEstadoSol { get; set; }

        [Display(Name = "Detalle")]
        [Required(ErrorMessage = "El nombre del estado solicitud es requerido")]
        public string NEstadoSolicitud { get; set; }
        public virtual ICollection<SolicitudCurso> SolicitudCurso { get; set; }

    }
}