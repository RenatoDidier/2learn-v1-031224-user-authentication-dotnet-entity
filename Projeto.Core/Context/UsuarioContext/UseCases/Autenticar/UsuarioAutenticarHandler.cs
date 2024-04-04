using MediatR;
using Projeto.Core.Context.UsuarioContext.UseCases.Autenticar.Contratos;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Autenticar
{
    public class UsuarioAutenticarHandler : IRequestHandler<UsuarioAutenticarRequest, UsuarioAutenticarResponse>
    {
        private readonly IRepository _repository;
        public UsuarioAutenticarHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<UsuarioAutenticarResponse> Handle(UsuarioAutenticarRequest request, CancellationToken cancellationToken)
        {
            #region 01. Fail Fast Validation
            var failFastValidation = ValidadorRequest.GarantirRequisicao(request);

            if (!failFastValidation.IsValid)
                return new UsuarioAutenticarResponse(400, "Erro na requisição", failFastValidation.Notifications);
            #endregion

            #region 02. Buscar Usuário
            var usuarioEncontrado = await _repository.BuscarUsuarioAsync(request.Email, new CancellationToken());

            if (usuarioEncontrado == null)
                return new UsuarioAutenticarResponse(401, "E-mail de validação incorreto");
            #endregion

            #region 03. Verificar o Código - Caso desatualizado, reenviar
            bool codigoExpirado = usuarioEncontrado.Email.Validacao.ValidarCodigoUsuario(request.CodigoValidacao);

            if (codigoExpirado)
            {
                var gerarNovoCodigo = await _repository.RenovarCodigoVerificacaoAsync(usuarioEncontrado, new CancellationToken());

                if (!gerarNovoCodigo)
                    return new UsuarioAutenticarResponse(402, "Problema para gerar um novo código");

            }
                

            if (!usuarioEncontrado.Email.Validacao.IsValid)
                return new UsuarioAutenticarResponse(402, "Código Validação", usuarioEncontrado.Email.Validacao.Notifications);
            #endregion

            #region 04. Validar o Código
            try
            {
                var resultado = await _repository.ValidarContaUsuarioAsync(usuarioEncontrado, new CancellationToken());

                if (!resultado)
                    return new UsuarioAutenticarResponse(403, "Problema para autenticar o usuário");

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UsuarioAutenticarResponse(500, "Problema para acessar o banco");
            }
            #endregion

            #region 05. Retornar para o usuário
            return new UsuarioAutenticarResponse("Usuário autenticado com sucesso");
            #endregion
        }
    }
}
