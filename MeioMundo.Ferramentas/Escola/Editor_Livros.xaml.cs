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

namespace MeioMundo.Ferramentas.Escola
{
    /// <summary>
    /// Interaction logic for Editor_Livros.xaml
    /// </summary>
    public partial class Editor_Livros : UserControl
    {
        public ObservableCollection<Internal.Livro> Livros { get => ManuaisSystem.Livros; }
        public Editor_Livros()
        {
            InitializeComponent();
        }
    }
}
