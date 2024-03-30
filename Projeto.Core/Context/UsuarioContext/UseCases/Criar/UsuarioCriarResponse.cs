
using Flunt.Notifications;
using Projeto.Core.Context.CompartilhadoContext.UseCases;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Criar
{
    public class UsuarioCriarResponse : ResponsePadrao
    {
        public UsuarioCriarResponse(string mensagem)
        {
            Mensagem = mensagem;
            Status = 201;
            Notificacoes = null;
        }
        public UsuarioCriarResponse(int status, IEnumerable<Notification>? notificacoes = null, string? mensagem = null)
        {
            Status = status;
            Mensagem = mensagem;
            Notificacoes = notificacoes;
        }
    }
}
