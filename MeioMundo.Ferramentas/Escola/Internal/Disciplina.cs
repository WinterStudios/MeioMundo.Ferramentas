using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public class Disciplina
    {
        public int ID { get; set; }
        public long Livro_ISBN { get; set; }

        public string Nome { get => Disciplinas.__Disciplinas[ID]; }
    }
}
