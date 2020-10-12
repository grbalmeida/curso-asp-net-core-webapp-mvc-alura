using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Alura.ListaLeitura.App.Repositorio;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<LivroRepositorioCSV>();
        }
    }
}