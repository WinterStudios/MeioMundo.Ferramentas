using MeioMundo.Ferramentas.Escola.Internal;
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

namespace MeioMundo.Ferramentas.Escola.Modelos
{
    /// <summary>
    /// Interaction logic for Modelo_2021_Profisional.xaml
    /// </summary>
    public partial class Modelo_2021_Profisional : UserControl, ModeloInfo
    {
        public Modelo_2021_Profisional()
        {
            InitializeComponent();
        }

        public string Version => "v.2021.08-Prof";
        public string Escola { get; set; }
        public Ano Ano { get; set; }
        public UserControl __UserControl => this;


        private int _DisciplinasGerais { get => 17; }

        public void Run()
        {
            Window _W = new Window();
            Border _Border = new Border();
            GroupBox _GroupBox = new GroupBox();
            TextBox _E = new TextBox() { Width = 200 };

            ResourceDictionary _ResourceDictionary = new ResourceDictionary() { Source = new Uri("/MeioMundo.Ferramentas;component/ShareResources.xaml", UriKind.RelativeOrAbsolute) };

            // Window Definitions
            _W.SizeToContent = SizeToContent.WidthAndHeight;
            _W.Content = _Border;
            _W.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _W.Resources.MergedDictionaries.Add(_ResourceDictionary);
            _W.Background = (SolidColorBrush)this.Resources["Background"];


            // Border Settings
            _Border.Child = _GroupBox;

            // Group Box Settings
            _GroupBox.Header = "Nome da Escola";
            _GroupBox.Content = _E;

            // Text Box Settings 
            _E.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                    _W.DialogResult = true;
            };
            _E.Focus();



            if (string.IsNullOrEmpty(Escola)  && _W.ShowDialog() == true)
                UC_TextBox_Escola.Text = _E.Text;
            else
                UC_TextBox_Escola.Text = Escola;

            if (Ano == null)
                Ano = new Ano();

            UC_TextBox_Ano.Text = Ano.ToString();
            
            Load();
        }
        public void Load()
        {
            List<Internal.Disciplina> __Disc_CG = new List<Disciplina>(Ano.Disciplinas.Where(x => x.Disc_Espe == false));
            for (int i = __Disc_CG.Count; i < _DisciplinasGerais; i++)
            {
                __Disc_CG.Add(new Disciplina());
            }
            UC_DISC_COMP_MODULOS.ItemsSource = __Disc_CG.Take(13);

            for (int i = 0; i < 2; i++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = (VisualBrush)this.FindResource("Color.Brush.Pattern.Right");
                rectangle.Height = (double)new LengthConverter().ConvertFrom("0,2cm");
                double margin = (double)new LengthConverter().ConvertFrom("0,2cm");
                rectangle.Margin = new Thickness(0, margin, 0, margin);
                UC_StackPanel_Matriculas.Children.Add(rectangle);
                Modelo_2021_06_Box escolha = new Modelo_2021_06_Box(Ciclo.Basico);

                int discLenghtDG = _DisciplinasGerais;
                Disciplina[] discG = new Disciplina[discLenghtDG];
                for (int z = 0; z < discLenghtDG; z++)
                {
                    if (z < Ano.Disciplinas.Count)
                        discG[z] = Ano.Disciplinas[z];
                    else
                    {
                        discG[z] = new Disciplina();
                    }
                }
                escolha.UC_ListBox_Discplinas_CG.ItemsSource = discG;
                UC_StackPanel_Matriculas.Children.Add(escolha);
            }
        }
    }
}
