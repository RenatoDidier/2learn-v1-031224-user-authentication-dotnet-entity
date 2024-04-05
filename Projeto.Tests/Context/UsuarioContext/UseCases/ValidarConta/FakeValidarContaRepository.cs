using Projeto.Core.Context.UsuarioContext.Enum;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.ValidarConta.Contratos;
using Projeto.Core.Context.UsuarioContext.ValueObjects;

namespace Projeto.Tests.Context.UsuarioContext.UseCases.ValidarConta
{
    public class FakeValidarContaRepository : IRepository
    {
        public Task<Usuario?> BuscarUsuarioAsync(string email, CancellationToken cancellationToken)
        {
            if (email.Contains("emailexistente@gmail.com") || email.Contains("emailexistentecodigoexpirado@gmail.com"))
            {
                Usuario fakeUsuario = new();

                var nome = new Nome("Usuário", "TotalmenteFake");
                var senha = new Senha("senhaValida");
                var emailUsuario = new Email(email);

                fakeUsuario.Nome = nome;
                fakeUsuario.Email = emailUsuario;
                fakeUsuario.Senha = senha;
                fakeUsuario.Email.Validacao = new Validacao();
                fakeUsuario.Email.Validacao.Codigo = "000000";

                if (email.Contains("emailexistentecodigoexpirado@gmail.com"))
                    fakeUsuario.Email.Validacao.LimiteValidacao = DateTime.UtcNow.AddMinutes(-15);

                return Task.FromResult<Usuario?>(fakeUsuario);
            }
            
            return Task.FromResult<Usuario?>(null);
        }

        public Task<bool> RenovarCodigoVerificacaoAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }

        public Task<bool> ValidarContaUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
        {

            return Task.FromResult(true);
        }
    }
}
