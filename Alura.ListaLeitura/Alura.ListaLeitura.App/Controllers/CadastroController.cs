using Alura.ListaLeitura.App.Repositorio;
using Alura.ListaLeitura.App.Negocio;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Logica
{
    public class CadastroController : Controller
    {
        private readonly LivroRepositorioCSV _repositorio;

        public CadastroController(LivroRepositorioCSV repositorio)
        {
            _repositorio = repositorio;
        }

        public string Incluir(Livro livro)
        {
            _repositorio.Incluir(livro);

            return "O livro foi adicionado com sucesso";
        }

        public IActionResult ExibeFormulario()
        {
            return View("formulario");
        }
    }
}
