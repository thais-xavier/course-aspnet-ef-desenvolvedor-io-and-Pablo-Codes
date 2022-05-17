using Microsoft.EntityFrameworkCore;
using Xavier.Usuario.EF.Api.Data;
using Xavier.Usuario.EF.Api.Model;

namespace Xavier.Usuario.EF.Api.Repository
{
    public class UsuarioRepository : IUsuarioReposiory
    {
        private readonly UsuarioContext _context;

        public UsuarioRepository(UsuarioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Modelo>> BuscaUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Modelo> BuscaUsuario(int id)
        {
            return await _context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void AdicionaUsuario(Modelo usuario)
        {
            _context.Add(usuario);
        }

        public void AtualizaUsuario(Modelo usuario)
        {
            _context.Update(usuario);
        }        

        public void DeletaUsuario(Modelo usuario)
        {
            _context.Remove(usuario);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}