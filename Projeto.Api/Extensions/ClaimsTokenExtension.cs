using System.Security.Claims;

namespace Projeto.Api.Extensions
{
    public static class ClaimsTokenExtension
    {
        public static string Id(this ClaimsPrincipal usuario)
            => usuario.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? string.Empty;
        public static string NomeUsuario(this ClaimsPrincipal usuario)
            => usuario.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
        public static string Email(this ClaimsPrincipal usuario)
            => usuario.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
    }
}
