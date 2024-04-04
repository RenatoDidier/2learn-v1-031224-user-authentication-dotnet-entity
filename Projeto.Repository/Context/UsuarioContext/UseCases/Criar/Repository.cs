using Microsoft.EntityFrameworkCore;
using Projeto.Core.Context.UsuarioContext.Enum;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.Criar.Contratos;
using Projeto.Repository.Data;

namespace Projeto.Repository.Context.UsuarioContext.UseCases.Criar
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Credencial?> BuscarCredencialAsync(CredencialEnum credencial, CancellationToken cancellationToken)
        {
            try
            {
                return await _context
                    .Credenciais
                    .Where(x => x.Titulo == credencial)
                    .FirstOrDefaultAsync();

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Credencial();
            }
        }

        public async Task<bool> EfetuarCadastroUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            try
            {

                await _context.Usuarios.AddAsync(usuario);
                _= await _context.SaveChangesAsync(cancellationToken);

                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> ValidarExistenciaUsuarioAsync(string email)
        {
            try
            {
                Usuario? usuario = await _context
                                            .Usuarios
                                            .AsNoTracking()
                                            .Where(x => x.Email.Endereco == email)
                                            .FirstOrDefaultAsync();

                return usuario != null;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
