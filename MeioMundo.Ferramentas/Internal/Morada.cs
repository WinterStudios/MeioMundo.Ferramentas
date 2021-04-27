using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Internal
{
    public class Morada
    {
        public int ID { get; set; }
        public int FornecedorID { get; set; }
        public string Titular { get; set; }
        public string Rua { get; set; }
        public string Localidade { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
