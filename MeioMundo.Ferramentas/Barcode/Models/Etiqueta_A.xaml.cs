using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class Etiqueta_A : UserControl
    {
        public string code { get; set; }
        public Etiqueta_A()
        {
            InitializeComponent();
            code = "1234567890123";
            UC_TextBlock_Code.Text = code;
            //UC_Image_CodeBar.Source = Barcode.CreateBarcodeToImage(code, BarcodeEncoding.Code39, 300, false);
    }
    }
}
