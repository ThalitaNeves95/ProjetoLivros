using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using ProjetoLivros.Models;

namespace ProjetoLivros.Context
{
    public class ProjetoLivrosContext : DbContext
    {
        // Cada Tabela -> DbSet - Tabela do Banco de Dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TiposUsuarios { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Categoria> Caegorias { get; set; }

        public ProjetoLivrosContext(DbContextOptions<ProjetoLivrosContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // String de Conexão
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-EUMC23F\\SQLEXPRESS;Initial Catalog=ECommerce;User Id=sa;Password=Senai@134;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(
                // Representacao da tabela
                entity =>
                {
                    // Primary Key
                    entity.HasKey(u => u.UsuarioId);

                    entity.Property(u => u.NomeCompleto)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                    entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                    // Email unico
                    entity.HasIndex(u => u.Email)
                    .IsUnique();
                    
                    entity.Property(u => u.Senha)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
                    
                    entity.Property(u => u.Telefone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                    entity.Property(u => u.DataCadastro)
                    .IsRequired();
                    
                    entity.Property(u => u.DataAtualizacao)
                    .IsRequired();
                    // HasOne = Usuário tem um tipo de usuáorio
                    // WithMany = Tipo Usuário tem varios usuarios
                    entity.HasOne(u => u.TipoUsuario)
                    .WithMany(t => t.Usuarios)
                    .HasForeignKey(u => u.TipoUsuario)
                    .OnDelete(DeleteBehavior.Cascade);
                }          
            );
        }
    }
}
