using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Alura.ListaLeitura.App.Repositorio;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.HTML;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Logica
{
    public class CadastroController
    {
        public string Incluir(Livro livro)
        {
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);

            return "O livro foi adicionado com sucesso";
        }

        public IActionResult ExibeFormulario()
        {
            var html = new ViewResult
            {
                ViewName = "formulario.html"
            };

            return html;
        }
    }
}
