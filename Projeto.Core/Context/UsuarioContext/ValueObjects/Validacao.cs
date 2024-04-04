using Projeto.Core.Context.CompartilhadoContext.Extensions;
using Projeto.Core.Context.CompartilhadoContext.ValueObjects;

namespace Projeto.Core.Context.UsuarioContext.ValueObjects
{
    public class Validacao : ValueObject
    {
        public Validacao()
        {
            
        }
        public string Codigo { get; set; } = StringExtension.GerarSeisCaracteres();
        public DateTime LimiteValidacao { get; set; } = DateTime.UtcNow.AddMinutes(15);
        public DateTime? ValidacaoRealizada { get; set; }
        public bool UsuarioValidado => ValidacaoRealizada != null;

        public void RenovarCodigoValidacao()
        {
            Codigo = StringExtension.GerarSeisCaracteres();
            LimiteValidacao  = DateTime.UtcNow.AddMinutes(15);
        }

        public bool ValidarCodigoUsuario(string codigoUsuario)
        {
            bool enviarNovoEmail = false;
            bool validacaoInvalida = false;

            if (UsuarioValidado)
            {
                AddNotification("Codigo Verificação", "Este código já foi validado");
                validacaoInvalida = true;
            }

            if (LimiteValidacao < DateTime.UtcNow)
            {
                AddNotification("Codigo Verificação", "Este código não é mais válido. Um novo código foi gerado");
                enviarNovoEmail = true;
                validacaoInvalida = true;
            }

            if (!string.Equals(codigoUsuario.Trim(), Codigo.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                AddNotification("Código Verificação", "Código inválido");
                validacaoInvalida = true;
            }

            if (!validacaoInvalida)
                ValidacaoRealizada = DateTime.UtcNow;

            return enviarNovoEmail;
        }

    }
}
