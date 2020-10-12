using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Alura.ListaLeitura.App.Logica;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);

            builder.MapRoute("Livros/ParaLer", LivrosLogica.LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLogica.LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLogica.LivrosLidos);
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivroParaLer);
            builder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogica.ExibeDetalhes);
            builder.MapRoute("Cadastro/NovoLivro", CadastroLogica.ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", CadastroLogica.ProcessaFormulario);

            var rotas = builder.Build();

            app.UseRouter(rotas);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
    }
}