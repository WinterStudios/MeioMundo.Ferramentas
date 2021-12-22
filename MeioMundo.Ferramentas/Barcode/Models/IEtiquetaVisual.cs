using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Barcode.Models
{
    public interface IEtiquetaVisual : IEtiqueta
    {
        public IBarCode BarCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Type IEtiquetaType => throw new NotImplementedException();

        public string Produto { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CodigoBarras { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Preco { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Taxa { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SKU { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool MostrarPreco { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
