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

namespace MeioMundo.Ferramentas.Escola
{
    /// <summary>
    /// Interaction logic for Editor_Escolas.xaml
    /// </summary>
    public partial class Editor_Geral : UserControl, INotifyPropertyChanged
    {
        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Internal.UC_EditoresGerais EditorMode { get; set; }



        public Internal.Escola? EscolaSelect { get => _escolaSelect; set { _escolaSelect = value; ManuaisSystem.Escolas[_escolaSelect.ID] = value; NotifyPropertyChanged(); } }
        private Internal.Escola? _escolaSelect;
        public Internal.Escola?[] Escolas { get => ManuaisSystem.Escolas.ToArray(); }

        public Editor_Geral()
        {
            EditorMode = Internal.UC_EditoresGerais.Escola;
            InitializeComponent();
            LoadEscolasAnos();

            UC_ListBox_Escolas.ItemsSource = Escolas;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();

            if(tag == "__CREATE_ESCOLA")
            {
                EscolaSelect = ManuaisSystem.AddEscola();
                UC_ListBox_Escolas.ItemsSource = Escolas;
            }
            if (tag == "__SAVE_ESCOLA")
                ManuaisSystem.SaveEscola();
        }

        private void UC_ListBox_Escolas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EscolaSelect = (Internal.Escola)UC_ListBox_Escolas.SelectedItem;
        }
        private void LoadEscolasAnos()
        {
            List<Internal.AnosDictionary> anos = new List<Internal.AnosDictionary>();
            foreach (var item in Internal.Anos.GetAnos())
            {
                Internal.AnosDictionary dictionary = new Internal.AnosDictionary();
                dictionary.ID = item.Key;
                dictionary.Nome = item.Value;
                if (item.Key % 10 == 0)
                    dictionary.IsSelect = false;
                else
                    dictionary.IsSelect = true;
                anos.Add(dictionary);
            }
            UC_ComboBox_Escola_Ano.ItemsSource = anos;
        }
    }
}
