
using MediatR;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.Autenticar.Contratos;
using Projeto.Core.Context.UsuarioContext.ValueObjects;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Autenticar
{
    public class UsuarioAutenticarHandler : IRequestHandler<UsuarioAutenticarRequest, UsuarioAutenticarResponse>
    {
        private readonly IRepository _repository;

        public UsuarioAutenticarHandler(IRepository repository)
            => _repository = repository;            
        public async Task<UsuarioAutenticarResponse> Handle(UsuarioAutenticarRequest request, CancellationToken cancellationToken)
        {
            #region 01. Verificar Requisição
            var failFastValidation = ValidadorRequest.GarantirRequisicao(request);

            if (!failFastValidation.IsValid)
                return new UsuarioAutenticarResponse(400, "Problema na requisição", failFastValidation.Notifications);
            #endregion

            #region 02. Buscar usuário completo no banco
            Usuario? usuarioEncontrado = new();

            try
            {
                usuarioEncontrado = await _repository.BuscarUsuarioCompletoAsync(request.Email, new CancellationToken());

                if (usuarioEncontrado == null)
                    return new UsuarioAutenticarResponse(401, "E-mail inválido");

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UsuarioAutenticarResponse(500, "Problema para acessar o banco");
            }
            #endregion

            #region 03. Validar se conta está ativa
            if (!usuarioEncontrado.Email.Validacao.UsuarioValidado)
                return new UsuarioAutenticarResponse(402, "Usuário ainda não foi validado");
            #endregion

            #region 04. Validar se a senha passada é a correta
            if (!Senha.ValidarHash(request.Senha, usuarioEncontrado.Senha.HashSenha))
                return new UsuarioAutenticarResponse(403, "Usuário inválido");
            #endregion

            #region 05. Preencher informações de retorno
            DadosRetornoUsuarioAutenticado dadosRetorno = new();
            dadosRetorno.Id = usuarioEncontrado.Id;
            dadosRetorno.Nome = usuarioEncontrado.Nome.ToString();
            dadosRetorno.Email = usuarioEncontrado.Email.ToString();

            dadosRetorno.Credenciais = usuarioEncontrado.Credenciais.Select(x => x.Titulo);
            #endregion

            #region 06. Retornar dados para o usuário
            return new UsuarioAutenticarResponse("Login efetuado com sucesso", dadosRetorno);
            #endregion
        }
    }
}
