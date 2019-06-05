using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class Chat
    {
        [Key]
        public int IdChat { get; set; }

        [ForeignKey("Usuarios")]
        public string Id { get; set; }

        [ForeignKey("Usuarios")]
        public string IdUsuario { get; set; }

        

        public virtual ICollection<MensajeChat> MensajeChat { get; set; }
    }
}