using System.Linq;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosController : Controller
    {
        private readonly LivroRepositorioCSV _repositorio;

        public LivrosController(LivroRepositorioCSV repositorio)
        {
            _repositorio = repositorio;
        }

        public string Detalhes(int id)
        {
            var livro = _repositorio.Todos.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return "Livro não encontrado";
            }

            return livro.Detalhes();
        }

        public IActionResult ParaLer()
        {
            ViewBag.Livros = _repositorio.ParaLer.Livros;
            return View("lista");
        }

        public IActionResult Lendo()
        {
            ViewBag.Livros = _repositorio.Lendo.Livros;
            return View("lista");
        }

        public IActionResult Lidos()
        {
            ViewBag.Livros = _repositorio.Lidos.Livros;
            return View("lista");
        }

        public string Teste()
        {
            return "Nova funcionalidade implementada!";
        }
    }
}
