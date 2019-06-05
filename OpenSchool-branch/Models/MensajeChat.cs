using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class MensajeChat
    {
        [Key]
        public int IdMensaje { get; set; }
        [ForeignKey("Chat")]
        public int? IdChat { get; set; }
        [ForeignKey("Usuarios")]
        public int? IdUsuario { get; set; }

        [Required]
        [StringLength(200)]
        public string Mensaje { get; set; }

        public DateTime FechaEnvio { get; set; }

        public virtual Chat Chat { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual Usuarios Usuarios1 { get; set; }
    }
}