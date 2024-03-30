
using Flunt.Notifications;
using Flunt.Validations;
using Projeto.Core.Context.CompartilhadoContext.UseCases.Contratos;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Criar
{
    public class ValidadorRequest : IValidador<UsuarioCriarRequest>
    {
        public Contract<Notification> GarantirRequisicao(UsuarioCriarRequest requisicao)
            => new Contract<Notification>()
                .Requires()
                .IsLowerOrEqualsThan(80, requisicao.PrimeiroNome.Length, "Primeiro Nome", "O nome não pode ter mais de 80 caracteres")
                .IsGreaterOrEqualsThan(4, requisicao.PrimeiroNome.Length, "Primeiro Nome", "O nome não pode ter menos de 4 caracteres")
                .IsLowerOrEqualsThan(80, requisicao.UltimoSobrenome.Length, "Último Sobrenome", "O nome não pode ter mais de 80 caracteres")
                .IsGreaterOrEqualsThan(4, requisicao.UltimoSobrenome.Length, "Último Sobrenome", "O nome não pode ter menos de 4 caracteres")
                .IsLowerOrEqualsThan(80, requisicao.Senha.Length, "Senha", "A senha não pode ter mais de 25 caracteres")
                .IsGreaterOrEqualsThan(4, requisicao.Senha.Length, "Senha", "A senha não pode ter menos de 6 caracteres")
                .IsEmail(requisicao.Email, "E-mail", "E-mail inválido");
    }
}
