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
    /// Interaction logic for VoucherModelo_Front.xaml
    /// </summary>
    public partial class VoucherModelo_Front : UserControl
    {
        public int GiftValue { get => _giftValue; set => _giftValue = value; }
        private int _giftValue;
        public VoucherModelo_Front()
        {
            InitializeComponent();
        }
        public VoucherModelo_Front(int value)
        {
            InitializeComponent();
            textValue.Text = value.ToString();
        }
    }
}
