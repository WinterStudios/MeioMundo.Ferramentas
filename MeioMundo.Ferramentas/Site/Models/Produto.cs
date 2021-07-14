using MeioMundo.Ferramentas.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U_System.Core.UX.Preferences;

namespace MeioMundo.Ferramentas.Site.Models
{
    public class Produto : ViewModelBase
    {
        private string _ref;
        private string _nome;
        private string _textoBreve;
        private string _textoDescricao;

        private bool _stockIsManager;

        private float _preco_cIVA;
        private float _preco_sIVA;
        private float _taxaIVA;
        private float _weight;

        private int _stockSite;
        private int _stockSage;


        #region Proprietes >>>> String

        public string REF
        {
            get { return _ref; }
            set { _ref = value; OnPropertyChanged(); }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnPropertyChanged(); }
        }
        public string TextoBreve
        {
            get { return _textoBreve; }
            set { _textoBreve = value; OnPropertyChanged(); }
        }
        public string TextoDescricao
        {
            get { return _textoDescricao; }
            set { _textoDescricao = value; OnPropertyChanged(); }
        }


        #endregion

        #region Proprietes >>>> BOOL

        /// <summary>
        /// Por controlo do stock com o stock no SAGE
        /// </summary>
        public bool StockIsManager 
        { 
            get { return _stockIsManager; }
            set { _stockIsManager = value; OnPropertyChanged(); }
        }

        #endregion

        #region Proprietes >>>> Float

        /// <summary>
        /// Preço com o IVA Aplicado
        /// </summary>
        public float Preco_cIVA
        {
            get { return _preco_cIVA; }
            set { _preco_cIVA = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Preço sem o IVA Aplicado
        /// </summary>
        public float Preco_sIVA
        {
            get { return _preco_sIVA; }
            set { _preco_sIVA = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Percentagem do IVA
        /// </summary>
        /// <remarks>Values in percentagem [###.##%]</remarks>
        public float TaxaIVA
        {
            get { return _taxaIVA; }
            set { _taxaIVA = value; OnPropertyChanged(); }
        }
        public float Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChanged(); }
        }
        #endregion


        #region Proprietes >>>> Int

        /// <summary>
        /// O stock que esta presente na loja
        /// </summary>
        public int StockSage
        {
            get { return _stockSage; }
            set { _stockSage = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Stock do site online
        /// </summary>
        public int StockSite
        {
            get { return _stockSite; }
            set { _stockSite = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Stock Fisico que existe
        /// </summary>
        public int StockFinal { get => StockSage; }
        /// <summary>
        /// A diferneça do stock da loja online com o da loja fisica
        /// </summary>
        public int StockDiference { get => StockSite - StockSage; }
        #endregion


        public Dimensoes Dimensoes { get; set; }



        public string[] Images { get; set; }

        public string ToStringCSV()
        {
            // REF, Nome, Stock, IVA, Preço
            return string.Format("{0},{1},{2},{3},{4}",
                REF,Nome,StockFinal,0,Preco_cIVA.ToString().Replace(',','.')
                );
        }
    }
}
