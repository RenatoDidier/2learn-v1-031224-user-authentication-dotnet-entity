namespace Projeto.Core.Context.CompartilhadoContext.Extensions
{
    public static class StringExtension
    {
        public static string GerarSeisCaracteres()
            => Guid.NewGuid().ToString("N")[0..6].ToUpper();
    }
}
