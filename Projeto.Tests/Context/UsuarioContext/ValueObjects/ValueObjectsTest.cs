using Projeto.Core.Context.UsuarioContext.ValueObjects;

namespace Projeto.Tests.Context.UsuarioContext.ValueObjects
{
    [TestClass]
    public class ValueObjectsTest
    {
        [TestMethod]
        [TestCategory("VO-Email")]
        [DataRow("emailinvalido")]
        public void Dado_um_email_invalido_deve_gerar_erro(string email)
        {
            Email emailTest = new(email);

            Assert.IsFalse(emailTest.IsValid);
        }

        [TestMethod]
        [TestCategory("VO-Email")]
        [DataRow("rendfv@gmail.com")]
        [DataRow("rendfv@hotmail.com")]
        [DataRow("rendfv@outlook.com")]
        [DataRow("rendfv@yahoo.com.br")]
        public void Dado_um_email_valido_deve_prosseguir(string email)
        {
            Email emailTest = new(email);

            Assert.IsTrue(emailTest.IsValid);
        }

        [TestMethod]
        [TestCategory("VO-Nome")]
        [DataRow("123", "SobrenomeCorreto")]
        [DataRow("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at nisl at diam nec.", "SobrenomeCorreto")]
        [DataRow("nomeCorreto", "123")]
        [DataRow("nomeCorreto", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at nisl at diam nec.")]
        public void Dado_um_nome_invalido_deve_gerar_erro(string nome, string sobrenome)
        {
            Nome nomeTeste = new(nome, sobrenome);

            nomeTeste.ValidarPreenchimentoDados();

            Assert.IsFalse(nomeTeste.IsValid);
        }

        [TestMethod]
        [TestCategory("VO-Nome")]
        [DataRow("NomeCorreto", "SobrenomeCorreto")]
        public void Dado_um_nome_valido_deve_prosseguir(string nome, string sobrenome)
        {
            Nome nomeTeste = new(nome, sobrenome);

            nomeTeste.ValidarPreenchimentoDados();

            Assert.IsTrue(nomeTeste.IsValid);
        }

        [TestMethod]
        [TestCategory("VO-Senha")]
        [DataRow("")]
        [DataRow("1234567")]
        [DataRow("01234567890123456789012345")]
        public void Dado_uma_senha_invalida_deve_gerar_erro(string senha)
        {
            Senha senhaTeste = new(senha);

            Assert.IsFalse(senhaTeste.IsValid);
        }

        [TestMethod]
        [TestCategory("VO-Senha")]
        [DataRow("01235678")]
        [DataRow("SenhaPadrao")]
        public void Dado_uma_senha_valida_deve_prosseguir(string senha)
        {
            Senha senhaTeste = new(senha);

            Assert.IsTrue(senhaTeste.IsValid);
        }


    }
}
