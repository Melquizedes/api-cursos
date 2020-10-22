using API.Cast.Cursos.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Cast.Cursos.Data
{
    public class CursoContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; } 
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CursosApi;Data Source=DESKTOP-2PORJ7Q\\SQLEXPRESS");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().HasKey(c => c.Id);
            modelBuilder.Entity<Categoria>().HasKey(c => c.Codigo);
            
        }
    }
}
