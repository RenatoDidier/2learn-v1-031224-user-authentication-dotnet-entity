using Flunt.Notifications;
using Projeto.Core.Context.CompartilhadoContext.UseCases;
using Projeto.Core.Context.UsuarioContext.Enum;

namespace Projeto.Core.Context.UsuarioContext.UseCases.Autenticar
{
    public class UsuarioAutenticarResponse : ResponsePadrao
    {
        public UsuarioAutenticarResponse(int status, string? mensagem = null, IEnumerable<Notification>? notificacoes = null)
        {
            Status = status;
            Mensagem = mensagem;
            Notificacoes = notificacoes;
            DadosRetorno = null;
        }

        public UsuarioAutenticarResponse(string mensagem, DadosRetornoUsuarioAutenticado? dadosRetorno = null)
        {
            Mensagem = mensagem;
            Status = 201;
            Notificacoes = null;
            DadosRetorno = dadosRetorno;
        }

        public DadosRetornoUsuarioAutenticado? DadosRetorno { get; set; }
    }
    public class DadosRetornoUsuarioAutenticado
    {
        public string Token { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<CredencialEnum> Credenciais { get; set; } = null!;
    };
}
