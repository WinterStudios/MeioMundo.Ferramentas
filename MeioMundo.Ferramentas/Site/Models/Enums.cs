using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Site.Models
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
        TaxaReduzida = 0,
        TaxaIntermedia = 1,
        TaxaNormal = 2
    }
}
