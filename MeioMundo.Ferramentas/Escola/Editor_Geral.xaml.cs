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



        public Editor_Geral()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
