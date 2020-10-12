using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Alura.ListaLeitura.App.Repositorio;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.HTML;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosController
    {
        public string Detalhes(int id)
        {
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return "Livro não encontrado";
            }

            return livro.Detalhes();
        }

        public IActionResult ParaLer()
        {
            var _repo = new LivroRepositorioCSV();

            var html = new ViewResult
            {
                ViewName = "lista"
            };

            return html;
        }

        public static Task Lendo(HttpContext context)
        {
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("lendo");
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(ObterLivros(_repo.Lendo.Livros, conteudoArquivo));
        }

        public static Task Lidos(HttpContext context)
        {
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("lidos");
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(ObterLivros(_repo.Lidos.Livros, conteudoArquivo));
        }

        public string Teste()
        {
            return "Nova funcionalidade implementada!";
        }

        private static string ObterLivros(IEnumerable<Livro> listaLivros, string conteudoArquivo)
        {
            var livros = "";

            foreach (var livro in listaLivros)
            {
                livros += $"<li>{livro.Titulo} - {livro.Autor}</li>";
            }

            return conteudoArquivo.Replace("#NOVO-ITEM#", livros);
        }
    }
}
