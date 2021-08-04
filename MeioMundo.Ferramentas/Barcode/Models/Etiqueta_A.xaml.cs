using MeioMundo.Ferramentas.Barcode.Internal;
using MeioMundo.Ferramentas.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeioMundo.Ferramentas.Barcode.Models
{
    /// <summary>
    /// Interaction logic for Etiqueta_A.xaml
    /// </summary>
    public partial class Etiqueta_A : UserControl, IEtiqueta, INotifyPropertyChanged
    {
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public IBarCode BarCode
        {
            get { return m_BarCode; }
            set { m_BarCode = value; OnPropertyChanged(); }
        }
        public string Produto
        {
            get { return m_Produto; }
            set { m_Produto = value; OnPropertyChanged(); }
        }
        public string CodigoBarras
        {
            get { return m_CodigoBarras; }
            set { m_CodigoBarras = value; BarCode.Code = value; BarCode.Draw(); OnPropertyChanged(); }
        }
        public float Preco
        {
            get { return m_Preco; }
            set { m_Preco = value; OnPropertyChanged(); }
        }
        public string SKU { get; set; }

        public bool MostrarPreco
        {
            get { return m_MostrarPreco; }
            set { m_MostrarPreco = value; if (value) preco.Visibility = Visibility.Visible; else preco.Visibility = Visibility.Collapsed; OnPropertyChanged(); }
        }

        public Type IEtiquetaType => this.GetType();

        private IBarCode m_BarCode;
        private string m_Produto;
        private string m_CodigoBarras;
        private float m_Preco;
        private bool m_MostrarPreco;
        public Etiqueta_A()
        {
            BarCode = new Code39();
            BarCode.DisplayCodeType = DisplayCodeType.None;
            InitializeComponent();
            //UC_TextBlock_Code.Text = BarCode.Code;
            //UC_Image_CodeBar.Source = Barcode.CreateBarcodeToImage(code, BarcodeEncoding.Code39, 300, false);
        }
        public Etiqueta_A(IEtiqueta etiqueta)
        {
            InitializeComponent();
            BarCode = etiqueta.BarCode;
            Produto = etiqueta.Produto;
            CodigoBarras = etiqueta.CodigoBarras;
            Preco = etiqueta.Preco;
            SKU = etiqueta.SKU;
            MostrarPreco = etiqueta.MostrarPreco;
        }
    }
}
