using CursoEFCore.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=cursoEFCore;User Id=root;Password=;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}