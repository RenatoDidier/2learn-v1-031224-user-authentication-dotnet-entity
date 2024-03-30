using Flunt.Notifications;

namespace Projeto.Core.Context.CompartilhadoContext.UseCases
{
    public class ResponsePadrao
    {
        public string? Mensagem { get; set; }
        public int Status { get; set; } = 400;
        public bool HaErro => Status is < 200 or >= 300;
        public IEnumerable<Notification>? Notificacoes { get; set; }
    }
}
