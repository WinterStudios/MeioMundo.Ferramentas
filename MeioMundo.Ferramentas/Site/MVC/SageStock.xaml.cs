using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using MeioMundo.Ferramentas.Extensions;
using MeioMundo.Ferramentas.Internal;
using MeioMundo.Ferramentas.Network;
using MeioMundo.Ferramentas.Systems;
using MeioMundo.Ferramentas.ViewModels;


namespace MeioMundo.Ferramentas.Site.MVC
{
    /// <summary>
    /// Interaction logic for SageStock.xaml
    /// </summary>
    public partial class SageStock : UserControl
    {
        public ObservableCollection<Produto> Produtos { get; set; }

        public SageStock()
        {
            //this.Loaded += SageStock_Loaded;
            InitializeComponent();
        }
        
        private async void SageStock_Loaded(object sender, RoutedEventArgs e)
        {
            var data = await Stock.GetProdutosAsync();
            Produtos = new ObservableCollection<Produto>();
            //UC_List_Produtos.ItemsSource = _List;
            //_List.AddRange(data.ToArray());
            
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string m_Tag = ((Button)sender).Tag.ToString();
        }
    }
}
