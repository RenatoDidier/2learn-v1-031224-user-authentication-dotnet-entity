using Projeto.Core.Context.CompartilhadoContext.ValueObjects;
using Projeto.Core.Context.CompartilhadoContext.ValueObjects.Contracts;
using System.Security.Cryptography;

namespace Projeto.Core.Context.UsuarioContext.ValueObjects
{
    public class Senha : ValueObject
    {
        #region Definindo variáveis
        private const short TamanhoSalt = 16;
        private const short TamanhoChave = 32;
        private const int Interacoes = 10000;
        private const char CaracterSeparacao = '.';
        #endregion

        #region Constructor
        public Senha()
        {
            
        }
        public Senha(string? senhaUsuario = null)
        {
            if (string.IsNullOrWhiteSpace(senhaUsuario))
                AddNotification("Senha", "Senha inválida");

            else if (!string.IsNullOrEmpty(senhaUsuario) && (senhaUsuario.Length < 8 || senhaUsuario.Length > 25))
                AddNotification("Senha", "A senha deve conter entre 8 a 20 caracteres");

            else
                HashSenha = GerarHash(senhaUsuario);
        }
        #endregion

        #region Propriedades
        public string HashSenha { get; set; } = string.Empty;
        #endregion

        #region 01. Função - Criar Hash
        protected static string GerarHash(
                string senhaUsuario,
                short tamanhoSalt = TamanhoSalt,
                short tamanhoChave = TamanhoChave,
                int interacoes = Interacoes,
                char caracterSeparacao = CaracterSeparacao
            )
        {
            senhaUsuario += ConfiguracoesGlobal.SegredosSenha.ChaveSalt;

            using var algoritmo = new Rfc2898DeriveBytes(
                    senhaUsuario,
                    tamanhoSalt,
                    interacoes,
                    HashAlgorithmName.SHA256
                );

            var chave = Convert.ToBase64String(algoritmo.GetBytes(tamanhoChave));

            var salt = Convert.ToBase64String(algoritmo.Salt);

            return $"{interacoes}{caracterSeparacao}{salt}{caracterSeparacao}{chave}";
        }
        #endregion

        #region 02. Função - Validar Hash
        public static bool ValidarHash(
                string senha,
                string hash,
                short tamanhoChave = TamanhoChave,
                int interacoes = Interacoes,
                char caracterSeparacao = CaracterSeparacao
            )
        {
            senha += ConfiguracoesGlobal.SegredosSenha.ChaveSalt;

            var partesHash = hash.Split(caracterSeparacao, 3);

            if (partesHash.Length != 3)
                return false;

            var partesHashInteracao = Convert.ToInt32(partesHash[0]);

            if (partesHashInteracao != interacoes)
                return false;

            var partesHashSalt = Convert.FromBase64String(partesHash[1]);
            var partesHashChave = Convert.FromBase64String(partesHash[2]);

            using var algoritmo = new Rfc2898DeriveBytes(
                    senha,
                    partesHashSalt,
                    interacoes,
                    HashAlgorithmName.SHA256
                );

            var validadorDeChave = algoritmo.GetBytes(tamanhoChave);

            return validadorDeChave.SequenceEqual(partesHashChave);
        }

        #endregion

    }
}
