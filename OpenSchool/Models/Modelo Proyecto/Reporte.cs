using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSchool.Models.Modelo_Proyecto
{
	public class Reporte
	{
		public string IdAlumno { get; set; }
		public string Email { get; set; }
		public int IdCurso { get; set; }
		public bool Ingresado { get; set; }

		public Reporte()
		{
			Ingresado = false;
		}
	}
}