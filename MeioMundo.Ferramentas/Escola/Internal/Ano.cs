using System.Collections.Generic;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public class Ano
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Disciplina> Disciplinas { get; set; }
    }
}