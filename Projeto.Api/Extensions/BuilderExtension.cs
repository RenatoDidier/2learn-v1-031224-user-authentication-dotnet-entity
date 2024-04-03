using Microsoft.EntityFrameworkCore;
using Projeto.Core;
using Projeto.Repository.Data;
using System.Data.SqlClient;

namespace Projeto.Api.Extensions
{
    public static class BuilderExtension
    {
        public static void AdicionarBanco(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConfiguracoesGlobal.Banco.StringConexao, b => b.MigrationsAssembly("Projeto.Api")));
        }

        public static void AdicionarConfiguracaoSegredos(this WebApplicationBuilder builder)
        {
            ConfiguracoesGlobal.Banco.StringConexao =
                builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            ConfiguracoesGlobal.SegredosSenha.ChaveSalt =
                builder.Configuration.GetSection("SenhaSegredos").GetValue<string>("ChaveSalt") ?? string.Empty;
            ConfiguracoesGlobal.SegredosSenha.JwtChave =
                builder.Configuration.GetSection("SenhaSegredos").GetValue<string>("JwtChave") ?? string.Empty;
        }

        public static void AdicionarMediator(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(x 
                => x.RegisterServicesFromAssembly(typeof(ConfiguracoesGlobal).Assembly));
        }
    }
}
