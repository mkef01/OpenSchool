using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OpenSchool.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : UsuariosAsp
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class UsuariosAsp : IdentityUser
    {
        public ICollection<MensajeChat> Chat1 { get; set; }
        public ICollection<MensajeChat> Chat2 { get; set; }
        public ICollection<ContenidoCurso> ContenidoCurso { get; set;}
        public ICollection<CoordinadorCategorias> CoordinadorCategorias { get; set; }
        public ICollection<EvaluacionesCursos> EvaluacionesCursos { get; set; }
        public ICollection<IntentosEvaluacion> IntentosEvaluacion { get; set; }
        public ICollection<ProfesoresCursos> ProfesoresCursos { get; set; }
        public ICollection<RegistroCurso> RegistroCurso { get; set; }
        public ICollection<RevisionContenido> RevisionContenido { get; set; }
        public ICollection<SolicitudCurso> SolicitudCurso { get; set; }

    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<CategoriasCursos> CategoriasCursos { get; set; }
        //public virtual DbSet<Chat> Chat { get; set; }
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
        public virtual DbSet<Visistante> Visitante { get; set; }
        public virtual DbSet<SeccionesCursos> SeccionesCursos { get; set; }
        public virtual DbSet<SolicitudCurso> SolicitudCurso { get; set; }
        public virtual DbSet<TipoContenido> TipoContenido { get; set; }
        public virtual DbSet<TipoPregunta> TipoPregunta { get; set; }
        public virtual DbSet<TiposEvaluaciones> TiposEvaluaciones { get; set; }
        //public virtual DbSet<Usuarios> Usuarios { get; set; }
        //public virtual DbSet<UsuariosRoles> UsuariosRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MensajeChat>()
                .HasRequired(m => m.Usuarios)
                .WithMany(t => t.Chat1)
                .HasForeignKey(m => m.receiver_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MensajeChat>()
                .HasRequired(m => m.Usuarios1)
                .WithMany(t => t.Chat2)
                .HasForeignKey(m => m.sender_id)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<OpenSchool.Models.UsuariosAsp> UsuariosAsps { get; set; }
    }
}