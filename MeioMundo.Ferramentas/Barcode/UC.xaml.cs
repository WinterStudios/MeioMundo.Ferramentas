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

        public ObservableCollection<Etiqueta> Etiquetas { get => m_etiquetas; set { m_etiquetas = value; OnPropertyChanged(); } }
        private ObservableCollection<Etiqueta> m_etiquetas;

        public EtiquetaA4 etiquetaPage { get => m_EtiquetaA4; set { m_EtiquetaA4 = value; OnPropertyChanged(); } }
        private EtiquetaA4 m_EtiquetaA4;

        public int Qtd { get => m_Qtd; set { m_Qtd = value; OnPropertyChanged(); } }
        private int m_Qtd;

        public IEtiqueta PreviewEtiqueta
        {
            get { return m_PreviewEtiqueta; }
            set { m_PreviewEtiqueta = value; OnPropertyChanged(); }
        }

        private IEtiqueta m_PreviewEtiqueta;

        public Etiqueta SelectEtiqueta { get => m_selectEtiqueta; set { m_selectEtiqueta = value; OnPropertyChanged(); } }
        private Etiqueta m_selectEtiqueta;

        public UC()
        {
            InitializeComponent();
            
            U_System.UX.MainFrame.RequestMax();
            PreviewEtiqueta = CreateNewEtiqueta();
            UC_VeiwBox_PreviewEtiqueta.Child = (UserControl)PreviewEtiqueta;
            //Etiquetas.CollectionChanged += Etiquetas_CollectionChanged;

            Etiquetas = new ObservableCollection<Etiqueta>();
            Etiquetas.Add(CreateNovaEtiqueta());
            SelectEtiqueta = Etiquetas.First();

            etiquetaPage = new EtiquetaA4();
            previewPage.Child = etiquetaPage;

        }

        private Etiqueta CreateNovaEtiqueta()
        {
            Etiqueta m_etiqueta = new Etiqueta();
            m_etiqueta.Preco = 0;
            m_etiqueta.Produto = string.Empty;
            m_etiqueta.SKU = string.Empty;
            m_etiqueta.BarCode = (IBarCode)Activator.CreateInstance(typeof(Code39));
            m_etiqueta.BarCode.BarcodeImageResolution = BarcodeImageResolution.Medium;
            m_etiqueta.BarCode.BarcodeHeight = BarcodeHeight.Normal;
            m_etiqueta.BarCode.DisplayCodeType = DisplayCodeType.Center;
            return m_etiqueta;
        }
        private IEtiqueta CreateNewEtiqueta()
        {
            IEtiqueta t_IEtiquetaPreview = (IEtiqueta)Activator.CreateInstance(typeof(Etiqueta_A));
            t_IEtiquetaPreview.Etiqueta = new Etiqueta();
            t_IEtiquetaPreview.Etiqueta.Preco = 0;
            t_IEtiquetaPreview.Etiqueta.Produto = string.Empty;
            t_IEtiquetaPreview.Etiqueta.SKU = string.Empty;
            t_IEtiquetaPreview.Etiqueta.BarCode = (IBarCode)Activator.CreateInstance(typeof(Code39));
            t_IEtiquetaPreview.Etiqueta.BarCode.BarcodeImageResolution = BarcodeImageResolution.Medium;
            t_IEtiquetaPreview.Etiqueta.BarCode.BarcodeHeight = BarcodeHeight.Normal;
            t_IEtiquetaPreview.Etiqueta.BarCode.DisplayCodeType = DisplayCodeType.Center;
            return t_IEtiquetaPreview;

        }

        public static IEnumerable<Type> GetIEtiquetas()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where((typeof(IEtiqueta)).IsAssignableFrom).Where(x => !x.IsInterface).ToList();
        }

        private void Etiquetas_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            etiquetaPage.UpdateList();  
        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            PreviewEtiqueta.Etiqueta.BarCode.Code = Code.Text;
            //Image_BarCode.Source = EtiquetaPreview.BarCode.CodeImage;
            GetInfo();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tag = ((ComboBox)sender).Tag.ToString();

            if (!this.IsLoaded)
                return;

            if(tag == "Resolution")
                PreviewEtiqueta.Etiqueta.BarCode.BarcodeImageResolution = (BarcodeImageResolution)((ComboBox)sender).SelectedIndex;
            if (tag == "DisplayType")
                PreviewEtiqueta.Etiqueta.BarCode.DisplayCodeType = (DisplayCodeType)((ComboBox)sender).SelectedIndex;
            if (tag == "BarHeight")
                PreviewEtiqueta.Etiqueta.BarCode.BarcodeHeight = (BarcodeHeight)((ComboBox)sender).SelectedIndex;
            if (tag == "BarType")
            {
                switch (((ComboBox)sender).SelectedItem)    
                {
                    case BarType.Code39:
                        PreviewEtiqueta.Etiqueta.BarCode = CreateCodeDefault(BarType.Code39);
                        break;
                    case BarType.EAN13:
                        PreviewEtiqueta.Etiqueta.BarCode = CreateCodeDefault(BarType.EAN13);
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
            if(PreviewEtiqueta.Etiqueta.BarCode.CodeImage != null)
                info_1.Text = string.Format("W:{0}px - H:{1}px", PreviewEtiqueta.Etiqueta.BarCode.CodeImage.PixelWidth, PreviewEtiqueta.Etiqueta.BarCode.CodeImage.PixelHeight);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();

            if (tag == "AddCode")
            {
                for (int i = 0; i < Qtd; i++)
                {
                    Etiqueta etiqueta = new Etiqueta();
                    etiqueta.Preco = PreviewEtiqueta.Etiqueta.Preco;
                    etiqueta.Produto = PreviewEtiqueta.Etiqueta.Produto;
                    etiqueta.SKU = PreviewEtiqueta.Etiqueta.SKU;
                    etiqueta.BarCode = (IBarCode)Activator.CreateInstance(PreviewEtiqueta.Etiqueta.BarCode.GetType());
                    etiqueta.CodigoBarras = PreviewEtiqueta.Etiqueta.CodigoBarras;
                    etiqueta.BarCode.Code = PreviewEtiqueta.Etiqueta.BarCode.Code;
                    etiqueta.BarCode.BarcodeImageResolution = PreviewEtiqueta.Etiqueta.BarCode.BarcodeImageResolution;
                    etiqueta.BarCode.BarcodeHeight = PreviewEtiqueta.Etiqueta.BarCode.BarcodeHeight;
                    etiqueta.BarCode.DisplayCodeType = PreviewEtiqueta.Etiqueta.BarCode.DisplayCodeType;
                    etiqueta.MostrarPreco = PreviewEtiqueta.Etiqueta.MostrarPreco;
                    etiqueta.Taxa = PreviewEtiqueta.Etiqueta.Taxa;

                    Etiquetas.Add(etiqueta);


                    //IEtiqueta t_IEtiquetaPreview = (IEtiqueta)Activator.CreateInstance(typeof(Etiqueta_A));

                    //t_IEtiquetaPreview.Etiqueta.Preco = PreviewEtiqueta.Etiqueta.Preco;
                    //t_IEtiquetaPreview.Etiqueta.Produto = PreviewEtiqueta.Etiqueta.Produto;
                    //t_IEtiquetaPreview.Etiqueta.SKU = PreviewEtiqueta.Etiqueta.SKU;
                    //t_IEtiquetaPreview.Etiqueta.BarCode = (IBarCode)Activator.CreateInstance(PreviewEtiqueta.Etiqueta.BarCode.GetType());
                    //t_IEtiquetaPreview.Etiqueta.CodigoBarras = PreviewEtiqueta.Etiqueta.CodigoBarras;
                    //t_IEtiquetaPreview.Etiqueta.BarCode.Code = PreviewEtiqueta.Etiqueta.BarCode.Code;
                    //t_IEtiquetaPreview.Etiqueta.BarCode.BarcodeImageResolution = PreviewEtiqueta.Etiqueta.BarCode.BarcodeImageResolution;
                    //t_IEtiquetaPreview.Etiqueta.BarCode.BarcodeHeight = PreviewEtiqueta.Etiqueta.BarCode.BarcodeHeight;
                    //t_IEtiquetaPreview.Etiqueta.BarCode.DisplayCodeType = PreviewEtiqueta.Etiqueta.BarCode.DisplayCodeType;
                    //t_IEtiquetaPreview.Etiqueta.MostrarPreco = PreviewEtiqueta.Etiqueta.MostrarPreco;
                    //t_IEtiquetaPreview.Etiqueta.Taxa = PreviewEtiqueta.Etiqueta.Taxa;
                    //Etiquetas.Add(t_IEtiquetaPreview.Etiqueta);
                    //PreviewEtiqueta = CreateNewEtiqueta();
                }


                //await this.Dispatcher.InvokeAsync(() =>
                //{   
                //    UC_VeiwBox_PreviewEtiqueta.Child = (UserControl)PreviewEtiqueta;
                //});
                //etiquetaPage = null;
                //etiquetaPage = new EtiquetaA4(Etiquetas.ToArray());
                //PreviewEtiqueta = CreateNewEtiqueta();
                //etiquetaPage.LoadEtiquetas(Etiquetas.ToArray());
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
                    SelectEtiqueta.Produto = list.OUT_Produto.Nome;
                    SelectEtiqueta.Preco = list.OUT_Produto.Preco_cIVA;
                    SelectEtiqueta.CodigoBarras = list.OUT_Produto.REF;
                    SelectEtiqueta.BarCode.Code = list.OUT_Produto.REF;
                    SelectEtiqueta.Taxa = list.OUT_Produto.TaxaIVA;
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
                PreviewEtiqueta.Etiqueta.Produto = text;
            if (tag == "CodigoBarras")
                PreviewEtiqueta.Etiqueta.CodigoBarras = text;
            if (tag == "Preco" && float.TryParse(text, out float price))
                PreviewEtiqueta.Etiqueta.Preco = price;

            //Image_BarCode.Source = EtiquetaPreview.BarCode.CodeImage;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!this.IsLoaded)
                return;

            CheckBox checkBox = (CheckBox)sender;
            PreviewEtiqueta.Etiqueta.MostrarPreco = checkBox.IsChecked.Value;
        }
    }
}
