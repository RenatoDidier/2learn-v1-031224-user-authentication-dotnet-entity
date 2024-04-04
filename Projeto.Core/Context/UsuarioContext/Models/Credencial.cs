using Projeto.Core.Context.UsuarioContext.Enum;

namespace Projeto.Core.Context.UsuarioContext.Models
{
    public class Credencial
    {
        public Credencial()
        {
            
        }
        public Credencial(CredencialEnum titulo)
        {
            Titulo = titulo;
        }
        public int Id { get; set; }
        public CredencialEnum Titulo { get; set; }

        public IEnumerable<Usuario>? Usuarios { get; set; }
    }
}
