using Projeto.Core.Context.UsuarioContext.Enum;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.Autenticar.Contratos;
using Projeto.Core.Context.UsuarioContext.ValueObjects;

namespace Projeto.Tests.Context.UsuarioContext.UseCases.Autenticar
{
    public class FakeAutenticarRepository : IRepository
    {
        public Task<Usuario?> BuscarUsuarioCompletoAsync(string email, CancellationToken cancellationToken)
        {

            if (email.Contains("emailcorretovalidado@gmail.com") || email.Contains("emailcorretonaovalidado@gmail.com"))
            {
                Usuario fakeUsuario = new();

                var nome = new Nome("Usuário", "TotalmenteFake");
                var senha = new Senha("senhaValida");
                var emailUsuario = new Email(email);
                var novaCredencial = new Credencial(CredencialEnum.Convidado);

                fakeUsuario.Nome = nome;
                fakeUsuario.Email = emailUsuario;
                fakeUsuario.Senha = senha;
                fakeUsuario.Email.Validacao = new Validacao();
                fakeUsuario.Credenciais = new List<Credencial>
                {
                    novaCredencial
                };

                if (email.Contains("emailcorretovalidado@gmail.com"))
                {
                    fakeUsuario.Email.Validacao.ValidacaoRealizada = DateTime.UtcNow;
                }
                return Task.FromResult<Usuario?>(fakeUsuario);
            }
            return Task.FromResult<Usuario?>(null);
            
        }
    }
}
