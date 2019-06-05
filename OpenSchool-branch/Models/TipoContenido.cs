using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class TipoContenido
    {
        [Key]
        public int IdTipoCont { get; set; }

        [Required]
        [StringLength(20)]
        public string Tipo { get; set; }
        public virtual ICollection<ContenidoCurso> ContenidoCurso { get; set; }

    }
}