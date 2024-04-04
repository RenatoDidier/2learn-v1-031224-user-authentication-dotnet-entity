
using Flunt.Validations;
using Projeto.Core.Context.CompartilhadoContext.ValueObjects;
using Projeto.Core.Context.CompartilhadoContext.ValueObjects.Contracts;

namespace Projeto.Core.Context.UsuarioContext.ValueObjects
{
    public class Nome : ValueObject, IValueObject
    {
        public Nome()
        {
            
        }
        public Nome(string primeiroNome, string ultimoSobrenome)
        {
            PrimeiroNome = primeiroNome;
            UltimoSobrenome = ultimoSobrenome;
        }
        public string PrimeiroNome { get; set; } = string.Empty;
        public string UltimoSobrenome { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{PrimeiroNome} {UltimoSobrenome}";
        }

        public void ValidarPreenchimentoDados()
        {
            AddNotifications(
                    new Contract<Nome>()
                        .Requires()
                        .IsGreaterOrEqualsThan(PrimeiroNome, 4, "PrimeiroNome", "O nome precisa ter mais do que 4 caracteres")
                        .IsLowerOrEqualsThan(PrimeiroNome, 80, "PrimeiroNome", "O nome precisa ter menos do que 80 caracteres")
                        .IsGreaterOrEqualsThan(UltimoSobrenome, 4, "UltimoSobrenome", "O sobrenome precisa ter mais do que 4 caracteres")
                        .IsLowerOrEqualsThan(UltimoSobrenome, 80, "UltimoSobrenome", "O sobrenome precisa ter menos do que 80 caracteres")
                );
        }
    }
}
