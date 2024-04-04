using Microsoft.IdentityModel.Tokens;
using Projeto.Core;
using Projeto.Core.Context.UsuarioContext.UseCases.Autenticar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projeto.Api.Extensions
{
    public static class JwtTokenExtension
    {
        public static string GerarTokenJwt(DadosRetornoUsuarioAutenticado data)
        {
            JwtSecurityTokenHandler handler = new();

            byte[] chave = Encoding.ASCII.GetBytes(ConfiguracoesGlobal.SegredosSenha.JwtChave);

            SigningCredentials credenciais = new(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
                );

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = GerarClaimsSubjectToken(data),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = credenciais
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        public static ClaimsIdentity GerarClaimsSubjectToken(DadosRetornoUsuarioAutenticado data)
        {
            ClaimsIdentity ci = new();

            ci.AddClaim(new Claim("Id", data.Id));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, data.Nome));
            ci.AddClaim(new Claim(ClaimTypes.Name, data.Email));

            foreach (var role in data.Credenciais)
                ci.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));


            return ci;
        }
    }
}
