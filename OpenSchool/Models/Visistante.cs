using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class Visistante
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre del usuario")]
        [MinLength(3, ErrorMessage = "La longitud minima es de 3 caracteres")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Email del usuario")]
        [Required(ErrorMessage = "El correo es requerido")]
        public string Email { get; set; }
    }
}