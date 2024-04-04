using Projeto.Core.Context.UsuarioContext.Models;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Autenticar.Contratos
{
    public interface IRepository
    {
        Task<Usuario?> BuscarUsuarioAsync(string email, CancellationToken cancellationToken);
        Task<bool> ValidarContaUsuarioAsync(Usuario usuario, CancellationToken cancellationToken);
        Task<bool> RenovarCodigoVerificacaoAsync(Usuario usuario, CancellationToken cancellationToken);
    }
}
