using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Mvc
{
    public class RoteamentoPadrao
    {
        public static Task TratamentoPadrao(HttpContext context)
        {
            // rota padrão: /<Classe>Logica/Metodo
            // {classe}/{metodo}

            var classe = Convert.ToString(context.GetRouteValue("classe"));
            var nomeMetodo = Convert.ToString(context.GetRouteValue("metodo"));

            var nomeCompleto = $"Alura.ListaLeitura.App.Logica.{classe}Logica";

            var tipo = Type.GetType(nomeCompleto);

            if (tipo == null)
            {
                return NotFound(context);
            }

            var metodo = tipo.GetMethods().FirstOrDefault(m => m.Name == nomeMetodo);

            if (metodo == null)
            {
                return NotFound(context);
            }

            var requestDelegate = (RequestDelegate)Delegate.CreateDelegate(typeof(RequestDelegate), metodo);

            return requestDelegate.Invoke(context);
        }

        private static Task NotFound(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return context.Response.WriteAsync("Not found");
        }
    }
}
