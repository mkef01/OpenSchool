using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class Roles
    {
        [Key]
        [Display(Name = "Código de rol")]
        public int IdRol { get; set; }

        [Display(Name = "Nombre del rol")]
        [Required(ErrorMessage = "El nombre del rol es requerido")]
        public string NombreRol { get; set; }

        //public virtual Usuarios Usuarios { get; set; }

        public virtual ICollection<UsuariosRoles> UsuariosRoles { get; set; }
    }
}