using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Livros
{
    internal class PortoEditora
    {
        public static string URL { get => "https://www.portoeditora.pt/"; }


        public static void GetBookCover(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("//div[@class='store-product-image-container']//span//img");
            var img = nodes.First().Attributes.First();
            string imgSource = $"{img.Value.Remove(img.Value.LastIndexOf('/'))}/1600x";
        }

        public static async Task GetBookCoverAsync(string url)
        {
            
        }
    }
}
