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
        public IEnumerable<Credencial> Credenciais { get; set; } = null!;
        public void AdicionarCredenciais(List<CredencialEnum> credenciais)
        {
            var listaCredenciais = new List<Credencial>();

            for (int i = 0; i < credenciais.Count; i++)
            {
                if (!System.Enum.IsDefined(typeof(CredencialEnum), credenciais[i]))
                    AddNotification("Credencial", "Foi fornecido uma credencial inválida");

                Credencial credencialAdicionado = new(credenciais[i]);
                listaCredenciais.Add(credencialAdicionado);
            }

            Credenciais = listaCredenciais;
        }
    }
}
