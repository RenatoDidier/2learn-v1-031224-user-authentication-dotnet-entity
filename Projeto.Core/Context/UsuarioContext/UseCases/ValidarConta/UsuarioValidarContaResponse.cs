using Flunt.Notifications;
using Projeto.Core.Context.CompartilhadoContext.UseCases;

namespace Projeto.Core.Context.UsuarioContext.UseCases.ValidarConta
{
    public class UsuarioValidarContaResponse : ResponsePadrao
    {
        public UsuarioValidarContaResponse(string mensagem)
        {
            Mensagem = mensagem;
            Status = 201;
            Notificacoes = null;
        }

        public UsuarioValidarContaResponse(int status, string? mensagem = null, IEnumerable<Notification>? notificacoes = null)
        {
            Status = status;
            Mensagem = mensagem;
            Notificacoes = notificacoes;
        }
    }
}
