using APINotificacionesV2.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace APINotificacionesV2.Models.Datos
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Notas> Notas { get; set; }
    }
}
