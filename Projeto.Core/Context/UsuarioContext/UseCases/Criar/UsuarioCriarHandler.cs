using MediatR;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.Criar.Contratos;
using Projeto.Core.Context.UsuarioContext.ValueObjects;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Criar
{
    public class UsuarioCriarHandler : IRequestHandler<UsuarioCriarRequest, UsuarioCriarResponse>
    {
        private readonly IRepository _repository;

        public UsuarioCriarHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<UsuarioCriarResponse> Handle(UsuarioCriarRequest request, CancellationToken cancellationToken)
        {
            #region Fail Fast Validation
            var failFastValidation = ValidadorRequest.GarantirRequisicao(request);

            if (!failFastValidation.IsValid)
                return new UsuarioCriarResponse(400, null, failFastValidation.Notifications);
            #endregion

            #region Verificar se usuário existe na base
            var existeUsuario = await _repository.ValidarExistenciaUsuarioAsync(request.Email);

            if (existeUsuario)
                return new UsuarioCriarResponse(400, "E-mail cadastrado inválido");

            #endregion

            #region Criar Usuário
            Nome nome = new(request.PrimeiroNome, request.UltimoSobrenome);
            Senha senha = new(request.Senha);
            Email email = new(request.Email);
            Validacao validacao = new();
            email.Validacao = validacao;

            Usuario novoUsuario = new();
            novoUsuario.Nome = nome;
            novoUsuario.Senha = senha;
            novoUsuario.Email = email;
            novoUsuario.Credenciais = new();

            novoUsuario.AddNotifications(nome);
            novoUsuario.AddNotifications(senha);
            novoUsuario.AddNotifications(email);

            if (!novoUsuario.IsValid)
                return new UsuarioCriarResponse(401, "Dados incorretos", novoUsuario.Notifications);

            #endregion

            #region Validar se usuário existe

            foreach (var credencialUsuario in request.Credenciais)
            {
                var retornoCredencial = await _repository.BuscarCredencialAsync(credencialUsuario, new CancellationToken());

                if (retornoCredencial != null)
                    novoUsuario.Credenciais.Add(retornoCredencial);
                // Esse aqui pode ser utilizado para criar novas credenciais no banco, no entanto, já que estou utilizando ENUM, tudo já vai estar cadastrado previamente no banco
                //else
                //{
                //    Credencial novaCredencial = new();
                //    novaCredencial.Titulo = credencialUsuario;
                //    novoUsuario.Credenciais.Add(novaCredencial);
                //}
            }

            if (!novoUsuario.IsValid)
                return new UsuarioCriarResponse(403, "Ocorreu um problema para cadastrar o usuário");

            #endregion

            #region Cadastrar Usuário
            try
            {
                var criarUsuario = await _repository.EfetuarCadastroUsuarioAsync(novoUsuario, new CancellationToken());

                if (!criarUsuario)
                    return new UsuarioCriarResponse(403, "Não foi possível criar o seu usuário");

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UsuarioCriarResponse(500, "Problema para acessar o banco");
            }

            #endregion

            #region Envio de e-mail para usuário

            #endregion

            #region Retornar os valores para o usuário
            RespostaCriarUsuarioDto respostaUsuario = new(novoUsuario.Nome.ToString(), novoUsuario.Email.ToString());

            return new UsuarioCriarResponse("Usuário criado com sucesso", respostaUsuario);
            #endregion

        }
    }
}
