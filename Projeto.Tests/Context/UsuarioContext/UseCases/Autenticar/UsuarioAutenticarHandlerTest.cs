using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Core.Context.UsuarioContext.UseCases.Autenticar;

namespace Projeto.Tests.Context.UsuarioContext.UseCases.Autenticar
{
    [TestClass]
    public class UsuarioAutenticarHandlerTest
    {
        private readonly UsuarioAutenticarHandler _handler;
        public UsuarioAutenticarHandlerTest()
        {
            _handler = new UsuarioAutenticarHandler(new FakeAutenticarRepository());
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("emailinvalido", "senha123")]
        [DataRow("emailvalido@gmail.com", "sen")]
        [DataRow("emailvalido@gmail.com", "012345678901234567890123456789")]
        public void Dado_um_request_invalido_deve_gerar_erro(string email, string senha)
        {
            UsuarioAutenticarRequest request = new(email, senha);

            var retorno = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(400, retorno.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("emailnaoencontrado@gmail.com", "senha123")]
        public void Dado_um_request_com_email_inexistente_deve_gerar_erro(string email, string senha)
        {
            UsuarioAutenticarRequest request = new(email, senha);

            var retorno = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(401, retorno.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("emailcorretonaovalidado@gmail.com", "senha123")]
        public void Dado_um_request_com_email_nao_validado_deve_gerar_erro(string email, string senha)
        {
            UsuarioAutenticarRequest request = new(email, senha);

            var retorno = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(402, retorno.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("emailcorretovalidado@gmail.com", "senhaInvalida")]
        public void Dado_um_request_com_email_validado_mas_senha_invalida_deve_gerar_erro(string email, string senha)
        {
            UsuarioAutenticarRequest request = new(email, senha);

            var retorno = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(403, retorno.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("emailcorretovalidado@gmail.com", "senhaValida")]
        public void Dado_um_request_com_email_validado_e_senha_correta_deve_prosseguir(string email, string senha)
        {
            UsuarioAutenticarRequest request = new(email, senha);

            var retorno = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(201, retorno.Result.Status);
        }
    }
}
