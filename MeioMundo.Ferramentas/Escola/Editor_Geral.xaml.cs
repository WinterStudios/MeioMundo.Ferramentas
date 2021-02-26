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

        public Internal.Ano Ano { get => ano; set { ano = value; NotifyPropertyChanged(); } }
        private Internal.Ano ano;


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
        private void AddAnoToEscola()
        {
            if (Escola == null || UC_ComboBox_Escola_Ano.SelectedItem == null)
                return;
            
            Internal.AnoUX anoUX = (Internal.AnoUX)UC_ComboBox_Escola_Ano.SelectedItem;

            Internal.Ano ano = new Internal.Ano();
            ano.ID = anoUX.ID;
            ano.Disciplinas = new Internal.Disciplina[0];

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
    }
}
