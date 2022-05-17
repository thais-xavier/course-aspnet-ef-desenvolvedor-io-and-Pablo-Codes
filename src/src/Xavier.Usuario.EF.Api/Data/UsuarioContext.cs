using Microsoft.EntityFrameworkCore;
using Xavier.Usuario.EF.Api.Model;

namespace Xavier.Usuario.EF.Api.Data
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {
        }

        public DbSet<Modelo> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dadosTabela = modelBuilder.Entity<Modelo>();
            dadosTabela.ToTable("tb_usuario");
            dadosTabela.HasKey(x => x.Id);
            dadosTabela.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            dadosTabela.Property(x => x.Nome).HasColumnName("nome").IsRequired();
            dadosTabela.Property(x => x.DataNascimento).HasColumnName("data_nascimento");
        }
    }
}