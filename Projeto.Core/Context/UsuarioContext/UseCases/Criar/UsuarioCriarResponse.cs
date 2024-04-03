
using Flunt.Notifications;
using Projeto.Core.Context.CompartilhadoContext.UseCases;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Criar
{
    public class UsuarioCriarResponse : ResponsePadrao
    {
        public UsuarioCriarResponse(string mensagem, RespostaCriarUsuarioDto? respostaDto = null)
        {
            Mensagem = mensagem;
            Status = 201;
            Notificacoes = null;
            RespostaDto = respostaDto;
        }
        public UsuarioCriarResponse(
            int status, 
            string? mensagem = null, 
            IEnumerable<Notification>? notificacoes = null)
        {
            Status = status;
            Mensagem = mensagem;
            Notificacoes = notificacoes;
        }
        public RespostaCriarUsuarioDto? RespostaDto { get; set; }
    }

    public record RespostaCriarUsuarioDto (
            string Nome,
            string Email
        );
}
