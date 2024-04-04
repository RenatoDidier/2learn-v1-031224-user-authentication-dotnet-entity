namespace Projeto.Api.Extensions
{
    public static class ConfiguracaoPoliciesExtension
    {
        public static void AdicionarPolicies(this WebApplicationBuilder builder) 
        {
            builder.Services.AddAuthorization(
                option =>
                    {
                        option.AddPolicy("Convidado", p => p.RequireRole("Convidado", "Usuario", "Administrador"));
                        option.AddPolicy("Usuario", p => p.RequireRole("Usuario", "Administrador"));
                        option.AddPolicy("Administrador", p => p.RequireRole("Administrador"));
                    }
                );
        }
    }
}
