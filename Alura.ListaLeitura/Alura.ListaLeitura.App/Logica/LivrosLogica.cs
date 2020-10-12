﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Alura.ListaLeitura.App.Repositorio;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.HTML;

namespace Alura.ListaLeitura.App.Logica
{
    public static class LivrosLogica
    {
        public static Task ExibeDetalhes(HttpContext context)
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

        public static Task LivrosParaLer(HttpContext context)
        {
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("para-ler");
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(ObterLivros(_repo.ParaLer.Livros, conteudoArquivo));
        }

        public static Task LivrosLendo(HttpContext context)
        {
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("lendo");
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(ObterLivros(_repo.Lendo.Livros, conteudoArquivo));
        }

        public static Task LivrosLidos(HttpContext context)
        {
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("lidos");
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(ObterLivros(_repo.Lidos.Livros, conteudoArquivo));
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
