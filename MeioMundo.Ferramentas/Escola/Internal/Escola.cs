using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public class Escola
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public Ano[] Anos { get; set; }
    }
}
