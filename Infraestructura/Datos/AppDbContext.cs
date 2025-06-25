
using Dominio.Entidad;
using Microsoft.EntityFrameworkCore;


namespace Infraestructura.Datos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Cliente> cliente { get; set; }
    }
}
