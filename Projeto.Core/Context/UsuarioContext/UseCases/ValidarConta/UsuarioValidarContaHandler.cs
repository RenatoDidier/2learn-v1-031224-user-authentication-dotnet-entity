using MediatR;

namespace Projeto.Core.Context.UsuarioContext.UseCases.ValidarConta
{
    public class UsuarioValidarContaHandler : IRequestHandler<UsuarioValidarContaRequest, UsuarioValidarContaResponse>
    {
        private readonly ValidarConta.Contratos.IRepository _repository;
        private readonly Criar.Contratos.IService _service;
        public UsuarioValidarContaHandler(ValidarConta.Contratos.IRepository repository, Criar.Contratos.IService service)
        {
            _repository = repository;
            _service = service;
        }
        public async Task<UsuarioValidarContaResponse> Handle(UsuarioValidarContaRequest request, CancellationToken cancellationToken)
        {
            #region 01. Fail Fast Validation
            var failFastValidation = ValidadorRequest.GarantirRequisicao(request);

            if (!failFastValidation.IsValid)
                return new UsuarioValidarContaResponse(400, "Erro na requisição", failFastValidation.Notifications);
            #endregion

            #region 02. Buscar Usuário
            var usuarioEncontrado = await _repository.BuscarUsuarioAsync(request.Email, new CancellationToken());

            if (usuarioEncontrado == null)
                return new UsuarioValidarContaResponse(401, "E-mail de validação incorreto");
            #endregion

            #region 03. Verificar o Código - Caso desatualizado, reenviar
            bool codigoExpirado = usuarioEncontrado.Email.Validacao.ValidarCodigoUsuario(request.CodigoValidacao);

            if (codigoExpirado)
            {
                var gerarNovoCodigo = await _repository.RenovarCodigoVerificacaoAsync(usuarioEncontrado, new CancellationToken());

                if (!gerarNovoCodigo)
                    return new UsuarioValidarContaResponse(402, "Problema para gerar um novo código");

                var envioEmailUsuario = await _service.EnviarEmailUsuario(usuarioEncontrado, new CancellationToken());

                if (!envioEmailUsuario)
                    return new UsuarioValidarContaResponse(403, "Problema para enviar o código para o usuário");

            }
                

            if (!usuarioEncontrado.Email.Validacao.IsValid)
                return new UsuarioValidarContaResponse(402, "Código Validação", usuarioEncontrado.Email.Validacao.Notifications);
            #endregion

            #region 04. Validar o Código
            try
            {
                var resultado = await _repository.ValidarContaUsuarioAsync(usuarioEncontrado, new CancellationToken());

                if (!resultado)
                    return new UsuarioValidarContaResponse(403, "Problema para autenticar o usuário");

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UsuarioValidarContaResponse(500, "Problema para acessar o banco");
            }
            #endregion

            #region 05. Retornar para o usuário
            return new UsuarioValidarContaResponse("Usuário autenticado com sucesso");
            #endregion
        }
    }
}
