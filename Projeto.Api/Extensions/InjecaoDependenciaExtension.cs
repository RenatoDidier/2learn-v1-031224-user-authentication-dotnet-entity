
namespace Projeto.Api.Extensions
{
    public static class InjecaoDependenciaExtension
    {
        public static void AdicionarDependenciaRepositorios(this IServiceCollection services)
        {
            services.AddTransient<
                    Projeto.Core.Context.UsuarioContext.UseCases.Criar.Contratos.IRepository,
                    Projeto.Repository.Context.UsuarioContext.UseCases.Criar.Repository
                >();

            services.AddTransient<
                    Projeto.Core.Context.UsuarioContext.UseCases.Autenticar.Contratos.IRepository,
                    Projeto.Repository.Context.UsuarioContext.UseCases.Autenticar.Repository
                >();
        }
    }
}
