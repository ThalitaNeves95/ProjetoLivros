using ProjetoLivros.Models;

namespace ProjetoLivros.Interfaces
{
    public interface ICategoriaRepository
    {
        List<Categoria> ListarTodos();

        Categoria? BuscarPorId(int id);

        void Cadastrar(Categoria categoria);

        // Update
        void Atualizar(int id, Categoria categoria);

        // Delete
        void Deletar(int id);
    }
}
