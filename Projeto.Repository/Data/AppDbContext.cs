using Microsoft.EntityFrameworkCore;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Repository.Context.UsuarioContext.Mapping;

namespace Projeto.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Credencial> Credenciais { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new CredencialMap());
        }
    }
}
