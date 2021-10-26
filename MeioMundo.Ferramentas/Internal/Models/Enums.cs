using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Internal.Models
{
    public enum FileType
    {
        Excel,
        Text,
        CSV,
        Json
    }
    public enum SourceLocation
    {
        Sage,
        WebSite
    }
    public enum TaxaImposto
    {
        Taxa_Reduzida = 0,
        Taxa_Intermedia = 1,
        Taxa_Normal = 2,
        Taxa_Isento = 3,

        Taxa_Desconhecida = 10
    }
}
