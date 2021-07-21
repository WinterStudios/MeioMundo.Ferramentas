using MeioMundo.Ferramentas.Barcode.Internal;
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

namespace MeioMundo.Ferramentas.Barcode
{
    /// <summary>
    /// Interaction logic for UC.xaml
    /// </summary>
    public partial class UC : UserControl
    {

        IBarCode barcodePreview { get; set; }

        public UC()
        {
            InitializeComponent();
            barcodePreview = (IBarCode)Activator.CreateInstance(typeof(Code39));
            barcodePreview.BarcodeImageResolution = BarcodeImageResolution.Medium;
            barcodePreview.BarcodeHeight = BarcodeHeight.Normal;
            barcodePreview.DisplayCodeType = DisplayCodeType.Center;
            Image_BarCode.Source = barcodePreview.CodeImage;
            //Image_BarCode.Source = code.DrawChar('0', true);
            
        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            barcodePreview.Code = Code.Text;
            Image_BarCode.Source = barcodePreview.CodeImage;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tag = ((ComboBox)sender).Tag.ToString();

            if (!this.IsLoaded)
                return;

            if(tag == "Resolution")
                barcodePreview.BarcodeImageResolution = (BarcodeImageResolution)((ComboBox)sender).SelectedIndex;
            if (tag == "DisplayType")
                barcodePreview.DisplayCodeType = (DisplayCodeType)((ComboBox)sender).SelectedIndex;
            if (tag == "BarHeight")
                barcodePreview.BarcodeHeight = (BarcodeHeight)((ComboBox)sender).SelectedIndex;
            if(tag == "BarType")
            {
                switch (((ComboBox)sender).SelectedItem)    
                {
                    case "":
                        break;
                    default:
                        break;
                }
            }    

            Image_BarCode.Source = barcodePreview.CodeImage;
        }


    }
}
