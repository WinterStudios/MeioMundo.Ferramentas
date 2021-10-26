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
    /// Interaction logic for Editor_Livros.xaml
    /// </summary>
    public partial class Editor_Livros : UserControl, INotifyPropertyChanged
    {
        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Internal.Livro Livro { get => livro; set { livro = value; NotifyPropertyChanged(); } }
        private Internal.Livro livro;
        public Editor_Livros()
        {
            InitializeComponent();
            UC_DataGrid_Livros.RowEditEnding += UC_DataGrid_Livros_RowEditEnding;
            UC_DataGrid_Livros.CellEditEnding += UC_DataGrid_Livros_CellEditEnding;
        }

        private void UC_DataGrid_Livros_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ManuaisSystem.SaveLivros();

        }

        private void UC_DataGrid_Livros_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            ManuaisSystem.SaveLivros();
        }

        private void UC_DataGrid_Livros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
