using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Core.Context.UsuarioContext.Enum;
using Projeto.Core.Context.UsuarioContext.UseCases.Criar;

namespace Projeto.Tests.Context.UsuarioContext.UseCases.Criar
{
    [TestClass]
    public class UsuarioCriarHandlerTest
    {
        private readonly UsuarioCriarHandler _handler;
        public UsuarioCriarHandlerTest()
        {
            _handler = new UsuarioCriarHandler(new FakeCriarRepository(), new FakeCriarService());
        }

        [TestMethod]
        [TestCategory("Handler-Criar")]
        [DataRow("123", "SobrenomeValido", "SenhaValida", "emailvalido@gmail.com", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("NomeValido", "123", "SenhaValida", "emailvalido@gmail.com", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("NomeValido", "SobrenomeValido", "123", "emailinvalido", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("NomeValido", "SobrenomeValido", "SenhaValida", "emailvalido@gmail.com", new CredencialEnum[] {})]
        [DataRow("NomeValido", "SobrenomeValido", "012345678901234567890123456", "emailvalido@gmail.com", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque vitae arcu semper, tristique eros nulla.", "SobrenomeValido", "SenhaValida", "emailvalido@gmail.com", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("NomeValido", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque vitae arcu semper, tristique eros nulla.", "SenhaValida", "emailvalido@gmail.com", new CredencialEnum[] { CredencialEnum.Convidado })]

        public void Dado_um_request_invalido_deve_gerar_erro(
                string primeiroNome,
                string ultimoSobrenome,
                string senha,
                string email,
                CredencialEnum[] credenciais
            )
        {
            UsuarioCriarRequest requisicao = new(primeiroNome, ultimoSobrenome, senha, email, credenciais);

            var retorno = _handler.Handle(requisicao, new CancellationToken());

            Assert.AreEqual(400, retorno.Result.Status);
        }


        [TestMethod]
        [TestCategory("Handler-Criar")]
        [DataRow("NomeValido", "SobrenomeValido", "SenhaValida", "emailexistente@gmail.com", new CredencialEnum[] { CredencialEnum.Convidado })]

        public void Dado_um_email_existente_deve_gerar_erro(
                string primeiroNome,
                string ultimoSobrenome,
                string senha,
                string email,
                CredencialEnum[] credenciais
            )
        {
            UsuarioCriarRequest requisicao = new(primeiroNome, ultimoSobrenome, senha, email, credenciais);

            var retorno = _handler.Handle(requisicao, new CancellationToken());

            Assert.AreEqual(401, retorno.Result.Status);
        }


        [TestMethod]
        [TestCategory("Handler-Criar")]
        [DataRow("NomeValido", "SobrenomeValido", "SenhaValida", "novoemail@gmail.com", new CredencialEnum[] { CredencialEnum.Convidado })]

        public void Dado_uma_usuario_valido_deve_prosseguir(
                string primeiroNome,
                string ultimoSobrenome,
                string senha,
                string email,
                CredencialEnum[] credenciais
            )
        {
            UsuarioCriarRequest requisicao = new(primeiroNome, ultimoSobrenome, senha, email, credenciais);

            var retorno = _handler.Handle(requisicao, new CancellationToken());

            Assert.AreEqual(201, retorno.Result.Status);
        }
    }
}
