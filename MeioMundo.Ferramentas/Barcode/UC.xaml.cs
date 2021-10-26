using MeioMundo.Ferramentas.Barcode.Internal;
using MeioMundo.Ferramentas.Barcode.Models;
using MeioMundo.Ferramentas.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace MeioMundo.Ferramentas.Barcode
{
    /// <summary>
    /// Interaction logic for UC.xaml
    /// </summary>
    public partial class UC : UserControl, INotifyPropertyChanged
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

        public ObservableCollection<IEtiqueta> Etiquetas { get; set; }

        public EtiquetaA4 etiquetaPage { get => m_EtiquetaA4; set { m_EtiquetaA4 = value; OnPropertyChanged(); } }
        private EtiquetaA4 m_EtiquetaA4;

        public IEtiqueta PreviewEtiqueta
        {
            get { return m_PreviewEtiqueta; }
            set { m_PreviewEtiqueta = value; OnPropertyChanged(); }
        }

        private IEtiqueta m_PreviewEtiqueta;


        public UC()
        {
            InitializeComponent();
            PreviewEtiqueta = CreateNewEtiqueta();
            UC_VeiwBox_PreviewEtiqueta.Child = (UserControl)PreviewEtiqueta;
            //UC_ComboBox_TipoEtiquetas.ItemsSource = GetIEtiquetas();
            Etiquetas = new ObservableCollection<IEtiqueta>();
            //Etiquetas.CollectionChanged += Etiquetas_CollectionChanged;
            //EtiquetaPreview = CreateNewEtiqueta();
            ////Image_BarCode.Source = code.DrawChar('0', true);
            etiquetaPage = new EtiquetaA4();
            previewPage.Child = etiquetaPage;
            //etiquetaPage.ListView_Etiquetas.ItemsSource = Etiquetas;
            //etiquetaPage.Etiquetas = Etiquetas;


        }

        private IEtiqueta CreateNewEtiqueta()
        {
            IEtiqueta t_IEtiquetaPreview = (IEtiqueta)Activator.CreateInstance(typeof(Etiqueta_A));

            t_IEtiquetaPreview.Preco = 0;
            t_IEtiquetaPreview.Produto = string.Empty;
            t_IEtiquetaPreview.SKU = string.Empty;
            t_IEtiquetaPreview.BarCode = (IBarCode)Activator.CreateInstance(typeof(Code39));
            t_IEtiquetaPreview.BarCode.BarcodeImageResolution = BarcodeImageResolution.Medium;
            t_IEtiquetaPreview.BarCode.BarcodeHeight = BarcodeHeight.Normal;
            t_IEtiquetaPreview.BarCode.DisplayCodeType = DisplayCodeType.Center;
            return t_IEtiquetaPreview;

        }

        public static IEnumerable<Type> GetIEtiquetas()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where((typeof(IEtiqueta)).IsAssignableFrom).Where(x => !x.IsInterface).ToList();
        }

        private void Etiquetas_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                
            }
            

        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            PreviewEtiqueta.BarCode.Code = Code.Text;
            //Image_BarCode.Source = EtiquetaPreview.BarCode.CodeImage;
            GetInfo();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tag = ((ComboBox)sender).Tag.ToString();

            if (!this.IsLoaded)
                return;

            if(tag == "Resolution")
                PreviewEtiqueta.BarCode.BarcodeImageResolution = (BarcodeImageResolution)((ComboBox)sender).SelectedIndex;
            if (tag == "DisplayType")
                PreviewEtiqueta.BarCode.DisplayCodeType = (DisplayCodeType)((ComboBox)sender).SelectedIndex;
            if (tag == "BarHeight")
                PreviewEtiqueta.BarCode.BarcodeHeight = (BarcodeHeight)((ComboBox)sender).SelectedIndex;
            if (tag == "BarType")
            {
                switch (((ComboBox)sender).SelectedItem)    
                {
                    case BarType.Code39:
                        PreviewEtiqueta.BarCode = CreateCodeDefault(BarType.Code39);
                        break;
                    case BarType.EAN13:
                        PreviewEtiqueta.BarCode = CreateCodeDefault(BarType.EAN13);
                        break;
                    default:
                        break;
                }
            }    

            if(tag == "")
            {

            }

            //Image_BarCode.Source = EtiquetaPreview.BarCode.CodeImage;
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
            if(PreviewEtiqueta.BarCode.CodeImage != null)
                info_1.Text = string.Format("W:{0}px - H:{1}px", PreviewEtiqueta.BarCode.CodeImage.PixelWidth, PreviewEtiqueta.BarCode.CodeImage.PixelHeight);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();

            if (tag == "AddCode")
            {
                Etiquetas.Add(PreviewEtiqueta);
                await this.Dispatcher.BeginInvoke(() =>
                {
                    PreviewEtiqueta = CreateNewEtiqueta();
                    UC_VeiwBox_PreviewEtiqueta.Child = (UserControl)PreviewEtiqueta;
                });
                //etiquetaPage = null;
                //etiquetaPage = new EtiquetaA4(Etiquetas.ToArray());
                etiquetaPage.LoadEtiquetas(Etiquetas.ToArray());
                etiquetaPage.Dispatcher.Invoke(() => etiquetaPage.UpdateLayout());
                
            }
            if (tag == "SearchProduct")
            {
                Window w = new Window();
                Ferramentas.Internal.MVC.UC_Produdots_List list = new Ferramentas.Internal.MVC.UC_Produdots_List();
                w.Content = list;
                list.ParentWindow = w;
                if(w.ShowDialog() == true && list.OUT_Produto != null)
                {
                    PreviewEtiqueta.Produto = list.OUT_Produto.Nome;
                    PreviewEtiqueta.Preco = list.OUT_Produto.Preco_cIVA;
                    PreviewEtiqueta.CodigoBarras = list.OUT_Produto.REF;
                    PreviewEtiqueta.BarCode.Code = list.OUT_Produto.REF;
                    PreviewEtiqueta.Taxa = list.OUT_Produto.TaxaIVA;
                }
            }
            if(tag == "PrintPreview")
            {
                PrintDialog printDialog = new PrintDialog();
                System.Printing.PageMediaSize a4 = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISOA4);
                printDialog.PrintTicket.PageMediaSize = a4;
                printDialog.PrintTicket.Duplexing = System.Printing.Duplexing.OneSided;

                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(etiquetaPage, "My First Print Job");
                }
            }
        }
        private void OnTextBox_Changed(object sender, TextChangedEventArgs args)
        {
            string tag = ((TextBox)sender).Tag.ToString();
            string text = ((TextBox)sender).Text;


            if (tag == "Nome")
                PreviewEtiqueta.Produto = text;
            if (tag == "CodigoBarras")
                PreviewEtiqueta.CodigoBarras = text;
            if (tag == "Preco" && float.TryParse(text, out float price))
                PreviewEtiqueta.Preco = price;

            //Image_BarCode.Source = EtiquetaPreview.BarCode.CodeImage;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!this.IsLoaded)
                return;

            CheckBox checkBox = (CheckBox)sender;
            PreviewEtiqueta.MostrarPreco = checkBox.IsChecked.Value;
        }
    }
}
