using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Internal
{
    public class Fornecedor
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public Morada[] Moradas { get; set; }
        public string Contribuinte { get; set; }
        public override string ToString()
        {
            return string.Format("ID: {0} - Nome: {1}",ID,Nome);
        }
    }
}
