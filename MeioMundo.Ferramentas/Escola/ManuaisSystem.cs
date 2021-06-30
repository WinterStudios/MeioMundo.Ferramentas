using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        public static void PrintModelos(Modelos modelo, PagEncomendasEscolares[] pags)
        {
            

            PrintDialog printDialog = new PrintDialog();
            System.Printing.PageMediaSize a4 = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISOA4);
            printDialog.PrintTicket.PageMediaSize = a4;

            if (printDialog.ShowDialog() == true)
            {
                var document = new FixedDocument(); 
                document.DocumentPaginator.PageSize = new Size(
                    (double)new LengthConverter().ConvertFromString("21cm"), 
                    (double)new LengthConverter().ConvertFromString("29,7cm")
                    );
                for (int i = 0; i < pags.Length; i++)
                {
                    for (int copies = 0; copies < pags[i].Copies; copies++)
                    {
                        // Add visual, measure/arrange page.
                        UserControl control = GetModelo(modelo, pags[i].Escola.Nome, pags[i].SelectAno);
                        Size pageSize = new Size(control.Width, control.Height);

                        FixedPage fixedPage = new FixedPage();
                        fixedPage.Width = pageSize.Width;
                        fixedPage.Height = pageSize.Height;

                        fixedPage.Children.Add((UIElement)control);
                        fixedPage.Measure(pageSize);
                        fixedPage.Arrange(new Rect(new Point(), pageSize));
                        fixedPage.UpdateLayout();
                        // Add page to document
                        var pageContent = new PageContent();
                        ((IAddChild)pageContent).AddChild(fixedPage);
                        document.Pages.Add(pageContent);
                    }
                }
                printDialog.PrintDocument(document.DocumentPaginator, "Mod.2021 - Encomendas Escolares");
            }
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

        public static void SaveLivros()
        {
            string livrosFile = DataLocationFolder + "Livros.json";

            string json = System.Text.Json.JsonSerializer.Serialize(Livros, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(livrosFile, json);
        }

        #endregion


        #region Xaml Extensions Methours Helpers
        public static int[] GetCopies()
        {
            int[] _c = new int[]
            {
                1,2,3,4,5,6,7,8,9,
                10,15,20,25
            };
            return _c;
        }

        #endregion
    }
}
