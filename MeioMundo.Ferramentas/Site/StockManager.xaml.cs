using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Threading;
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

using MeioMundo.Ferramentas.Site.Models;

namespace MeioMundo.Ferramentas.Site
{
    /// <summary>
    /// Interaction logic for StockManager.xaml
    /// </summary>
    public partial class StockManager : UserControl
    {
        public ObservableCollection<Site.Models.Produto> Produtos { get; set; }
        public StockManager()
        {
            InitializeComponent();
            Produtos = new ObservableCollection<Site.Models.Produto>();
            UC_StockManager.ItemsSource = Produtos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Tag = ((Button)sender).Tag.ToString();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Sage Files|*.xls;*.txt|" +
                            "Text File (*.txt)|*.txt|" +
                            "All Files |*.*";

            if (dialog.ShowDialog() == true)
            {
                if(Tag == "Sage")
                    LoadFile(dialog.FileName, SourceLocation.Sage);
            }
                
            
        }
        private async Task LoadFile(string fileLocation, SourceLocation source)
        {
            string fileExtension = System.IO.Path.GetExtension(fileLocation);
            if (fileExtension.ToUpper() == ".TXT" && source == SourceLocation.Sage)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(1252);

                StreamReader reader = new StreamReader(fileLocation, encoding ,true);
                string line;

                //Index Colluns
                int index = 0;
                int RefIndex = 0;
                int ProdutoIndex = 0;
                int PvpIndex = 0;
                int IvaIndex = 0;
                int StockIndex = 0;


                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] cols = line.Split(',');
                    if(index == 0)
                    {
                        RefIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "REF");
                        ProdutoIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "PRODUTO");
                        PvpIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "PVP");
                        IvaIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "IMPOSTO");
                        StockIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "DISPONÍVEL");
                    }
                    else
                    {
                        Produto p = new Produto();
                        p.REF = cols[RefIndex].Replace("\"", "");
                        p.Nome = cols[ProdutoIndex].Replace("\"", "");
                        float preco = float.NaN;
                        if (float.TryParse(cols[PvpIndex].Replace('.', ','), out preco))
                            p.Preco_C_IVA = preco;
                        p.IVA = cols[IvaIndex];
                        int stockSage = 0;
                        if (int.TryParse(cols[StockIndex], out stockSage))
                            p.StockSage = stockSage;
                        Produtos.Add(p);
                    }
                    
                    index++;
                    
                }
                
            }
        }
    }
}
