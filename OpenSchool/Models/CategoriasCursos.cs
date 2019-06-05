using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class CategoriasCursos
    {
        [Key]
        [Display(Name ="Código Categoria")]
        [Required]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5, ErrorMessage = "La longitud mínima es de 5 caracteres")]
        [Display(Name ="Nombre de Categoria")]
        public string DetalleCategoria { get; set; }
        public virtual ICollection<CoordinadorCategorias> CoordinadorCategorias { get; set; }
        public virtual ICollection<CursosCategorias> CursosCategorias { get; set; }

    }
}