using MeioMundo.Ferramentas.Escola.Internal;
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
    /// Interaction logic for UC_PrindDialog.xaml
    /// </summary>
    public partial class UC_PrindDialog : UserControl
    {
        public ObservableCollection<PagEncomendasEscolares> Pags { get; set; }
        public ObservableCollection<Internal.Escola> Escolas { get; set; }
        public UC_PrindDialog()
        {
            InitializeComponent();
            DataContext = Pags;
            Pags = new ObservableCollection<PagEncomendasEscolares>();
            Escolas = new ObservableCollection<Internal.Escola>(ManuaisSystem.Escolas.ToList());
            gridPrint.AddingNewItem += GridPrint_AddingNewItem;
        }

        private void GridPrint_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }
    }

    public class PagEncomendasEscolares : INotifyPropertyChanged
    {
        
        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public int ID { get; set; }
        public Internal.Escola Escola { get => escola; set { escola = value; NotifyPropertyChanged(); } }
        private Internal.Escola escola;
        public Internal.Ano SelectAno { get; set; }
        public int Copies { get; set; }
    }
}
