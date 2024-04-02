namespace Projeto.Core
{
    public class ConfiguracoesGlobal
    {
        public static ConfiguracaoSegredosSenha SegredosSenha { get; set; } = new();
        public static ConfiguracaoBanco Banco { get; set; } = new();
        public class ConfiguracaoSegredosSenha
        {
            public string ChaveSalt { get; set; } = string.Empty;
            public string JwtChave { get; set; } = string.Empty;
        }

        public class ConfiguracaoBanco
        {
            public string StringConexao { get; set; } = string.Empty;
        }
    }
}
