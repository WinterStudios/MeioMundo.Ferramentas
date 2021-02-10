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

namespace MeioMundo.Ferramentas.Escola
{
    /// <summary>
    /// Interaction logic for Editor_Escolas.xaml
    /// </summary>
    public partial class Editor_Geral : UserControl
    {
        public Internal.UC_EditoresGerais EditorMode { get; set; }



        public Internal.Escola? EscolaSelect { get; set; }

        public Editor_Geral()
        {
            EditorMode = Internal.UC_EditoresGerais.Escola;
            InitializeComponent();

        }

        private void UC_ComboBox_EditorMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            EditorMode = (Internal.UC_EditoresGerais)UC_ComboBox_EditorMode.SelectedItem;

            if(EditorMode == Internal.UC_EditoresGerais.Ano)
            {
                UC_Grid_Ano.Visibility = Visibility.Visible;
                UC_Grid_Escolas.Visibility = Visibility.Collapsed;
            }
            if(EditorMode == Internal.UC_EditoresGerais.Escola)
            {
                UC_Grid_Ano.Visibility = Visibility.Collapsed;
                UC_Grid_Escolas.Visibility = Visibility.Visible;
            }
        }
    }
}
