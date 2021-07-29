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

using MeioMundo.Ferramentas.Escola.Internal;


namespace MeioMundo.Ferramentas.Escola.Modelos
{
    /// <summary>
    /// Interaction logic for Modelo_2020_07_Escolha.xaml
    /// </summary>
    public partial class Modelo_2020_07_Escolha : UserControl
    {
        public Ciclo __CICLO { get => __ciclo; set { __ciclo = value; UI_UPDATE_CICLO(value); } }
        private Ciclo __ciclo;
        public Modelo_2020_07_Escolha()
        {
            InitializeComponent();
            __CICLO = Ciclo.Secundario;
        }

        private void UI_UPDATE_CICLO(Ciclo ciclo)
        {
            if (ciclo == Ciclo.Basico)
                __ENSINO_BASICO();
            if (ciclo == Ciclo.Primaria)
                __ENSINO_PRIMARIO();
        }
        private void __ENSINO_PRIMARIO()
        {
            UC_BORDER_COMP_ESPEC.Visibility = Visibility.Collapsed;
            UC_BORDER_SPACE.Visibility = Visibility.Collapsed;

            UC_BORDER_COMP_GERAL.Visibility = Visibility.Visible;

            Grid.SetColumnSpan(UC_BORDER_COMP_GERAL, 3);
            
            UC_ROW_HEIGHT_PRIMARIA.Height = new GridLength((double)new LengthConverter().ConvertFrom("1,2cm"));
        }
        private void __ENSINO_BASICO()
        {
            UC_BORDER_COMP_ESPEC.Visibility = Visibility.Collapsed;
            UC_BORDER_SPACE.Visibility = Visibility.Collapsed;

            UC_BORDER_COMP_GERAL.Visibility = Visibility.Visible;

            Grid.SetColumnSpan(UC_BORDER_COMP_GERAL, 3);
        }
        private void __ENSINO_SECUNDARIO()
        {
            UC_BORDER_COMP_ESPEC.Visibility = Visibility.Visible;
            UC_BORDER_SPACE.Visibility = Visibility.Visible;

            UC_BORDER_COMP_GERAL.Visibility = Visibility.Visible;
        }
    }
}
