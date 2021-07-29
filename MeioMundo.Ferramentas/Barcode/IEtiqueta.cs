using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Barcode
{
    public interface IEtiqueta
    {
        IBarCode BarCode { get; set; }
        Type IEtiquetaType { get; }
        string Produto { get; set; }
        string CodigoBarras { get; set; }
        float Preco { get; set; }
        string SKU { get; set; }
    }
}
