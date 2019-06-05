using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class UsuariosRoles
    {
        [Key]
        [Required]
        [Display(Name ="Código del Usuario Rol")]
        public int IdUsersRol { get; set; }
        [Required]
        [ForeignKey("Roles")]
        [Display(Name ="Código del Rol")]
        public int IdRol { get; set; }
        [Required]
        [Display(Name ="Código del Usuario")]
        [ForeignKey("Usuarios")]
        public int IdUsuario { get; set; }

        public virtual Roles Roles { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}