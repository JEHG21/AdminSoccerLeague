using Microsoft.EntityFrameworkCore;

namespace TorneosAdmin.Web.Models
{
    public class ModelEntities : DbContext
    {
        public ModelEntities() { }

        public ModelEntities(DbContextOptions<ModelEntities> options)
        : base(options)
        { }

        public DbSet<Arbitros> Arbitros { get; set; }
        public DbSet<Campeonatos> Campeonatos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Dirigentes> Dirigentes { get; set; }
        public DbSet<Equipos> Equipos { get; set; }
        public DbSet<EstadosCiviles> EstadosCiviles { get; set; }
        public DbSet<Inscripciones> Inscripciones { get; set; }
        public DbSet<Instrucciones> Instrucciones { get; set; }
        public DbSet<Jornadas> Jornadas { get; set; }
        public DbSet<Jugadores> Jugadores { get; set; }
        public DbSet<Ligas> Ligas { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
        public DbSet<PagosInscripciones> PagosInscripciones { get; set; }
        public DbSet<Parroquias> Parroquias { get; set; }
        public DbSet<Partidos> Partidos { get; set; }
        public DbSet<PartidosEstados> PartidosEstados { get; set; }
        public DbSet<PartidosJugadores> PartidosJugadores { get; set; }
        public DbSet<Pases>Pases { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Profesiones> Profesiones { get; set; }
        public DbSet<Provincias> Provincias { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<TiposPagos> TiposPagos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<UsuariosLogins> UsuariosLogins { get; set; }
        public DbSet<UsuariosRoles> UsuariosRoles { get; set; }

    }
}
