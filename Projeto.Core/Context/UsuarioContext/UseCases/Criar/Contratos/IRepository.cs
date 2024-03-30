using Projeto.Core.Context.UsuarioContext.Models;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Criar.Contratos
{
    public interface IRepository
    {
        Task<bool> ValidarExistenciaUsuarioAsync(string email, CancellationToken cancellationToken);
        Task<bool> EfetuarCadastroUsuarioAsync(Usuario usuario, CancellationToken cancellationToken);
    }
}
