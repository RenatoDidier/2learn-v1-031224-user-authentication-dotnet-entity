using Projeto.Core.Context.CompartilhadoContext.ValueObjects;
using System.Text.RegularExpressions;

namespace Projeto.Core.Context.UsuarioContext.ValueObjects
{
    public partial class Email : ValueObject
    {
        #region Variáveis
        private const string PadraoRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        #endregion

        #region Constructors
        public Email()
        {
            
        }
        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                AddNotification("E-mail", "Endereço de e-mail inválido");

            Endereco = endereco.Trim().ToLower();

            if (!EmailRegex().IsMatch(Endereco))
                AddNotification("E-mail", "Endereço de e-mail inválido");
        }
        #endregion

        #region Propriedades
        public string Endereco { get; set; } = string.Empty;
        public Validacao Validacao { get; set; } = new();
        #endregion

        #region Conversão implícita && Auxiliares
        public override string ToString()
            => Endereco;

        public static implicit operator string(Email email)
            => email.ToString();

        public static implicit operator Email(string endereco)
            => new(endereco);
        #endregion

        #region Validação do Regex no E-mail
        [GeneratedRegex(PadraoRegex)]
        private static partial Regex EmailRegex();
        #endregion
    }
}
