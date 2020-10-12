using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);

            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer);
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes);
            builder.MapRoute("Cadastro/NovoLivro", ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", ProcessaFormulario);

            var rotas = builder.Build();

            app.UseRouter(rotas);

            //app.Run(Roteamento);
        }

        public Task ProcessaFormulario(HttpContext context)
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

        public Task ExibeFormulario(HttpContext context)
        {
            var html = CarregaArquivoHTML("formulario");
            return context.Response.WriteAsync(html);
        }

        public string CarregaArquivoHTML(string nomeArquivo)
        {
            var nomeCompletoArquivo = $"HTML/{nomeArquivo}.html";

            using (var file = File.OpenText(nomeCompletoArquivo))
            {
                return file.ReadToEnd();
            }
        }

        public Task ExibeDetalhes(HttpContext context)
        {
            var id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                return context.Response.WriteAsync("Livro não encontrado");
            }

            return context.Response.WriteAsync(livro.Detalhes());
        }

        public Task NovoLivroParaLer(HttpContext context)
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public Task Roteamento(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            var caminhosAtendidos = new Dictionary<string, RequestDelegate>
            {
                { "/Livros/ParaLer", LivrosParaLer },
                { "/Livros/Lendo", LivrosLendo },
                { "/Livros/Lidos", LivrosLidos }
            };

            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                var metodo = caminhosAtendidos[context.Request.Path];
                return metodo.Invoke(context);
            }

            context.Response.StatusCode = StatusCodes.Status404NotFound;

            return context.Response.WriteAsync("Caminho inexistente.");
        }

        public Task LivrosParaLer(HttpContext context)
        {
            var conteudoArquivo = CarregaArquivoHTML("para-ler");
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(ObterLivros(_repo.ParaLer.Livros, conteudoArquivo));
        }

        public Task LivrosLendo(HttpContext context)
        {
            var conteudoArquivo = CarregaArquivoHTML("lendo");
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(ObterLivros(_repo.Lendo.Livros, conteudoArquivo));
        }

        public Task LivrosLidos(HttpContext context)
        {
            var conteudoArquivo = CarregaArquivoHTML("lidos");
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(ObterLivros(_repo.Lidos.Livros, conteudoArquivo));
        }

        private string ObterLivros(IEnumerable<Livro> listaLivros, string conteudoArquivo)
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