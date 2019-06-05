using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class Cursos
    {
        [Key]
        public int IdCurso { get; set; }


        [Required(ErrorMessage = "Es requerido.")]
        [Display(Name = "Nombre del curso")]
        [StringLength(50)]
        [MinLength(4, ErrorMessage = "La longitud minima es de 4 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es requerido.")]
        [Display(Name = "Nivel")]
        [ForeignKey("NivelCursos")]
        public int IdNivel { get; set; }

        [Required(ErrorMessage = "Es requerido.")]
        [Display(Name = "Sección")]
        [ForeignKey("SeccionesCursos")]
        public int IdSeccion { get; set; }

        [Required(ErrorMessage = "Es requerido.")]
        [Display(Name = "Estado del Curso")]
        [ForeignKey("EstadoCurso")]
        public int IdEstCurs { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "Es requerido.")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha Fin")]
        [Required(ErrorMessage = "Es requerido.")]
        public DateTime FechaFin { get; set; }
        public virtual ICollection<ContenidoCurso> ContenidoCurso { get; set; }

        public virtual EstadoCurso EstadoCurso { get; set; }

        public virtual NivelCursos NivelCursos { get; set; }

        public virtual SeccionesCursos SeccionesCursos { get; set; }

        public virtual ICollection<CursosCategorias> CursosCategorias { get; set; }

        public virtual ICollection<EvaluacionesCursos> EvaluacionesCursos { get; set; }

        public virtual ICollection<ProfesoresCursos> ProfesoresCursos { get; set; }

        public virtual ICollection<RegistroCurso> RegistroCurso { get; set; }

        public virtual ICollection<SolicitudCurso> SolicitudCurso { get; set; }
    }
}