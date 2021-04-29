using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Internal
{
    public class Morada
    {
        public int FornecedorID { get; set; }
        public string Titular { get; set; }
        public string Rua { get; set; }
        public string Localidade { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }


        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Rua, Localidade, ZipCode, Country);
        }
    }
}
