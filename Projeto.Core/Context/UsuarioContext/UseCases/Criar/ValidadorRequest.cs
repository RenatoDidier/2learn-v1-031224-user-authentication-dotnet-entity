
using Flunt.Notifications;
using Flunt.Validations;
using Projeto.Core.Context.CompartilhadoContext.UseCases.Contratos;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Criar
{
    public class ValidadorRequest
    {
        public static Contract<Notification> GarantirRequisicao(UsuarioCriarRequest requisicao)
            => new Contract<Notification>()
                .Requires()
                .IsLowerOrEqualsThan(requisicao.PrimeiroNome, 80, "Primeiro Nome", "O nome não pode ter mais de 80 caracteres")
                .IsGreaterOrEqualsThan(requisicao.PrimeiroNome, 4, "Primeiro Nome", "O nome não pode ter menos de 4 caracteres")
                .IsLowerOrEqualsThan(requisicao.UltimoSobrenome, 80, "Último Sobrenome", "O nome não pode ter mais de 80 caracteres")
                .IsGreaterOrEqualsThan(requisicao.UltimoSobrenome, 4, "Último Sobrenome", "O nome não pode ter menos de 4 caracteres")
                .IsLowerOrEqualsThan(requisicao.Senha, 80, "Senha", "A senha não pode ter mais de 25 caracteres")
                .IsGreaterOrEqualsThan(requisicao.Senha, 4, "Senha", "A senha não pode ter menos de 6 caracteres")
                .IsGreaterOrEqualsThan(requisicao.Credenciais.Length, 1, "Credenciais", "É necessário ter ao menos uma credencial")
                .IsEmail(requisicao.Email, "E-mail", "E-mail inválido");
    }
}
