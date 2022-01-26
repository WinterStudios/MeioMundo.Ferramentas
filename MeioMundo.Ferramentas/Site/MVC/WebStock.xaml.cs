using MeioMundo.Ferramentas.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MeioMundo.Ferramentas.Site.MVC
{
    /// <summary>
    /// Interaction logic for WebStocks.xaml
    /// </summary>
    public partial class WebStock : UserControl, INotifyPropertyChanged
    {
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        public ObservableCollection<Produto> Produtos { get => t_produtos; set { t_produtos = value; OnPropertyChanged(); } }
        private ObservableCollection<Produto> t_produtos;
        public WebStock()
        {
            InitializeComponent();
            LoadAsync();
        }
        
        private void LoadAsync()
        {
            var list = Systems.Stock.GetProdutosWebSiteAsync();
            Produtos = new ObservableCollection<Produto>(list);
        }
    }
}
