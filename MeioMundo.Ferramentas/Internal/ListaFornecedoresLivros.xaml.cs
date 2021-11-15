using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ListaFornecedoresLivros.xaml
    /// </summary>
    public partial class ListaFornecedoresLivros : UserControl
    {
        public ObservableCollection<Models.FornecedorLivro> FornecedorLivros { get; set; }
        public ListaFornecedoresLivros()
        {
            FornecedorLivros = new ObservableCollection<Models.FornecedorLivro>();
            FornecedorLivros.CollectionChanged += FornecedorLivros_CollectionChanged;
            InitializeComponent();
        }

        private void FornecedorLivros_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //Save();
            }
        }
    }
}
