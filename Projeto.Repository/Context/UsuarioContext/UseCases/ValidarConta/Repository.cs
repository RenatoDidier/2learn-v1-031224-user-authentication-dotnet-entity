using Microsoft.EntityFrameworkCore;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.ValidarConta.Contratos;
using Projeto.Repository.Data;

namespace Projeto.Repository.Context.UsuarioContext.UseCases.ValidarConta
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario?> BuscarUsuarioAsync(string email, CancellationToken cancellationToken)
        {
            Usuario? usuarioEncontrado = new();

            try
            {
                usuarioEncontrado = await _context.Usuarios
                                            .Where(x => x.Email.Endereco == email)
                                            .FirstOrDefaultAsync();

                return usuarioEncontrado;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return usuarioEncontrado;
            }
        }

        public async Task<bool> RenovarCodigoVerificacaoAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            try
            {
                usuario.Email.Validacao.RenovarCodigoValidacao();

                _context.Usuarios.Update(usuario);

                await _context.SaveChangesAsync(cancellationToken);

                return true;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public async Task<bool> ValidarContaUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            try
            {
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
