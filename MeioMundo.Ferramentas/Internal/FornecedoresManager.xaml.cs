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

namespace MeioMundo.Ferramentas.Internal
{
    /// <summary>
    /// Interaction logic for FornecedoresSystem.xaml
    /// </summary>
    public partial class FornecedoresManager : UserControl
    {
        public List<Fornecedor> Fornecedors { get; set; }
        public FornecedoresManager()
        {
            Fornecedors = FornecedorSystem.Fornecedores;
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();

            if(tag == "_UPDATE_FORNECEDORES")
            {
                await FornecedorSystem.LoadFromFile();
                grid.Items.Refresh();
            }
        }
    }
}
