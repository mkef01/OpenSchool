using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class CoordinadorCategorias
    {
        [Key]
        [Required]
        public int IdCoordCate { get; set; }
        [Required]
        [ForeignKey("CategoriasCursos")]
        public int IdCategoria { get; set; }
        [Required]
        [ForeignKey("Usuarios")]
        public string Id { get; set; }

        public virtual CategoriasCursos CategoriasCursos { get; set; }

        public virtual UsuariosAsp Usuarios { get; set; }
    }
}