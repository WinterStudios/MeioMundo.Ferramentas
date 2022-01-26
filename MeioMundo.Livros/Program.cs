using System;

namespace MeioMundo.Livros
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hi !");

            PortoEditora.GetBookCover("https://www.portoeditora.pt/produtos/ficha/violeta/25593303");
        }
    }
}



