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

using MeioMundo.Ferramentas;
using MeioMundo.Ferramentas.Internal;

namespace MeioMundo.Ferramentas.Correio.Internal
{
    /// <summary>
    /// Interaction logic for CartaBodyTemplate.xaml
    /// </summary>
    public partial class CartaBodyTemplate : UserControl
    {
        public CartaBodyTemplate()
        {
            InitializeComponent(); 
            AppContext.SetSwitch(@"Switch.System.Windows.Controls.DoNotAugmentWordBreakingUsingSpeller", true);
            dateTimeToday.Text = string.Format("Oliveira do Hospital, {0}",DateTime.Today.ToLongDateString());
            //leftSideText.Text = string.Format("{0}, {1}, {2}, NIF:{3}",
            //    MeioMundoInformation.DominaçãoSocial, MeioMundoInformationMorada.Rua, MeioMundoInformationMorada.CodigoPostal, MeioMundoInformation.NIF).ToUpper();
        }

        private void richtextbox_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();

            if(tag == "__SelectForn")
            {
                FornecedorSelect fornecedorAddress = new FornecedorSelect();
                Window window = new Window();
                window.Content = fornecedorAddress;
                window.SizeToContent = SizeToContent.WidthAndHeight;
                Morada morada = new Morada();
                if (window.ShowDialog() == true)
                {                   
                    Paragraph nome = new Paragraph();
                    nome.Inlines.Add(fornecedorAddress.Morada.FornecedorNome);

                    Paragraph _morada = new Paragraph();
                    _morada.Inlines.Add(fornecedorAddress.Morada.Rua);
                    Paragraph _locat = new Paragraph();
                    _morada.Inlines.Add(fornecedorAddress.Morada.Localidade);
                    Paragraph zip = new Paragraph();
                    zip.Inlines.Add(fornecedorAddress.Morada.ZipCode);

                    fornMorada.Document = new FlowDocument();

                    fornMorada.Document.Blocks.Add(nome);
                    fornMorada.Document.Blocks.Add(_morada);
                    fornMorada.Document.Blocks.Add(_locat);
                    fornMorada.Document.Blocks.Add(zip);

                }

            }

        }
    }
}
