using Flunt.Notifications;
using Projeto.Core.Context.UsuarioContext.Enum;
using Projeto.Core.Context.UsuarioContext.ValueObjects;

namespace Projeto.Core.Context.UsuarioContext.Models
{
    public class Usuario : Notifiable<Notification>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[0..8].ToUpper();
        public Nome Nome { get; set; } = null!;
        public Email Email { get; set; } = null!;
        public Senha Senha { get; set; } = null!;
        public List<Credencial> Credenciais { get; set; } = null!;
    }
}
