using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Core.Context.UsuarioContext.Models;

namespace Projeto.Repository.Context.UsuarioContext.Mapping
{
    public class CredencialMap : IEntityTypeConfiguration<Credencial>
    {
        public void Configure(EntityTypeBuilder<Credencial> builder)
        {
            builder.ToTable("Credencial");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Titulo)
                .IsRequired(true)
                .HasColumnName("Titulo")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.HasIndex(x => x.Titulo, "IX_Credencial_Titulo")
                .IsUnique();

        }
    }
}
