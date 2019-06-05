using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class CursosCategorias
    {
        [Key]
        [Required]
        public int IdCurCate { get; set; }
        [Required]
        [ForeignKey("CategoriasCursos")]
        public int IdCategoria { get; set; }
        [Required]
        [ForeignKey("Cursos")]
        public int IdCurso { get; set; }

        public virtual CategoriasCursos CategoriasCursos { get; set; }

        public virtual Cursos Cursos { get; set; }
    }
}