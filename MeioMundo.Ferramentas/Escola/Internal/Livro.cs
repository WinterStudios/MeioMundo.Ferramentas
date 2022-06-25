using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public class Livro
    {
        public int ID { get; set; }
        public long ISBN { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }

        public int Ano { get; set; }
        public int Disciplina { get; set; }
    }
}
