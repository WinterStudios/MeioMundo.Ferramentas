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
            dateTimeToday.Text = DateTime.Today.ToLongDateString();
            //leftSideText.Text = string.Format("{0}, {1}, {2}, NIF:{3}",
            //    MeioMundoInformation.DominaçãoSocial, MeioMundoInformationMorada.Rua, MeioMundoInformationMorada.CodigoPostal, MeioMundoInformation.NIF).ToUpper();
        }

        private void richtextbox_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
