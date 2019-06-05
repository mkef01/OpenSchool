using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class Chat
    {
        [Key]
        public int IdChat { get; set; }

        public int IdUsuario { get; set; }

        public int IdUsuario2 { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual ICollection<MensajeChat> MensajeChat { get; set; }
    }
}