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

namespace MeioMundo.Ferramentas.Voucher
{
    /// <summary>
    /// Interaction logic for VoucherEmition.xaml
    /// </summary>
    public partial class VoucherEmition : UserControl
    {
        public VoucherEmition()
        {
            InitializeComponent();
            UC_DataGrid_Vouchers.ItemsSource = VoucherSystem.Vouchers;
            UC_DataGrid_Vouchers.RowEditEnding += UC_DataGrid_Vouchers_RowEditEnding;
        }

        private void UC_DataGrid_Vouchers_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            VoucherSystem.Save();
        }
    }
}
