using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using ProjetoLivros.Models;

namespace ProjetoLivros.Context
{
    // Todo contexto tem que herdar de DbContext

    // ORM do C# - Cada linguagem tem o seu
    public class ProjetoLivrosContext : DbContext
    {
        // Cada Tabela -> DbSet - Tabela do Banco de Dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TiposUsuarios { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public ProjetoLivrosContext(DbContextOptions<ProjetoLivrosContext> options) : base(options)
        {
            
        }
        // Como ele se conecta com o banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // String de Conexão
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-EUMC23F\\SQLEXPRESS;Initial Catalog=ProjetoLivros;User Id=sa;Password=Senai@134;TrustServerCertificate=true;");
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
                    .HasForeignKey(u => u.TipoUsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
                });
            // Toda tabela começa assim:
            //  modelBuilder.Entity<TipoUsuario>(entity =>
            //{

            //});
            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                // Configuro a primary key
                // TODO campo UNIQUE é um índice
                entity.HasKey(t => t.TipoUsuarioId);

                // Campo a campo configurando 
                entity.Property(t => t.DescricaoTipo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

                // Descricao não pode se repetir 
                entity.HasIndex(t  => t.DescricaoTipo)
                .IsUnique();
            });

            modelBuilder.Entity<Livro>(entity =>
            {
                entity.HasKey(l => l.LivroId);

                entity.Property(l => l.Titulo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

                entity.Property(l => l.Autor)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(l => l.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);

                entity.Property(l => l.DataPublicacao)
                .IsRequired();

                entity.HasOne(l => l.Categoria)
                .WithMany(c => c.Livros)
                .HasForeignKey(l => l.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.CategoriaId);

                entity.Property(c => c.NomeCategoria)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Assinatura>(entity =>
            {
                entity.HasKey(a => a.AssinaturaId);

                entity.Property(a => a.DataInicio)
               .IsRequired();
                
                entity.Property(a => a.DataFim)
               .IsRequired();

                entity.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
