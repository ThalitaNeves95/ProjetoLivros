using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoLivros.Interfaces;

namespace ProjetoLivros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private InvalidCastException? _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        // IActionResult = Interface que vem do .net - Permite que um metodo retorne um status code
        public IActionResult ListarCategorias()
        {
            return Ok(_categoriaRepository.ListarCate());
        }
    }
}
