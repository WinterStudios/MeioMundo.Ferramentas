using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Site.Models
{
    public class Produto
    {
        public string REF { get; set; }
        public string Nome { get; set; }
        public int StockSage { get; set; }
        public int StockSite { get; set; }
        public int StockFinal { get => StockSage; }
        public int StockDiference { get => StockSite - StockSage; }
        public float Preco_C_IVA { get; set; }
        public float Preco_S_IVA { 
            get {
                if (_iva == TaxaImposto.TaxaNormal)
                    return (float)(Preco_C_IVA / 1.23);
                if (_iva == TaxaImposto.TaxaIntermedia)
                    return (float)(Preco_C_IVA / 1.13);
                if (_iva == TaxaImposto.TaxaReduzida)
                    return (float)(Preco_C_IVA / 1.06);

                return 0;
            } }
        public string IVA { 
            get {
                switch (_iva)
                {
                    case TaxaImposto.TaxaReduzida:
                        return "6 %";
                    case TaxaImposto.TaxaIntermedia:
                        return "13 %";
                    case TaxaImposto.TaxaNormal:
                        return "23 %";
                    default:
                        return "0 %";
                }
            }
            set {
                if (value == "IVA Taxa Normal")
                    _iva = TaxaImposto.TaxaNormal;
                if (value == "IVA Taxa Intermedia")
                    _iva = TaxaImposto.TaxaIntermedia;
                if (value == "IVA Taxa Reduzida")
                    _iva = TaxaImposto.TaxaReduzida;
            }}
        private TaxaImposto _iva;



        public string ToStringCSV()
        {
            // REF, Nome, Stock, IVA, Preço
            return string.Format("{0},{1},{2},{3},{4}",
                REF,Nome,StockFinal,IVA,Preco_C_IVA.ToString().Replace(',','.')
                );
        }
    }
}
