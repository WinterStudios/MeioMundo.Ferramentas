using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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

namespace MeioMundo.Ferramentas.Correio
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {

        public TypeRegister RegisterType { get => _registerType; set { _registerType = value; } }
        private TypeRegister _registerType;

        public Main()
        {
            InitializeComponent();
            //UC_Viewbox_PreviewModelo.Child = new CartaTemplate();
            //combox_registorsTypes.ItemsSource = Enum.GetValues(typeof(TypeRegister)).Cast<TypeRegister>();
            //combox_registorsTypes.SelectedIndex = 0;
            var d = Internal.FornecedorSystem.Fornecedores;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tag = ((TextBox)sender).Tag.ToString();
            string text = ((TextBox)sender).Text;

            if (tag == "Morada_Rua")
                carta.Morada_Rua = text;
            if (tag == "Morada_Localidade")
                carta.Morada_Localidade = text;
            if (tag == "Morada_CodigoPostal")
                carta.Morada_CodigoPostal = text;
            if (tag == "Morada_Pais")
                carta.Morada_Pais = text;

            if (tag == "Registo")
                carta.ResgistoCode = text;            
        }

        private void combox_registorsTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            carta.UpdateSelo(RegisterType);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            if (tag == "PrintEnvelope")
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    System.Printing.PageMediaSize envelopeDL = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISODLEnvelope);
                    printDialog.PrintTicket.PageMediaSize = envelopeDL;

                    printDialog.PrintVisual(carta, "My First Print Job");
                }
            }
        }
    }
}
