using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
	public class Login
	{
		public string Usuario { get; set; }
		public string Contraseña { get; set; }
		public string NombreVisitante { get; set; }
		public string CorreoVisitante { get; set; }
	}
}