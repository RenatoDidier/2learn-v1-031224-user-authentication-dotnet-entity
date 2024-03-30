using Projeto.Core.Context.CompartilhadoContext.ValueObjects;

namespace Projeto.Core.Context.UsuarioContext.ValueObjects
{
    public class Validacao : ValueObject
    {
        public Validacao()
        {
            
        }
        public string Codigo { get; set; } = Guid.NewGuid().ToString("N")[0..6].ToUpper();
        public DateTime LimiteValidacao { get; set; } = DateTime.UtcNow.AddMinutes(15);
        public DateTime? ValidacaoRealizada { get; set; }
        public bool UsuarioValidado => ValidacaoRealizada != null;

        public void ValidarCodigoUsuario(string codigoUsuario)
        {
            if (UsuarioValidado)
                AddNotification("Codigo Verificação", "Este código já foi validado");

            if (LimiteValidacao < DateTime.UtcNow)
                AddNotification("Codigo Verificação", "Este código não é mais válido");

            if (!string.Equals(codigoUsuario.Trim(), Codigo.Trim(), StringComparison.CurrentCultureIgnoreCase))
                AddNotification("Código Verificação", "Código inválido");

            ValidacaoRealizada = DateTime.UtcNow;
        }

    }
}
