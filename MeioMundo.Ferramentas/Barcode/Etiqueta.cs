using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeioMundo.Ferramentas.Internal;

namespace MeioMundo.Ferramentas.Barcode
{
    public class Etiqueta : ViewModelBase
    {
        public virtual IBarCode BarCode { get => m_BarCode; set { m_BarCode = value; OnPropertyChanged(); } }
        public virtual Type IEtiquetaType { get; }
        public virtual string Produto { get => m_Produto; set { m_Produto = value; OnPropertyChanged(); } }
        public virtual string CodigoBarras { get => m_CodigoBarras; set { m_CodigoBarras = value; OnPropertyChanged(); } }
        public virtual float Preco { get => m_Preco; set { m_Preco = value; } }
        public virtual float Taxa { get => m_Taxa; set { m_Taxa = value; OnPropertyChanged(); } }
        public virtual string SKU { get => m_sku; set { m_sku = value; OnPropertyChanged(); } }
        public virtual bool MostrarPreco { get => m_MostrarPreco; set { m_MostrarPreco = value; OnPropertyChanged(); } }


        private IBarCode m_BarCode;
        private string m_Produto;
        private string m_CodigoBarras;
        private string m_sku;
        private float m_Preco;
        private float m_Taxa;
        private bool m_MostrarPreco;
        private float m_Imposto;
    }
}
