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

namespace MeioMundo.Ferramentas.Voucher.Modelo
{
    /// <summary>
    /// Interaction logic for VoucherModelo_Back.xaml
    /// </summary>
    public partial class VoucherModelo_Back : UserControl
    {
        public VoucherModelo_Back()
        {
            InitializeComponent();
        }
        public VoucherModelo_Back(int value, int serialNumber)
        {
            InitializeComponent();
            UC_Image_GiftCode.Source = Barcode.Barcode.CreateBarcodeToImage(string.Format("MM-GIFTCARD-{0}", value), Barcode.BarcodeEncoding.Code39);
            UC_Image_GiftCode_Cliente.Source = Barcode.Barcode.CreateBarcodeToImage(string.Format("GC-{0}", serialNumber.ToString("000")), Barcode.BarcodeEncoding.Code39);
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
