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

using MeioMundo.Ferramentas.Barcode;
using MeioMundo.Ferramentas.Barcode.Internal;

namespace MeioMundo.Ferramentas.Correio
{
    /// <summary>
    /// Interaction logic for CartaTemplate.xaml
    /// </summary>
    public partial class CartaTemplate : UserControl, INotifyPropertyChanged
    {
        public string ResgistoCode { get => resgistoCode; set { resgistoCode = value; UpdateResgistoCode(value); NotifyPropertyChanged(); } }
        private string resgistoCode;

        public ImageSource ResgistoSource { get => resgistoSource; set { resgistoSource = value; NotifyPropertyChanged(); } }
        private ImageSource resgistoSource;

        public ImageSource SeloSource { get => seloSource; set { seloSource = value; NotifyPropertyChanged(); } }
        private ImageSource seloSource;


        public string Morada_Rua { get => _morada_rua; set { _morada_rua = value; NotifyPropertyChanged(); } }
        private string _morada_rua;
        public string Morada_Localidade { get => _morada_localidade; set { _morada_localidade = value; NotifyPropertyChanged(); } }
        private string _morada_localidade;
        public string Morada_CodigoPostal { get => _morada_codigopostal; set { _morada_codigopostal = value; NotifyPropertyChanged(); } }
        private string _morada_codigopostal;
        public string Morada_Pais { get => _morada_pais; set { _morada_pais = value; NotifyPropertyChanged(); } }
        private string _morada_pais;

        public Visibility GridLogoVisiblity { get => _gridLogoVisiblity; set { _gridLogoVisiblity = value; NotifyPropertyChanged(); } }
        private Visibility _gridLogoVisiblity;


        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public CartaTemplate()
        {
            InitializeComponent();
            code.Source = ResgistoSource;
        }

        private void UpdateResgistoCode(string s)
        {
            //code.Source = new BitmapImage();
            
            code.Source = Barcode.Barcode.CreateBarcode(s, Barcode.BarcodeEncoding.Code39).ToImage((int)Math.Round(code.Width), (int)Math.Round(code.Height));

        }

        internal void UpdateSelo(TypeRegister type)
        {
            BitmapImage image = null;
            switch (type)
            {
                case TypeRegister.Normal:
                    break;
                case TypeRegister.Azul:
                    image = new BitmapImage(new Uri("pack://application:,,,/MeioMundo.Ferramentas;component/Assets/correio-azul.png"));
                    break;
                case TypeRegister.Verde:
                    image = new BitmapImage(new Uri("pack://application:,,,/MeioMundo.Ferramentas;component/Assets/correio-verde.png"));
                    break;
                case TypeRegister.Registado:
                    image = new BitmapImage(new Uri("pack://application:,,,/MeioMundo.Ferramentas;component/Assets/correio-registado.png"));
                    break;
                default:
                    break;
            }
            SeloSource = image;
        }
    }
}
