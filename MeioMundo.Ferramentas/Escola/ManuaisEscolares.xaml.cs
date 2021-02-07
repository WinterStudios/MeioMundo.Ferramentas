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
    /// Interaction logic for ManuaisEscolares.xaml
    /// </summary>
    public partial class ManuaisEscolares : UserControl, INotifyPropertyChanged
    {
        
        public Internal.Escola Escola { get => _escola; set { _escola = value; NotifyPropertyChanged(); } }
        private Internal.Escola _escola;
        public ObservableCollection<Internal.Escola> Escolas { get => ManuaisSystem.Escolas; }
        
        public Internal.Ano Ano { get; set; }

        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ManuaisEscolares()
        {
            ManuaisSystem.Inicialize();
            InitializeComponent();
        }

        private void UC_ComboBox_Escolas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
