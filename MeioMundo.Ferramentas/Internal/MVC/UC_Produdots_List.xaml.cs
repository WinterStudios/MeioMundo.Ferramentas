using MeioMundo.Ferramentas.Internal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace MeioMundo.Ferramentas.Internal.MVC
{
    /// <summary>
    /// Interaction logic for UC_Produdots_List.xaml
    /// </summary>
    public partial class UC_Produdots_List : UserControl, INotifyPropertyChanged
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
        public ObservableCollection<Produto> Produtos { get; set; }

        public Produto OUT_Produto 
        {
            get { return m_out_Produto; }
            set { m_out_Produto = value; OnPropertyChanged(); }
        }

        private Produto m_out_Produto;
        public Window ParentWindow { get; set; }
        public UC_Produdots_List()
        {
            InitializeComponent();
            
            this.Loaded += UC_Produdots_List_Loaded;
        }

        private async void UC_Produdots_List_Loaded(object sender, RoutedEventArgs e)
        {
            Produtos = new ObservableCollection<Produto>();
            UC_DataGrid_ListProdutos.ItemsSource = Produtos;
            string serverPath = @"srvmm";

            string filePath = @"\\Srvmm\USR\MeioMundo_Local\Listagem de Artigos _EUROS_.TXT";
            FileStream file = Network.AccessFiles.ReadFile(filePath, "meiomundo", "meiomundo");

            if (file == null)
                return;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding(1252);

            StreamReader reader = new StreamReader(file, encoding, true);

            string line = string.Empty;
            int index = 0;
            //Index Colluns
            int RefIndex = 0;
            int NomeIndex = 1;
            int PvpIndex = 2;
            int IvaIndex = 5;
            int StockIndex = 3;
                
            while ((line = await reader.ReadLineAsync()) != null)
            {
                string[] cols = line.Split(',');
                if (cols.Length > 5)
                {
                    if (index == 0)
                    {
                        //RefIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "REF");
                        //NomeIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "PRODUTO");
                        //PvpIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "PVP");
                        //IvaIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "IMPOSTO");
                        //StockIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "DISPONÍVEL");
                    }
                    else
                    {
                        Models.Produto p = new Models.Produto();
                        p.REF = cols[RefIndex].Replace("\"", "");
                        p.Nome = cols[NomeIndex].Replace("\"", "");
                        float preco = float.NaN;
                        if (float.TryParse(cols[PvpIndex].Replace('.', ','), out preco))
                            p.Preco_cIVA = preco;
                        p.TaxaImposto = Produto.SetTaxaImposto(cols[IvaIndex]);
                        p.Preco_sIVA = p.Preco_cIVA / (1 + p.TaxaIVA);
                        int stockSage = 0;
                        if (int.TryParse(cols[StockIndex], out stockSage))
                            p.StockSage = stockSage;

                                
                        Produtos.Add(p);
                    }
                    index++;
                }

            }                
                
        }

        private void UC_DataGrid_ListProdutos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParentWindow.DialogResult = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string t = ((TextBox)sender).Text;
            ICollectionView filteredView = new CollectionViewSource { Source = UC_DataGrid_ListProdutos.ItemsSource }.View;
            UC_DataGrid_ListProdutos.Items.Filter = new Predicate<object>(x => ((Produto)x).REF.Contains(t));
        }
    }
}

