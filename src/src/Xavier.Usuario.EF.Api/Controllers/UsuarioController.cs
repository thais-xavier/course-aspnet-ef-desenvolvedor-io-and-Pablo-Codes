using Microsoft.AspNetCore.Mvc;
using Xavier.Usuario.EF.Api.Model;
using Xavier.Usuario.EF.Api.Repository;

namespace Xavier.Usuario.EF.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioReposiory _reposiory;

        public UsuarioController(IUsuarioReposiory reposiory)
        {
            _reposiory = reposiory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _reposiory.BuscaUsuarios();
            return usuarios.Any()
                    ? Ok(usuarios)
                    : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _reposiory.BuscaUsuario(id);
            return usuario != null
                    ? Ok(usuario)
                    : NotFound("Usuário não encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Modelo usuario)
        {
            _reposiory.AdicionaUsuario(usuario);
            return await _reposiory.SaveChangesAsync()
                    ? Ok("Usuario adicionado com sucesso")
                    : BadRequest("Erro ao salvar o usuário");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Modelo usuario)
        {
            var usuarioBanco = await _reposiory.BuscaUsuario(id);
            if (usuarioBanco == null) return NotFound("Usuário não encontrado");

            usuarioBanco.Nome = usuario.Nome ?? usuarioBanco.Nome;
            usuarioBanco.DataNascimento = usuario.DataNascimento != new DateTime()
                ? usuario.DataNascimento : usuarioBanco.DataNascimento;

            _reposiory.AtualizaUsuario(usuarioBanco);

            return await _reposiory.SaveChangesAsync()
                    ? Ok("Usuario atualizado com sucesso")
                    : BadRequest("Erro ao atualizar o usuário");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioBanco = await _reposiory.BuscaUsuario(id);
            if (usuarioBanco == null) return NotFound("Usuário não encontrado");

            _reposiory.DeletaUsuario(usuarioBanco);

            return await _reposiory.SaveChangesAsync()
                    ? Ok("Usuario deletado com sucesso")
                    : BadRequest("Erro ao deletar o usuário");
        }
    }
}