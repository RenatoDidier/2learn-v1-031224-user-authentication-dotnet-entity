using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projeto.Core.Context.UsuarioContext.UseCases.Criar;

namespace Projeto.Api.Controllers;

[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IRequestHandler<UsuarioCriarRequest, UsuarioCriarResponse> _handlerCriar;
    public UsuarioController(
            IRequestHandler<UsuarioCriarRequest, UsuarioCriarResponse> handlerCriar
        )
    {
        _handlerCriar = handlerCriar;
    }
    [HttpGet("/")]
    public string Get()
    {
        return "Conectado";
    }

    [HttpPost("/v1/usuario/criar")]
    public async Task<UsuarioCriarResponse> CriarUsuarioAsync(
            [FromBody] UsuarioCriarRequest request
        )
    {
        var resposta = await _handlerCriar.Handle(request, new CancellationToken());

        return resposta;
    }
}
