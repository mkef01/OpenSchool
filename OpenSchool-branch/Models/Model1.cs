using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class Model1 : DbContext
    {
        public Model1()

           : base("DefaultConnection")
        {
        }
      
        public virtual DbSet<CategoriasCursos> CategoriasCursos { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<ContenidoCurso> ContenidoCurso { get; set; }
        public virtual DbSet<CoordinadorCategorias> CoordinadorCategorias { get; set; }
        public virtual DbSet<Cursos> Cursos { get; set; }
        public virtual DbSet<CursosCategorias> CursosCategorias { get; set; }
        public virtual DbSet<EstadoCurso> EstadoCurso { get; set; }
        public virtual DbSet<EstadoRegistro> EstadoRegistro { get; set; }
        public virtual DbSet<EstadoSolicitud> EstadoSolicitud { get; set; }
        public virtual DbSet<EvaluacionesCursos> EvaluacionesCursos { get; set; }
        public virtual DbSet<IntentosEvaluacion> IntentosEvaluacion { get; set; }
        public virtual DbSet<MensajeChat> MensajeChat { get; set; }
        public virtual DbSet<NivelCursos> NivelCursos { get; set; }
        public virtual DbSet<PreguntasEvaluacion> PreguntasEvaluacion { get; set; }
        public virtual DbSet<ProfesoresCursos> ProfesoresCursos { get; set; }
        public virtual DbSet<RegistroCurso> RegistroCurso { get; set; }
        public virtual DbSet<RespuestasEvaluacion> RespuestasEvaluacion { get; set; }
        public virtual DbSet<RespuestasIntento> RespuestasIntento { get; set; }
        public virtual DbSet<RevisionContenido> RevisionContenido { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SeccionesCursos> SeccionesCursos { get; set; }
        public virtual DbSet<SolicitudCurso> SolicitudCurso { get; set; }
        public virtual DbSet<TipoContenido> TipoContenido { get; set; }
        public virtual DbSet<TipoPregunta> TipoPregunta { get; set; }
        public virtual DbSet<TiposEvaluaciones> TiposEvaluaciones { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<UsuariosRoles> UsuariosRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}