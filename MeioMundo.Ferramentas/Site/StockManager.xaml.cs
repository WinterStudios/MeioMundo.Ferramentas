﻿using Microsoft.Win32;
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
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System.ComponentModel;

namespace MeioMundo.Ferramentas.Site
{
    /// <summary>
    /// Interaction logic for StockManager.xaml
    /// </summary>
    public partial class StockManager : UserControl
    {
        public ObservableCollection<Site.Models.Produto> Produtos { get; set; }
        private List<Produto> SageProdutos { get; set; }

        public bool WebSiteLoad { get; set; }
        public bool SageLoad { get; set; }
        public StockManager()
        {
            InitializeComponent();
            Produtos = new ObservableCollection<Site.Models.Produto>();
            SageProdutos = new List<Produto>();
            UC_StockManager.ItemsSource = Produtos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            if (tag == "Load")
            {
                string sageLocation;
                string webLocation;

                OpenFileDialog WebDialog = new OpenFileDialog();
                WebDialog.Filter = "Website Files|*.csv;*.txt|" +
                                   "All Files |*.*";
                WebDialog.Title = "Carregar ficheiro com os dados do Site";

                if (WebDialog.ShowDialog() == true)
                {
                    webLocation = WebDialog.FileName;
                    OpenFileDialog SageDialog = new OpenFileDialog();
                    SageDialog.Filter = "Sage Files|*.xls;*.txt|" +
                                        "All Files |*.*";

                    SageDialog.Title = "Carregar ficheiro com os dados do SAGE";
                    if (SageDialog.ShowDialog() == true)
                    {
                        sageLocation = SageDialog.FileName;
                        LoadDataBase(webLocation, sageLocation);
                    }
                }
            }
            if(tag == "Upload")
            {
                CreateCSVFile();
            }
                
            
        }
        private async void LoadDataBase(string webLocation, string sageLocation)
        {
            Produtos.Clear();
            SageProdutos.Clear();
            string webExtension = System.IO.Path.GetExtension(webLocation);
            string sageExtension = System.IO.Path.GetExtension(sageLocation);
            if (webExtension.ToUpper() == ".CSV" && sageExtension.ToUpper() == ".TXT")
            {
                await LoadWebSiteAsync(webLocation);
                await LoadSageAsync(sageLocation);
                Task task = new Task(() => UpdateStockAsync());
                task.Start();
            }
        }

        private async Task LoadWebSiteAsync(string location)
        {
            StreamReader reader = new StreamReader(location, Encoding.UTF8);
            string line;

            //Index Colluns
            int index = 0;
            int RefIndex = 0;
            int ProdutoNome = 1;
            int PvpIndex = 4;
            int IvaIndex = 2;
            int StockIndex = 3;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                //Separating columns to array
                string newLine = line.Replace(@"\", "");
                string[] cols = CSVParser.Split(newLine);
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].Contains('\"'))
                        cols[i] = cols[i].Replace("\"", "");
                }
                if (index == 0)
                {
                    RefIndex = cols.ToList().FindIndex(x => x == "REF");
                    ProdutoNome = cols.ToList().FindIndex(x => x == "Nome");
                    PvpIndex = cols.ToList().FindIndex(x => x == "Preço normal");
                    IvaIndex = cols.ToList().FindIndex(x => x == "Classe de imposto");
                    StockIndex = cols.ToList().FindIndex(x => x == "Stock");
                }
                else
                {
                    Produto p = new Produto();
                    p.REF = cols[RefIndex];
                    p.Nome = cols[ProdutoNome];
                    if(int.TryParse(cols[StockIndex], out _))
                        p.StockSite = int.Parse(cols[StockIndex]);
                    Produtos.Add(p);
                }

                index++;
                UC_StatusBar_ListObjects.Text = string.Format("Produtos: {0}", Produtos.Count);
            }
        }

        private async Task LoadSageAsync(string location)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding(1252);

            StreamReader reader = new StreamReader(location, encoding, true);
            string line;

            //Index Colluns
            int index = 0;
            int RefIndex = 0;
            int NomeIndex = 1;
            int PvpIndex = 2;
            int IvaIndex = 5;
            int StockIndex = 3;


            while ((line = reader.ReadLine()) != null)
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
                        Produto p = new Produto();
                        p.REF = cols[RefIndex].Replace("\"", "");
                        p.Nome = cols[NomeIndex].Replace("\"", "");
                        float preco = float.NaN;
                        if (float.TryParse(cols[PvpIndex].Replace('.', ','), out preco))
                            p.Preco_cIVA = preco;
                        //p.IVA = cols[IvaIndex];
                        int stockSage = 0;
                        if (int.TryParse(cols[StockIndex], out stockSage))
                            p.StockSage = stockSage;

                        SageProdutos.Add(p);
                    }
                }
                index++;
                UC_StatusBar_ListObjects.Text = string.Format("Produtos: {0}", Produtos.Count);
            }
        }

        private void UpdateStockAsync()
        {
            double x = 0;
            double percentagem = 0;
            double max = Produtos.Count * SageProdutos.Count;
            Dispatcher dispatcher = this.Dispatcher;
            long c = 0;
            Parallel.For(0, Produtos.Count, i =>
            {
                for (int z = 0; z < SageProdutos.Count; z++)
                {
                    c++;
                    if (Produtos[i].REF == SageProdutos[z].REF)
                    {                        
                        dispatcher.Invoke(() =>    
                        {
                            UC_StatusBar_ListObjects.Text = string.Format("Comparações: {0}", c);
                            UC_StatusBar_Progress.Value = percentagem;
                        }, DispatcherPriority.Background);
                        Produtos[i].StockSage = SageProdutos[z].StockSage;
                        Produtos[i].Preco_cIVA = SageProdutos[z].Preco_cIVA;
                        break;
                    }
                }
                x++;
                percentagem = x / Produtos.Count;
            });
            dispatcher.Invoke(() => UC_StatusBar_ListObjects.Text = "Done");
        }


        private async Task CreateCSVFile()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "Export_1";

            if(saveFile.ShowDialog() == true)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("REF,Nome,Stock,\"Classe de imposto\",\"Preço normal\"");
                for (int i = 0; i < Produtos.Count; i++)
                {
                    sb.AppendLine(Produtos[i].ToStringCSV());
                }
                
                await File.WriteAllTextAsync(saveFile.FileName, sb.ToString());
            }

            
        }

        private bool FilterRowsREF(object item)
        {
            if (((Produto)item).REF.Contains(TextBox_SearchText.Text) || ((Produto)item).Nome.Contains(TextBox_SearchText.Text))
                    return true;
            return false;
        }

        private async void TextBox_RefSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;

            string tag = box.Tag.ToString();
            
            if (box != null)
            {
                string searchText = Dispatcher.Invoke(() => box.Text);
                if (!string.IsNullOrEmpty(searchText))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Produtos);
                    await Dispatcher.InvokeAsync(() => view.Filter = FilterRowsREF, DispatcherPriority.Background);

                }
                if(string.IsNullOrEmpty(searchText))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Produtos);
                    view.Filter = null;
                }
            }
        }
    }
}
