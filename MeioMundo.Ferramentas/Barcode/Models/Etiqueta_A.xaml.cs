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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        public string code { get; set; }
        public IBarCode BarCode 
        {
            get { return m_BarCode; }
            set { m_BarCode = value; OnPropertyChanged(); }
        }
        public string Produto { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CodigoBarras { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Preco { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SKU { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private IBarCode m_BarCode;

        public Etiqueta_A()
        {
            InitializeComponent();
            code = "1234567890123";
            UC_TextBlock_Code.Text = code;
            //UC_Image_CodeBar.Source = Barcode.CreateBarcodeToImage(code, BarcodeEncoding.Code39, 300, false);
    }
    }
}
