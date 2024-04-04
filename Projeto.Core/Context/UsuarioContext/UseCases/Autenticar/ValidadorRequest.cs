using Flunt.Notifications;
using Flunt.Validations;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Autenticar
{
    public class ValidadorRequest
    {
        public static Contract<Notification> GarantirRequisicao(UsuarioAutenticarRequest request)
            => new Contract<Notification>()
                .Requires()
                .IsEmail(request.Email, "E-mail", "E-mail inválido")
                .IsTrue(request.CodigoValidacao.Length == 6, "Código validação", "Código de validação inválido");
    }
}
