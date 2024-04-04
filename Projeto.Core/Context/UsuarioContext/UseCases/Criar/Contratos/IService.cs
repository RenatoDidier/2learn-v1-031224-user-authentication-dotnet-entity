using Projeto.Core.Context.UsuarioContext.Models;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Criar.Contratos
{
    public interface IService
    {
        Task<bool> EnviarEmailUsuario(Usuario usuario, CancellationToken cancellationToken);
    }
}
