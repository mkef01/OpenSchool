using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class EstadoRegistro
    {
        [Key]
        [Display(Name = "Código de estado registro")]
        public int IdEstado { get; set; }

        [Display(Name = "Estado de registro")]
        [Required(ErrorMessage = "El estado del registro  es requerido")]
        public string Estado { get; set; }
        public virtual ICollection<RegistroCurso> RegistroCurso { get; set; }

    }
}