using Projeto.Core.Context.UsuarioContext.Enum;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.Criar.Contratos;

namespace Projeto.Tests.Context.UsuarioContext.UseCases.Criar
{
    public class FakeCriarRepository : IRepository
    {
        public Task<Credencial?> BuscarCredencialAsync(CredencialEnum credencial, CancellationToken cancellationToken)
        {
            if (credencial.ToString() == "Convidado")
            {
                Credencial novaCredencial = new(credencial);
                novaCredencial.Id = 1;

                return Task.FromResult<Credencial?>(novaCredencial);
            }
            return Task.FromResult<Credencial?>(null);
        }

        public Task<bool> EfetuarCadastroUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            if (usuario.Email.ToString() == "novoemail@gmail.com")
                return Task.FromResult(true);

            return Task.FromResult(false);
        }

        public Task<bool> ValidarExistenciaUsuarioAsync(string email)
        {
            if (email.Contains("emailexistente@gmail.com"))
                return Task.FromResult(true);

            return Task.FromResult(false);
        }
    }
}
