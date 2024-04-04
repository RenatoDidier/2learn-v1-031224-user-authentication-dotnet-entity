using Flunt.Notifications;
using Flunt.Validations;

namespace Projeto.Core.Context.CompartilhadoContext.UseCases.Contratos
{
    public interface IValidador<T> where T : class
    {
        public Contract<Notification> GarantirRequisicao(T requisicao);
    }
}
