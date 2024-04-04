using Flunt.Notifications;
using Projeto.Core.Context.CompartilhadoContext.UseCases;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Autenticar
{
    public class UsuarioAutenticarResponse : ResponsePadrao
    {
        public UsuarioAutenticarResponse(string mensagem)
        {
            Mensagem = mensagem;
            Status = 201;
            Notificacoes = null;
        }

        public UsuarioAutenticarResponse(int status, string? mensagem = null, IEnumerable<Notification> notificacoes = null)
        {
            Status = status;
            Mensagem = mensagem;
            Notificacoes = notificacoes;
        }
    }
}
