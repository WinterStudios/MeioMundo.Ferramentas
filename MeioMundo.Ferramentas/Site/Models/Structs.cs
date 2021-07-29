using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Site.Models
{
    /// <summary>
    /// Representa uma estrutura com CxLxA em cm
    /// </summary>
    public struct Dimensoes
    {
        public float Comprimento { get; set; }
        public float Largura { get; set; }
        public float Altura { get; set; }
    }

    public struct TaxaIva
    {
        public TaxaImposto Tax { get; set; }
    }
}
