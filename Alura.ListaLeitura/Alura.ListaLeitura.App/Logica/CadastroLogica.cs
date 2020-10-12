using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Alura.ListaLeitura.App.Repositorio;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.HTML;

namespace Alura.ListaLeitura.App.Logica
{
    public static class CadastroLogica
    {
        public static Task Incluir(HttpContext context)
        {
            var livro = new Livro
            {
                Titulo = context.Request.Form["titulo"],
                Autor = context.Request.Form["autor"]
            };

            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);

            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
        }

        public static Task ExibeFormulario(HttpContext context)
        {
            var html = HtmlUtils.CarregaArquivoHTML("formulario");
            return context.Response.WriteAsync(html);
        }

        public static Task NovoLivro(HttpContext context)
        {
            var livro = new Livro
            {
                Titulo = Convert.ToString(context.GetRouteValue("nome")),
                Autor = Convert.ToString(context.GetRouteValue("autor"))
            };

            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);

            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
        }
    }
}
