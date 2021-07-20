using MeioMundo.Ferramentas.Barcode;
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

namespace MeioMundo.Ferramentas.UI
{
    /// <summary>
    /// Interaction logic for Barcode.xaml
    /// </summary>
    public partial class Barcode : UserControl
    {
        public Barcode()
        {
            InitializeComponent();
        }

        private void UI_TB_Ref_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;
            if (UI_TB_Ref.Text.Length > 0)
            {
                //UI_Preview_Code.Source = MeioMundo.Ferramentas.Barcode.Barcode.CreateBarcodeToImage(UI_TB_Ref.Text, BarcodeEncoding.Code39);
            }
            else
                UI_Preview_Code.Source = null;
        }
    }
}
