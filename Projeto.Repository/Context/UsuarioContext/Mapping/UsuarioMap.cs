using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Core.Context.UsuarioContext.Models;

namespace Projeto.Repository.Context.UsuarioContext.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            #region 01. Propriedade da class
            builder.Property(x => x.Id)
                .IsRequired(true)
                .HasColumnName("Id")
                .HasColumnType("VARCHAR")
                .HasMaxLength(8);

            #endregion

            #region 02. Value Objects
            builder.OwnsOne(x => x.Nome, nome =>
            {
                nome.Property(n => n.PrimeiroNome)
                    .IsRequired(true)
                    .HasColumnName("PrimeiroNome")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(80);

                nome.Property(n => n.UltimoSobrenome)
                    .IsRequired(true)
                    .HasColumnName("UltimoSobrenome")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(80);

                nome.Ignore(n => n.Notifications);
            });
            builder.OwnsOne(x => x.Senha, senha =>
            {
                senha.Property(s => s.HashSenha)
                    .IsRequired(true)
                    .HasColumnName("HashSenha")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(120);

                senha.Ignore(s => s.Notifications);
            });

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(e => e.Endereco)
                    .IsRequired(true)
                    .HasColumnName("Endereco")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(120);

                email.Ignore(e => e.Notifications);

                email.HasIndex(e => e.Endereco).IsUnique();

                email.OwnsOne(x => x.Validacao, validacao =>
                {
                    validacao.Property(v => v.Codigo)
                        .IsRequired(true)
                        .HasColumnName("Codigo")
                        .HasColumnType("VARCHAR")
                        .HasMaxLength(6);

                    validacao.Property(v => v.LimiteValidacao)
                        .IsRequired(true)
                        .HasColumnName("LimiteValidacao")
                        .HasColumnType("SMALLDATETIME");

                    validacao.Property(v => v.ValidacaoRealizada)
                        .IsRequired(false)
                        .HasColumnName("ValidacaoRealizada")
                        .HasColumnType("SMALLDATETIME");

                    validacao.Ignore(v => v.Notifications);
                    validacao.Ignore(v => v.UsuarioValidado);
                });
                    
            });
            #endregion

            #region 03. N:N Credenciais
            builder.HasMany(x => x.Credenciais)
                .WithMany(x => x.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioCredencial",

                    l => l.HasOne<Credencial>()
                        .WithMany()
                        .HasForeignKey("CredencialId")
                        .HasConstraintName("FK_UsuarioCredencial_CredencialId")
                        .OnDelete(DeleteBehavior.Cascade),

                    r => r.HasOne<Usuario>()
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK_UsuarioCredencial_UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)

                );
            #endregion

            #region 04. Propriedades não mapeadas

            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.IsValid);

            #endregion


        }
    }
}
