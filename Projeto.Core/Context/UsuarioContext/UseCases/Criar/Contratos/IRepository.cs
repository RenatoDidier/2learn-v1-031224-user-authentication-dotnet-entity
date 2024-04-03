using Projeto.Core.Context.UsuarioContext.Enum;
using Projeto.Core.Context.UsuarioContext.Models;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Criar.Contratos
{
    public interface IRepository
    {
        Task<bool> ValidarExistenciaUsuarioAsync(string email);
        Task<bool> EfetuarCadastroUsuarioAsync(Usuario usuario, CancellationToken cancellationToken);
        Task<Credencial?> BuscarCredencialAsync(CredencialEnum credencial, CancellationToken cancellationToken);
    }
}
