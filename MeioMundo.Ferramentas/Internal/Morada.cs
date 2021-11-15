using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Internal
{
    public class Morada
    {
        public string Rua { get; set; }
        public string Localidade { get; set; }
        public string Concelho { get; set; }
        public string Distrito { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public TipoMorada TipoMorada { get; set; }
        public string _TipoMorada { get => TipoMorada.ToString().ToUpper(); }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Rua, Localidade, ZipCode, Country);
        }
    }
    public enum TipoMorada
    {
        Normal = 0,
        Devoluções = 1,
        Outra = 2,
        Envio = 3,
        Facturação = 4
    }
}
