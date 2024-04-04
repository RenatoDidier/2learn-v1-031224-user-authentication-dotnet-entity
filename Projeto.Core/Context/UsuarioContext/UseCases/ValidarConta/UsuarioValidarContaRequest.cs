using MediatR;

namespace Projeto.Core.Context.UsuarioContext.UseCases.ValidarConta
{
    public record UsuarioValidarContaRequest(string Email, string CodigoValidacao) : IRequest<UsuarioValidarContaResponse>;
    
}
