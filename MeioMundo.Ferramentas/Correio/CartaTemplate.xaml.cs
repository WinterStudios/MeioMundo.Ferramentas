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
            ResgistoSource = Barcode.Barcode.CreateBarcode("FR19832323PT", Barcode.BarcodeEncoding.Code39).ToImage((int)Math.Round(code.Width), (int)Math.Round(code.Height));
            code.Source = ResgistoSource;
        }

        private void UpdateResgistoCode(string s)
        {
            //code.Source = new BitmapImage();
            
            code.Source = Barcode.Barcode.CreateBarcode(s, Barcode.BarcodeEncoding.Code39).ToImage((int)Math.Round(code.Width), (int)Math.Round(code.Height));

        }
    }
}
