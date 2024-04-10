using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projeto.Core;
using Projeto.Repository.Data;
using System.Data.SqlClient;
using System.Text;

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


            ConfiguracoesGlobal.DadosSendGrid.ApiSendGrid =
                builder.Configuration.GetSection("SendGridSegredos").GetValue<string>("ChaveApi") ?? string.Empty;
            ConfiguracoesGlobal.DadosSendGrid.NomeRemetente =
                builder.Configuration.GetSection("SendGridSegredos").GetValue<string>("NomeRemetente") ?? string.Empty;
            ConfiguracoesGlobal.DadosSendGrid.EmailRemetente =
                builder.Configuration.GetSection("SendGridSegredos").GetValue<string>("EmailRemetente") ?? string.Empty;
        }

        public static void AdicionarMediator(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(x 
                => x.RegisterServicesFromAssembly(typeof(ConfiguracoesGlobal).Assembly));
        }

        public static void AdicionarAutenticacao(this WebApplicationBuilder builder)
        {
            byte[] chave = Encoding.ASCII.GetBytes(ConfiguracoesGlobal.SegredosSenha.JwtChave);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }
            );
        }

        public static void AdicionarConfiguracaoCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });
        }
    }
}
