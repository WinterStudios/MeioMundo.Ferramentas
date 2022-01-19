using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MeioMundo.Ferramentas.Internal;

namespace MeioMundo.Ferramentas.ViewModels
{
    public class Produto : ViewModelBase
    {
        public string Ref { get => m_ref; set { m_ref = value; OnPropertyChanged(); } }
        public string Nome { get => m_nome; set { m_nome = value; OnPropertyChanged(); } }
        public float Preco_cIVA { get => m_preco_cIVA; set { m_preco_cIVA = value; OnPropertyChanged(); } }
        public float Preco_sIVA { get => m_preco_sIVA; set { m_preco_sIVA = value; UpdatePrecoCIva(value); OnPropertyChanged(); } }
        public int Stock { get => m_stock; set { m_stock = value; OnPropertyChanged(); } }
        public TipoImposto Imposto { get => m_imposto; set { m_imposto = value; OnPropertyChanged(); } }
        public DateTime CreationDate { get => m_creationDate; set { m_creationDate = value; OnPropertyChanged(); } }

        private string m_ref;
        private string m_nome;
        private float m_preco_cIVA;
        private float m_preco_sIVA;
        private int m_stock;
        private TipoImposto m_imposto;
        private DateTime m_creationDate;


        private void UpdatePrecoCIva(float value)
        {
            
        }
    }
}
