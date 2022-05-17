using Xavier.Usuario.EF.Api.Model;

namespace Xavier.Usuario.EF.Api.Repository
{
    public interface IUsuarioReposiory
    {
        Task<IEnumerable<Modelo>> BuscaUsuarios();
        Task<Modelo> BuscaUsuario(int id);
        void AdicionaUsuario(Modelo usuario);
        void AtualizaUsuario(Modelo usuario);
        void DeletaUsuario(Modelo usuario);

        Task<bool> SaveChangesAsync();
    }
}