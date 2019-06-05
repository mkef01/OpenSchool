using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OpenSchool.Models
{
    public class Usuarios
    {
        [Key]
        [Display(Name ="Codigo Usuario")]
        public int IdUsuario { get; set; }

        [StringLength(40)]
        [Display(Name = "Nombre del usuario")]
        [MinLength(3, ErrorMessage = "La longitud minima es de 3 caracteres")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido del usuario")]
        [MinLength(3, ErrorMessage = "La longitud minima es de 3 caracteres")]
        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido { get; set; }


        [Display(Name = "Email del usuario")]
        [Required(ErrorMessage = "El correo es requerido")]
        public string Email { get; set; }


        [Display(Name = "Contraseña del usuario")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Contrasenha { get; set; }


        [Display(Name = "Salt del usuario")]
        [Required(ErrorMessage = "El salt es requerido")]
        public string Salt { get; set; }


        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es requerido")]
        public DateTime FechaNacimiento { get; set; }


        [Display(Name = "Fecha de registro")]
        [Required(ErrorMessage = "La fecha de registro es requerido")]
        public DateTime FechaRegistro { get; set; }

        //public virtual Roles Roles { get; set; }

        public virtual ICollection<Chat> Chat { get; set; }

        public virtual ICollection<ContenidoCurso> ContenidoCurso { get; set; }

        public virtual ICollection<CoordinadorCategorias> CoordinadorCategorias { get; set; }

        public virtual ICollection<EvaluacionesCursos> EvaluacionesCursos { get; set; }

        public virtual ICollection<IntentosEvaluacion> IntentosEvaluacion { get; set; }

        public virtual ICollection<MensajeChat> MensajeChat { get; set; }

        public virtual ICollection<MensajeChat> MensajeChat1 { get; set; }

        public virtual ICollection<ProfesoresCursos> ProfesoresCursos { get; set; }

        public virtual ICollection<RegistroCurso> RegistroCurso { get; set; }

        public virtual ICollection<RevisionContenido> RevisionContenido { get; set; }

        public virtual ICollection<SolicitudCurso> SolicitudCurso { get; set; }

        public virtual ICollection<UsuariosRoles> UsuariosRoles { get; set; }
        
    }
}