using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSchool.Models.Modelo_Proyecto
{
	public class AgregarContenido
	{
		[Required]
		public int IdCurso { get; set; }
		[Required]
		public string Descripcion { get; set; }
		[Required]
		public string Contenido { get; set; }
		[Required]
		public string FechaPublicacion { get; set; }
		public string Texto { get; set; }
		public string Hipervinculo { get; set; }
		public bool video { get; set; }
	}
}