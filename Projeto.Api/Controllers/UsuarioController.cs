using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projeto.Core.Context.UsuarioContext.UseCases.Autenticar;
using Projeto.Core.Context.UsuarioContext.UseCases.Criar;

namespace Projeto.Api.Controllers;

[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IRequestHandler<UsuarioCriarRequest, UsuarioCriarResponse> _handlerCriar;
    private readonly IRequestHandler<UsuarioAutenticarRequest, UsuarioAutenticarResponse> _handlerAutenticar;
    public UsuarioController(
            IRequestHandler<UsuarioCriarRequest, UsuarioCriarResponse> handlerCriar,
            IRequestHandler<UsuarioAutenticarRequest, UsuarioAutenticarResponse> handlerAutenticar
        )
    {
        _handlerCriar = handlerCriar;
        _handlerAutenticar = handlerAutenticar;
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

    [HttpPost("/v1/usuario/autenticar")]
    public async Task<UsuarioAutenticarResponse> AutenticarUsuarioAsync(
            [FromBody] UsuarioAutenticarRequest request
        )
    {
        var resposta = await _handlerAutenticar.Handle(request, new CancellationToken());

        return resposta;
    }
}
