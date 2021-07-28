using MeioMundo.Ferramentas.Barcode.Internal;
using MeioMundo.Ferramentas.Barcode.Models;
using MeioMundo.Ferramentas.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        IEtiqueta EtiquetaPreview { get; set; }

        public ObservableCollection<IBarCode> Codes { get; set; }
        internal ObservableCollection<IEtiqueta> Etiquetas { get; set; }

        private EtiquetaA4 etiquetaPage { get; set; }
        

        public UC()
        {
            InitializeComponent();
            Codes = new ObservableCollection<IBarCode>();
            Codes.CollectionChanged += Codes_CollectionChanged;
            EtiquetaPreview = (IEtiqueta)Activator.CreateInstance(typeof(Etiqueta_A));
            EtiquetaPreview.BarCode = (IBarCode)Activator.CreateInstance(typeof(Code39));
            EtiquetaPreview.BarCode.BarcodeImageResolution = BarcodeImageResolution.Medium;
            EtiquetaPreview.BarCode.BarcodeHeight = BarcodeHeight.Normal;
            EtiquetaPreview.BarCode.DisplayCodeType = DisplayCodeType.Center;
            Image_BarCode.Source = EtiquetaPreview.BarCode.CodeImage;
            //Image_BarCode.Source = code.DrawChar('0', true);
            etiquetaPage = new EtiquetaA4();
            previewPage.Child = etiquetaPage;

        }

        private void Codes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {               
                
            }
        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            EtiquetaPreview.BarCode.Code = Code.Text;
            Image_BarCode.Source = EtiquetaPreview.BarCode.CodeImage;
            GetInfo();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tag = ((ComboBox)sender).Tag.ToString();

            if (!this.IsLoaded)
                return;

            if(tag == "Resolution")
                EtiquetaPreview.BarCode.BarcodeImageResolution = (BarcodeImageResolution)((ComboBox)sender).SelectedIndex;
            if (tag == "DisplayType")
                EtiquetaPreview.BarCode.DisplayCodeType = (DisplayCodeType)((ComboBox)sender).SelectedIndex;
            if (tag == "BarHeight")
                EtiquetaPreview.BarCode.BarcodeHeight = (BarcodeHeight)((ComboBox)sender).SelectedIndex;
            if (tag == "BarType")
            {
                switch (((ComboBox)sender).SelectedItem)    
                {
                    case BarType.Code39:
                        EtiquetaPreview.BarCode = CreateCodeDefault(BarType.Code39);
                        break;
                    case BarType.EAN13:
                        EtiquetaPreview.BarCode = CreateCodeDefault(BarType.EAN13);
                        break;
                    default:
                        break;
                }
            }    

            Image_BarCode.Source = EtiquetaPreview.BarCode.CodeImage;
            GetInfo();
        }

        private IBarCode CreateCodeDefault(BarType typeOfBar)
        {
            IBarCode bar = null;
            if(typeOfBar == BarType.Code39)
            {
                bar = (IBarCode)Activator.CreateInstance(typeof(Code39), 
                    new object[] {
                        Code.Text,
                        (BarcodeImageResolution)UC_ComboBox_BarResolution.SelectedIndex,
                        (BarcodeHeight)UC_ComboBox_BarHeight.SelectedIndex,
                        (DisplayCodeType)UC_ComboBox_BarDisplay.SelectedIndex
                });
            }
            if (typeOfBar == BarType.EAN13)
            {
                bar = (IBarCode)Activator.CreateInstance(typeof(EAN13),
                    new object[] {
                        Code.Text,
                        (BarcodeImageResolution)UC_ComboBox_BarResolution.SelectedIndex,
                        (BarcodeHeight)UC_ComboBox_BarHeight.SelectedIndex,
                        (DisplayCodeType)UC_ComboBox_BarDisplay.SelectedIndex
                });
            }
            return bar;
        }

        private void GetInfo()
        {
            if(EtiquetaPreview.BarCode.CodeImage != null)
                info_1.Text = string.Format("W:{0}px - H:{1}px", EtiquetaPreview.BarCode.CodeImage.PixelWidth, EtiquetaPreview.BarCode.CodeImage.PixelHeight);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();

            if (tag == "AddCode")
            {
                Codes.Add(EtiquetaPreview.BarCode);
            }
        }

        private void CodigoNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            EtiquetaPreview.BarCode.Nome = CodigoNome.Text;
        }
    }
}
