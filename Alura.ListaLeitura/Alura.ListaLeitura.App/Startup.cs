using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Alura.ListaLeitura.App.Logica;
using Alura.ListaLeitura.App.Mvc;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);

            builder.MapRoute("{classe}/{metodo}", RoteamentoPadrao.TratamentoPadrao);

            var rotas = builder.Build();

            app.UseRouter(rotas);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
    }
}