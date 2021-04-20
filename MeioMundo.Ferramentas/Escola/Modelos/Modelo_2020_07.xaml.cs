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
    /// Interaction logic for Modelo_2020_07.xaml
    /// </summary>
    public partial class Modelo_2020_07 : UserControl, ModeloInfo
    {
        public Modelo_2020_07()
        {
            InitializeComponent();
        }

        public string Version => "v.2020.07";
        public string Escola { get; set; }
        public Ano Ano { get; set; }
        public UserControl __UserControl => this;

        public enum Ciclo
        {
            Primaria = 0,
            Basico = 1,
            Secundario = 2
        }

        public Ciclo _Ciclo { get => _ciclo; set { _ciclo = value; } }
        private Ciclo _ciclo;

        private int _DisciplinasGerais
        {
            get
            {
                switch (_Ciclo)
                {
                    case Ciclo.Primaria:
                        return 5;
                    case Ciclo.Basico:
                        return 15;
                    case Ciclo.Secundario:
                        return 9;
                    default:
                        return 0;

                }
            }
        }

        public void Run()
        {
            _Ciclo = Ciclo.Basico;
            if (Ano.ID.ToString()[0] == '1')
                _Ciclo = Ciclo.Primaria;
            if (Ano.ID.ToString()[0] == '4')
                _Ciclo = Ciclo.Secundario;

            SetUI(_Ciclo);
        }


        /// <summary>
        /// 
        /// </summary>
        public void LoadDisciplinas()
        {
            List<Internal.Disciplina> __Disc_CG = new List<Disciplina>(Ano.Disciplinas);

            for (int i = __Disc_CG.Count; i < _DisciplinasGerais; i++)
            {
                __Disc_CG.Add(new Disciplina());
            }

            UC_DISC_COMP_GERAL.ItemsSource = __Disc_CG;
        }

        public void SetUI(Ciclo ciclo)
        {
            UC_TextBox_Ano.Text = Ano.ToString();
            UC_TextBox_Escola.Text = Escola;
            if (ciclo == Ciclo.Primaria)
            {
                UC_STACKPANEL_COMP_ESPECIFICA.Visibility = Visibility.Collapsed;
            }
            if (ciclo == Ciclo.Basico)
            {
                UC_STACKPANEL_COMP_ESPECIFICA.Visibility = Visibility.Collapsed;
            }
            if (ciclo == Ciclo.Secundario)
            {
                UC_STACKPANEL_COMP_ESPECIFICA.Visibility = Visibility.Visible;
            }
            LoadMatricula();
            LoadDisciplinas();
        }
        public void LoadMatricula()
        {
            if (_Ciclo == Ciclo.Primaria)
            {
                for (int i = 0; i < 4; i++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = (VisualBrush)this.FindResource("Color.Brush.Pattern.Right");
                    rectangle.Height = (double)new LengthConverter().ConvertFrom("0,2cm");
                    double margin = (double)new LengthConverter().ConvertFrom("0,2cm");
                    rectangle.Margin = new Thickness(0, margin, 0, margin);
                    UC_StackPanel_Matriculas.Children.Add(rectangle);
                    Modelo_2020_07_Escolha escolha = new Modelo_2020_07_Escolha();
                    UC_StackPanel_Matriculas.Children.Add(escolha);
                    escolha.__CICLO = Ciclo.Primaria;
                }

            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = (VisualBrush)this.FindResource("Color.Brush.Pattern.Right");
                    rectangle.Height = (double)new LengthConverter().ConvertFrom("0,2cm");
                    double margin = (double)new LengthConverter().ConvertFrom("0,2cm");
                    rectangle.Margin = new Thickness(0, margin, 0, margin);
                    UC_StackPanel_Matriculas.Children.Add(rectangle);
                    Modelo_2020_07_Escolha escolha = new Modelo_2020_07_Escolha();
                    UC_StackPanel_Matriculas.Children.Add(escolha);
                    if (_Ciclo == Ciclo.Basico)
                        escolha.__CICLO = Ciclo.Basico;
                    
                }
            }
        }
    }
}
