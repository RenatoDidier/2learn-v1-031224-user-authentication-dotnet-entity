using Microsoft.EntityFrameworkCore;
using Projeto.Core.Context.UsuarioContext.Models;
using Projeto.Core.Context.UsuarioContext.UseCases.Autenticar.Contratos;
using Projeto.Repository.Data;

namespace Projeto.Repository.Context.UsuarioContext.UseCases.Autenticar
{
    public class Repository : IRepository
    {
        public readonly AppDbContext _context;
        public Repository(AppDbContext context)
            => _context = context;
        public async Task<Usuario?> BuscarUsuarioCompletoAsync(string email, CancellationToken cancellationToken)
        {
            Usuario? retornoUsuario = new();
            try
            {
                retornoUsuario = await _context
                                    .Usuarios
                                    .Include(u => u.Credenciais)
                                    .Where(u => u.Email.Endereco == email)
                                    .FirstOrDefaultAsync();

                return retornoUsuario;
            } catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return retornoUsuario;
            }
        }
    }
}
