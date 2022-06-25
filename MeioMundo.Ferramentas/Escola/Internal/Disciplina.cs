using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MeioMundo.Ferramentas.Escola.Internal;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public class Disciplina
    {
        public int ID { get; set; }
        public int? Livro_ID { get; set; }
        public long Livro_ISBN { get; set; }
        public bool OP_Basica_3 { get; set; }
        public bool Disc_Espe { get; set; }
        public string OP_Nome { get; set; }
        public string Nome { get => Disciplinas.__Disciplinas[ID]; }
        public string FullNome { get => string.Format("{0} {1}", Nome, OP_Nome); }
    }
}
