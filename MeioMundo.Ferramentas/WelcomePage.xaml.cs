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


using U_System.Core;

namespace MeioMundo.Ferramentas
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = (string)((Button)sender).Tag;

            if(tag == "_MANUAL_ESCOLAR")
            {
                TabItem tab = new TabItem();
                tab.Header = "Manuais Escolares";
                tab.Tag = "/MeioMundo.Ferramentas;component/Assets/books.png";
                Escola.ManuaisEscolares manuais = new Escola.ManuaisEscolares();
                tab.Content = manuais;
                U_System.Core.UX.TabsSystem.Add(tab);
            }
        }
    }
}
