using Projeto.Core.Context.UsuarioContext.Models;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Autenticar.Contratos
{
    public interface IRepository
    {
        Task<Usuario?> BuscarUsuarioCompletoAsync(string email, CancellationToken cancellationToken);
    }
}
