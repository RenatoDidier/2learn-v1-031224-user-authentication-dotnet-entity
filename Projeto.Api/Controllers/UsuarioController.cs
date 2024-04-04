using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projeto.Core.Context.UsuarioContext.UseCases.ValidarConta;
using Projeto.Core.Context.UsuarioContext.UseCases.Autenticar;
using Projeto.Core.Context.UsuarioContext.UseCases.Criar;
using Projeto.Api.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Projeto.Api.Controllers;

[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IRequestHandler<UsuarioCriarRequest, UsuarioCriarResponse> _handlerCriar;
    private readonly IRequestHandler<UsuarioValidarContaRequest, UsuarioValidarContaResponse> _handlerValidarConta;
    private readonly IRequestHandler<UsuarioAutenticarRequest, UsuarioAutenticarResponse> _handlerAutenticar;
    public UsuarioController(
            IRequestHandler<UsuarioCriarRequest, UsuarioCriarResponse> handlerCriar,
            IRequestHandler<UsuarioValidarContaRequest, UsuarioValidarContaResponse> handlerValidarConta,
            IRequestHandler<UsuarioAutenticarRequest, UsuarioAutenticarResponse> handlerAutenticar
        )
    {
        _handlerCriar = handlerCriar;
        _handlerValidarConta = handlerValidarConta;
        _handlerAutenticar = handlerAutenticar;
    }
    [HttpGet("/")]
    public string Get()
    {
        return "Conectado";
    }

    [HttpPost("/v1/usuario/criar")]
    public async Task<UsuarioCriarResponse> CriarUsuarioAsync(
            [FromBody] UsuarioCriarRequest requisicao
        )
    {
        var resposta = await _handlerCriar.Handle(requisicao, new CancellationToken());

        return resposta;
    }

    [HttpPost("/v1/usuario/validar-conta")]
    public async Task<UsuarioValidarContaResponse> ValidarContaUsuarioAsync(
            [FromBody] UsuarioValidarContaRequest requisicao
        )
    {
        var resposta = await _handlerValidarConta.Handle(requisicao, new CancellationToken());

        return resposta;
    }

    [HttpPost("/v1/usuario/autenticar")]
    public async Task<UsuarioAutenticarResponse> LoginAsync(
            [FromBody] UsuarioAutenticarRequest requisicao
        )
    {
        var resposta = await _handlerAutenticar.Handle(requisicao, new CancellationToken());

        if (!resposta.HaErro && resposta.DadosRetorno != null)
            resposta.DadosRetorno.Token = JwtTokenExtension.GerarTokenJwt(resposta.DadosRetorno);

        return resposta;
    }

    [Authorize]
    [HttpGet("/v1/blog/principal")]
    public string AcessarTelaPrincipal()
    {
        return "Tela principal";
    }

    [Authorize(Policy = "Convidado")]
    [HttpGet("/v1/blog/convidado")]
    public string AcessarTelaConvidado()
    {
        return "Tela convidado";
    }

    [Authorize(Policy = "Usuario")]
    [HttpGet("/v1/blog/usuario")]
    public string AcessarTelaUsuario()
    {
        return "Tela usuário";
    }

    [Authorize(Policy = "Administrador")]
    [HttpGet("/v1/blog/administrador")]
    public string AcessarTelaAdministrador()
    {
        return "Tela administrador";
    }

}
