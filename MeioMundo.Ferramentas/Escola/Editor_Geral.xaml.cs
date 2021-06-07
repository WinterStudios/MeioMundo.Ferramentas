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


        public Internal.Editores EditorMode { get => editorMode; 
            set {
                if(value == Internal.Editores.Escola)
                {
                    UC_Grid_Escolas.Visibility = Visibility.Visible;
                    UC_Grid_Disciplinas.Visibility = Visibility.Collapsed;
                }
                if(value == Internal.Editores.Ano)
                {
                    UC_Grid_Disciplinas.Visibility = Visibility.Visible;
                    UC_Grid_Escolas.Visibility = Visibility.Collapsed;
                }
                editorMode = value;
            } }
        private Internal.Editores editorMode;


        public Internal.Escola Escola { get => escola; set { escola = value; CheckAnoAdd(); NotifyPropertyChanged(); } }
        private Internal.Escola escola;

        public Internal.Ano Ano { get => ano; set { ano = value; FilterLivros(); NotifyPropertyChanged(); } }
        private Internal.Ano ano;

        public Internal.Livro[] Livros { get => livros; set { livros = value; NotifyPropertyChanged(); } }
        private Internal.Livro[] livros;

        public Editor_Geral()
        {
            InitializeComponent();
            UC_ListBox_Escolas.ItemsSource = ManuaisSystem.Escolas;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();

            if(tag == "__CREATE_ESCOLA")
            {
                ManuaisSystem.AddEscola();
                UC_ListBox_Escolas.Items.Refresh();
            }
            if (tag == "__SAVE_ESCOLA")
                ManuaisSystem.SaveEscola();

            if (tag == "__ESCOLA_ANO_ADD")
                AddAnoToEscola();
        }
        private void CheckAnoAdd()
        {
            if (!this.IsLoaded)
                return;
            foreach (Internal.AnoUX item in UC_ComboBox_Escola_Ano.Items)
            {
                item.IsEnable = false;
            }

            for (int i = 0; i < Escola.Anos.Count; i++)
            {
                foreach (Internal.AnoUX item in UC_ComboBox_Escola_Ano.Items)
                {
                    if (Escola.Anos[i].ID == item.ID)
                        item.IsEnable = true;
                }
            }
            UC_ComboBox_Escola_Ano.Items.Refresh();


        }

        private void FilterLivros(int disciplinaID = 0)
        {
            if (EditorMode != Internal.Editores.Ano)
                return;

            if (Escola == null || Ano == null)
                return;

            int ciclo = Ano.ID - (Ano.ID % 10);
            List<Internal.Livro> _livros = new List<Internal.Livro>();
            _livros.AddRange(ManuaisSystem.Livros.Where(x => x.Ano == ciclo).OrderBy(x => x.Nome));
            if (disciplinaID == 0)
                _livros.AddRange(ManuaisSystem.Livros.Where(x => x.Ano == Ano.ID).OrderBy(x => x.Nome));
            else
                _livros.AddRange(ManuaisSystem.Livros.Where(x => x.Ano == Ano.ID && x.Disciplina == disciplinaID).OrderBy(x => x.Nome));

            Livros = _livros.OrderBy(x => x.Nome).ToArray();
        }

        private void AddAnoToEscola()
        {
            if (Escola == null || UC_ComboBox_Escola_Ano.SelectedItem == null)
                return;
            
            Internal.AnoUX anoUX = (Internal.AnoUX)UC_ComboBox_Escola_Ano.SelectedItem;

            Internal.Ano ano = new Internal.Ano();
            ano.ID = anoUX.ID;
            ano.Disciplinas = new List<Internal.Disciplina>();

            Escola.Anos.Add(ano);

            //anoUX.IsEnable = true;
            UC_ComboBox_Escola_Ano.Items.Refresh();
            if(UC_ComboBox_Escola_Ano.SelectedIndex < UC_ComboBox_Escola_Ano.Items.Count)
            {
                UC_ComboBox_Escola_Ano.SelectedIndex++;
                if (((Internal.AnoUX)UC_ComboBox_Escola_Ano.SelectedItem).ID % 10 == 0)
                    UC_ComboBox_Escola_Ano.SelectedIndex++;
            }
                
            UC_ListBox_Escolas_Anos.Items.Refresh();
        }

        private void UC_ListBox_AllDisciplinas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int disciplinaID = ((Internal.DisciplinasUX)UC_ListBox_AllDisciplinas.SelectedItem).ID;

            Internal.Disciplina disciplina = new Internal.Disciplina();
            disciplina.ID = disciplinaID;

            Ano.Disciplinas.Add(disciplina);
            UC_ListBox_EscolaAnoDisciplinas.Items.Refresh();
        }


        private void UC_Grid_Disciplina_DataGrid_DisciplinaEditor_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Internal.Disciplina _disciplina = (Internal.Disciplina)UC_Grid_Disciplina_DataGrid_DisciplinaEditor.SelectedItem;

            if (_disciplina != null)
                FilterLivros(_disciplina.ID);
            else
                FilterLivros();
        }

        private void UC_Grid_Disciplina_ListBox_Livros_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Internal.Livro _livro = (Internal.Livro)UC_Grid_Disciplina_ListBox_Livros.SelectedItem;

            Internal.Disciplina _disciplina = (Internal.Disciplina)UC_Grid_Disciplina_DataGrid_DisciplinaEditor.SelectedItem;

            _disciplina.Livro_ISBN = _livro.ISBN;
            UC_Grid_Disciplina_DataGrid_DisciplinaEditor.Items.Refresh();
        }

        private void UC_ListBox_EscolaAnoDisciplinas_Remove_BTN(object sender, RoutedEventArgs e)
        {
            Internal.Disciplina _disciplina = (Internal.Disciplina)((Button)sender).DataContext;
            Ano.Disciplinas.Remove(_disciplina);
            UC_ListBox_EscolaAnoDisciplinas.Items.Refresh();
        }

        private void UC_ListBox_Escolas_Anos_Remove_BTN(object sender, RoutedEventArgs e)
        {
            Internal.Ano _ano = (Internal.Ano)((Button)sender).DataContext;
            Escola.Anos.Remove(_ano);
            UC_ListBox_Escolas_Anos.Items.Refresh();
        }
    }
}
