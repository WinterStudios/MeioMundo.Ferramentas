using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using MeioMundo.Ferramentas.Escola.Internal;
using MeioMundo.Ferramentas.Escola.Modelos;


namespace MeioMundo.Ferramentas.Escola
{
    public class ManuaisSystem
    {
        //public static ObservableCollection<Internal.Escola> Escolas { get; set; }
        //public static ObservableCollection<Internal.Livro> Livros { get; set; }

        public static List<Internal.Escola> Escolas { get; set; }
        public static List<Internal.Livro> Livros { get; set; }

        

        public enum Modelos
        {
            v_2020_07 = 0,
            v_2021_06 = 1,
            v_2021_06_Outro = 2
        }

        public static UserControl GetModelo(Modelos modelo, string nomeEscola, Internal.Ano ano)
        {
            if(modelo == Modelos.v_2020_07)
            {
                GC.Collect();
                Modelo_2020_07 _modelo = new Modelo_2020_07();
                _modelo.Escola = nomeEscola;
                _modelo.Ano = ano;
                _modelo.Run();
                return _modelo;
            }
            if (modelo == Modelos.v_2021_06)
            {
                GC.Collect();
                Modelo_2021_06 _modelo = new Modelo_2021_06();
                _modelo.Escola = nomeEscola;
                _modelo.Ano = ano;
                _modelo.Run();
                return _modelo;
            }
            if(modelo == Modelos.v_2021_06_Outro)
            {
                GC.Collect();
                Modelo_2021_06_Outro _modelo = new Modelo_2021_06_Outro();
                _modelo.Escola = nomeEscola;
                _modelo.Ano = ano;
                _modelo.Run();
                return _modelo;
            }
            return null;
        }

        private static string DataLocationFolder { get {
                string pluginAssemblyDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace("\\","/");
                string pluginDataDirectory = pluginAssemblyDirectory;
                if (!System.IO.Directory.Exists(pluginDataDirectory + "/Data/MeioMundo.Ferramentas/Escola"))
                    System.IO.Directory.CreateDirectory(pluginDataDirectory + "/Data/MeioMundo.Ferramentas/Escola");
                return pluginDataDirectory + "/Data/MeioMundo.Ferramentas/Escola/";
            } }

        public static void Inicialize()
        {
            Escolas = new List<Internal.Escola>();
            //Livros = new ObservableCollection<Internal.Livro>();
            LoadLivros();
            LoadEscolas();
        }

        public static void PrintModelos(Modelos modelo)
        {
            Window window = new Window();

            UC_PrindDialog uc_prindDialog= new UC_PrindDialog();
            window.Content = uc_prindDialog;
            window.Height = 480;
            window.Width = 400;

            window.ShowDialog();
            
            //window.


            //PrintDialog printDialog = new PrintDialog();
            //System.Printing.PageMediaSize envelopeDL = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISODLEnvelope);
            //printDialog.PrintTicket.PageMediaSize = envelopeDL;
            //printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;

            //if (printDialog.ShowDialog() == true)
            //{
            //    UserControl control = GetModelo(Modelos.v_2021_06, "Oliveira do Hospital", Escolas[0].Anos[0]);

            //    var document = new FixedDocument();                
            //    Size pageSize = new Size(control.Width, control.Height);

            //    var fixedPage = new FixedPage();

            //    // Add visual, measure/arrange page.

            //    fixedPage.Width = pageSize.Width;
            //    fixedPage.Height = pageSize.Height;

            //    document.DocumentPaginator.PageSize = pageSize;

            //    fixedPage.Children.Add((UIElement)control);
            //    fixedPage.Measure(pageSize);
            //    fixedPage.Arrange(new Rect(new Point(), pageSize));
            //    fixedPage.UpdateLayout();

            //    // Add page to document
            //    var pageContent = new PageContent();
            //    ((IAddChild)pageContent).AddChild(fixedPage);
            //    document.Pages.Add(pageContent);
                
            //    printDialog.PrintDocument(document.DocumentPaginator, "TEst");
            //}
        }

        #region Escolas

        //public static Internal.Escola AddEscola()
        //{
        //    Internal.Escola escola = new Internal.Escola();
        //    escola.Name = "Escola ...";
        //    escola.ID = Escolas.Count;
        //    escola.Anos = new List<Internal.Ano>();

        //    Escolas.Add(escola);
        //    return escola;
        //}

        public static Internal.Escola[] GetEscolas() => Escolas.ToArray();

        public static void LoadEscolas()
        {
            string escolaFile = DataLocationFolder + "Escolas.json";
            if (System.IO.File.Exists(escolaFile))
            {
                string json = System.IO.File.ReadAllText(escolaFile);
                Escolas = System.Text.Json.JsonSerializer.Deserialize<Internal.Escola[]>(json).ToList();
            }

        }

        internal static void AddEscola()
        {
            Internal.Escola escola = new Internal.Escola();
            escola.ID = Escolas.Count;
            escola.Nome = "Escola ...";
            escola.Anos = new List<Internal.Ano>();
            Escolas.Add(escola);
        }


        public static void SaveEscola()
        {
            string escolaFile = DataLocationFolder + "Escolas.json";
            System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions() { WriteIndented = true };
            string json = System.Text.Json.JsonSerializer.Serialize(Escolas.ToArray(), options);
            System.IO.File.WriteAllText(escolaFile, json);
        }




        #endregion

        #region Livros
        public static void LoadLivros()
        {
            string livrosFile = DataLocationFolder + "Livros.json";
            if (System.IO.File.Exists(livrosFile))
            {
                string json = System.IO.File.ReadAllText(livrosFile);
                Livros = System.Text.Json.JsonSerializer.Deserialize<Internal.Livro[]>(json).ToList();
            }
        }

        #endregion
    }
}
