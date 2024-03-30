namespace Projeto.Core.Context.UsuarioContext.UseCases.Criar
{
    public record UsuarioCriarRequest(
            string PrimeiroNome,
            string UltimoSobrenome,
            string Senha,
            string Email
        );
  
}
