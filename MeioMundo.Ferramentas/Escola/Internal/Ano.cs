using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public class Ano
    {
        public int ID { get; set; }
        public List<Disciplina> Disciplinas { get; set; }

        public override string ToString()
        {
            return Anos.__Anos.GetValueOrDefault(ID);
        }
    }
}
