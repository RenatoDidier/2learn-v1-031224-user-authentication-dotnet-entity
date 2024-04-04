using MediatR;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Autenticar
{
    public record UsuarioAutenticarRequest(string Email, string CodigoValidacao) : IRequest<UsuarioAutenticarResponse>;
    
}
