using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Core.Context.UsuarioContext.UseCases.ValidarConta;
using Projeto.Tests.Context.UsuarioContext.UseCases.Criar;

namespace Projeto.Tests.Context.UsuarioContext.UseCases.ValidarConta
{
    [TestClass]
    public class UsuarioValidarContaHandlerTest
    {
        private readonly UsuarioValidarContaHandler _handler;
        public UsuarioValidarContaHandlerTest()
        {
            _handler = new UsuarioValidarContaHandler(new FakeValidarContaRepository(), new FakeCriarService());
        }

        [TestMethod]
        [TestCategory("Handler-ValidarConta")]
        [DataRow("email@gmail.com", "12345")]
        [DataRow("emailinvalido", "1234567")]
        public void Dado_um_request_invalido_deve_gerar_erro(string email, string senha)
        {
            UsuarioValidarContaRequest request = new(email, senha);

            var retorno = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(400, retorno.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-ValidarConta")]
        [DataRow("emailexistentecodigoexpirado@gmail.com", "123456")]
        public void Dado_um_email_com_codigo_expirado_deve_gerar_erro(string email, string senha)
        {
            UsuarioValidarContaRequest request = new(email, senha);

            var retorno = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(404, retorno.Result.Status);
        }


        [TestMethod]
        [TestCategory("Handler-ValidarConta")]
        [DataRow("emailexistente@gmail.com", "123456")]
        public void Dado_um_codigo_invalido_deve_gerar_erro(string email, string senha)
        {
            UsuarioValidarContaRequest request = new(email, senha);

            var retorno = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(402, retorno.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-ValidarConta")]
        [DataRow("emailexistente@gmail.com", "000000")]
        public void Dado_um_request_valido_deve_prosseguir(string email, string senha)
        {
            UsuarioValidarContaRequest request = new(email, senha);

            var retorno = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(201, retorno.Result.Status);
        }
    }
}
