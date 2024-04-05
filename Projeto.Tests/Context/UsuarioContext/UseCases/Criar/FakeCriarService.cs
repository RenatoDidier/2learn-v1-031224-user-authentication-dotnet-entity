
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.Criar.Contratos;

namespace Projeto.Tests.Context.UsuarioContext.UseCases.Criar
{
    public class FakeCriarService : IService
    {
        public Task<bool> EnviarEmailUsuario(Usuario usuario, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
