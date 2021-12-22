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

        
        public Etiqueta Etiqueta { get => m_etiqueta; set { m_etiqueta = value; OnPropertyChanged(); } }
        private Etiqueta m_etiqueta;
        
        public Etiqueta_A()
        {
            //BarCode = new Code39();
            //BarCode.DisplayCodeType = DisplayCodeType.None;
            
            InitializeComponent();
            //MostrarPreco = true;
            //UC_TextBlock_Code.Text = BarCode.Code;
            //UC_Image_CodeBar.Source = Barcode.CreateBarcodeToImage(code, BarcodeEncoding.Code39, 300, false);
        }
        public Etiqueta_A(IEtiqueta etiqueta)
        {
            InitializeComponent();
            //this.Etiqueta = etiqueta;
            //Etiqueta = etiqueta;
            //BarCode = etiqueta.BarCode;
            //Produto = etiqueta.Produto;
            //CodigoBarras = etiqueta.CodigoBarras;
            //Preco = etiqueta.Preco;
            //SKU = etiqueta.SKU;
            //MostrarPreco = etiqueta.MostrarPreco;
            //Taxa = etiqueta.Taxa;
        }
    }
}
