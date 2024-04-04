using Flunt.Notifications;
using Flunt.Validations;
using System.Diagnostics.Contracts;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Autenticar
{
    public class ValidadorRequest
    {
        public static Contract<Notification> GarantirRequisicao(UsuarioAutenticarRequest requisicao)
            => new Contract<Notification>()
                .Requires()
                .IsEmail(requisicao.Email, "E-mail", "E-mail inválido")
                .IsLowerOrEqualsThan(requisicao.Senha, 25, "Senha", "A senha não pode ter mais de 25 caracteres")
                .IsGreaterOrEqualsThan(requisicao.Senha, 4, "Senha", "A senha não pode ter menos de 6 caracteres");
    }
}
