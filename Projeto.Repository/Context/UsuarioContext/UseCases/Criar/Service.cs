using Projeto.Core;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.Criar.Contratos;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Projeto.Repository.Context.UsuarioContext.UseCases.Criar
{
    public class Service : IService
    {
        public async Task<bool> EnviarEmailUsuario(Usuario usuario, CancellationToken cancellationToken)
        {
            try
            {
                SendGridClient apiSendGrid = new(ConfiguracoesGlobal.DadosSendGrid.ApiSendGrid);
                EmailAddress remetente = new(ConfiguracoesGlobal.DadosSendGrid.EmailRemetente, ConfiguracoesGlobal.DadosSendGrid.NomeRemetente);
                const string titulo = "Código de verificação";

                EmailAddress destinatario = new(usuario.Email.ToString(), usuario.Nome.ToString());
                string conteudoEmail = $"O seu código de verificação é: {usuario.Email.Validacao.Codigo}";

                SendGridMessage pacoteEnvioEmail = MailHelper.CreateSingleEmail(remetente, destinatario, titulo, conteudoEmail, conteudoEmail);

                Response retorno = await apiSendGrid.SendEmailAsync(pacoteEnvioEmail, cancellationToken);

                return retorno.IsSuccessStatusCode;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
