using Microsoft.EntityFrameworkCore;
using AsistenciaApi.Models;

namespace AsistenciaApi.Data
{
    public class AsistenciaDbContext : DbContext
    {
        public AsistenciaDbContext(DbContextOptions<AsistenciaDbContext> options) : base(options) { }

        public DbSet<RegistroAsistencia> RegistrosAsistencia { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
