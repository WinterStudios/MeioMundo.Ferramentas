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


        public Internal.Escola?[] Escolas { get => ManuaisSystem.Escolas.ToArray(); }


        public Internal.Escola? EscolaSelect { get => _escolaSelect; set { _escolaSelect = value; ManuaisSystem.Escolas[_escolaSelect.ID] = value; NotifyPropertyChanged(); } }
        private Internal.Escola? _escolaSelect;

        public Internal.Ano? AnoSelect { get => _anoSelect; set { _anoSelect = value; NotifyPropertyChanged(); } }
        private Internal.Ano? _anoSelect;

        public Editor_Geral()
        {
            EditorMode = Internal.UC_EditoresGerais.Escola;
            InitializeComponent();
            //LoadEscolasAnos();

            UC_ListBox_Escolas.ItemsSource = Escolas; 
            LoadDisciplinas();
        }

        private void UC_ComboBox_EditorMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            EditorMode = (Internal.UC_EditoresGerais)UC_ComboBox_EditorMode.SelectedItem;

            if(EditorMode == Internal.UC_EditoresGerais.Disciplina)
            {
                UC_Grid_Disciplinas.Visibility = Visibility.Visible;
                UC_Grid_Escolas.Visibility = Visibility.Collapsed;
            }
            if(EditorMode == Internal.UC_EditoresGerais.Escola)
            {
                UC_Grid_Disciplinas.Visibility = Visibility.Collapsed;
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

            if (tag == "__ESCOLA_ANO_ADD")
                AddAnoToEscola();
        }

        private void UC_ListBox_Escolas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;
            EscolaSelect = (Internal.Escola)UC_ListBox_Escolas.SelectedItem;
            UC_ListBox_Escolas_Anos.ItemsSource = EscolaSelect.Anos;
            LoadEscolasAnos();
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
                {
                    dictionary.IsSelect = false;
                    dictionary.IsCiclo = true;
                }
                else
                    dictionary.IsSelect = true;
                for (int i = 0; i < EscolaSelect.Anos.Count; i++)
                {
                    if (EscolaSelect.Anos[i].ID == item.Key)
                    {
                        dictionary.IsSelect = false;
                        break;
                    }
                }

                anos.Add(dictionary);
            }
            UC_ComboBox_Escola_Ano.ItemsSource = anos;
        }

        private void AddAnoToEscola()
        {
            Internal.Ano ano = new Internal.Ano();

            Internal.AnosDictionary _anoDictionary = (Internal.AnosDictionary)UC_ComboBox_Escola_Ano.SelectedItem;
            ano.ID = _anoDictionary.ID;
            ano.Name = _anoDictionary.Nome;
            ano.Disciplinas = new List<Internal.Disciplina>();
            _anoDictionary.IsSelect = false;
            EscolaSelect.Anos.Add(ano);
            EscolaSelect.Anos = EscolaSelect.Anos.OrderBy(x => x.ID).ToList();
            UC_ListBox_Escolas_Anos.ItemsSource = EscolaSelect.Anos;
        }

        private void LoadDisciplinas() 
        {
            List<Internal.DisciplinasDictionary> disciplinas = new List<Internal.DisciplinasDictionary>();
            foreach (var item in Internal.Disciplinas.GetDisciplinas())
            {
                Internal.DisciplinasDictionary dictionary = new Internal.DisciplinasDictionary();
                dictionary.ID = item.Key;
                dictionary.Nome = item.Value;
                //if (item.Key % 10 == 0)
                //{
                //    dictionary.IsSelect = false;
                //    dictionary.IsCiclo = true;
                //}
                //else
                //    dictionary.IsSelect = true;
                //for (int i = 0; i < EscolaSelect.Anos.Count; i++)
                //{
                //    if (EscolaSelect.Anos[i].ID == item.Key)
                //    {
                //        dictionary.IsSelect = false;
                //        break;
                //    }
                //}

                disciplinas.Add(dictionary);
            }
            UC_ListBox_AllDisciplinas.ItemsSource = disciplinas;
        }

        private void UC_ListBox_Escolas_Anos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded || UC_ListBox_Escolas_Anos.SelectedIndex < 0)
                return;

            AnoSelect = (Internal.Ano)UC_ListBox_Escolas_Anos.SelectedItem;
            UC_ListBox_EscolaAnoDisciplinas.ItemsSource = AnoSelect.Disciplinas;

        }

        private void UC_LisbBox_AllDisciplinas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            

            ListBox parent = (ListBox)sender;
            Internal.DisciplinasDictionary dictionary = (Internal.DisciplinasDictionary)parent.SelectedItem;
            AddDisciplinaToAno(dictionary);

            
        }
        private void AddDisciplinaToAno(Internal.DisciplinasDictionary dictionary = null)
        {
            Internal.Disciplina disciplina = new Internal.Disciplina();

            disciplina.ID = dictionary.ID;
            disciplina.Nome = dictionary.Nome;
            disciplina.Livros = new List<Internal.Livro>();

            AnoSelect.Disciplinas.Add(disciplina);

            UC_ListBox_EscolaAnoDisciplinas.Items.Refresh();

            //EscolaSelect.Anos
            //EscolaSelect.Anos = EscolaSelect.Anos.OrderBy(x => x.ID).ToList();
            //UC_ListBox_Escolas_Anos.ItemsSource = EscolaSelect.Anos;
        }
    }
}
