using System.IO;

namespace Alura.ListaLeitura.App.HTML
{
    public static class HtmlUtils
    {
        public static string CarregaArquivoHTML(string nomeArquivo)
        {
            var nomeCompletoArquivo = $"HTML/{nomeArquivo}.html";

            using (var file = File.OpenText(nomeCompletoArquivo))
            {
                return file.ReadToEnd();
            }
        }
    }
}
