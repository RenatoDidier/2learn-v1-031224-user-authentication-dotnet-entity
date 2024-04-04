using Flunt.Notifications;
using Flunt.Validations;

namespace Projeto.Core.Context.UsuarioContext.UseCases.ValidarConta
{
    public class ValidadorRequest
    {
        public static Contract<Notification> GarantirRequisicao(UsuarioValidarContaRequest request)
            => new Contract<Notification>()
                .Requires()
                .IsEmail(request.Email, "E-mail", "E-mail inválido")
                .IsTrue(request.CodigoValidacao.Length == 6, "Código validação", "Código de validação inválido");
    }
}
