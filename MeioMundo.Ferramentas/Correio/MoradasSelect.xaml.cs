using MeioMundo.Ferramentas.Internal;
using System;
using System.Collections.Generic;
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

namespace MeioMundo.Ferramentas.Correio
{
    /// <summary>
    /// Interaction logic for FornecedorSelect.xaml
    /// </summary>
    public partial class MoradasSelect : UserControl, INotifyPropertyChanged
    {

        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public People Person { get => person; set { person = value; NotifyPropertyChanged(); } }
        private People person;

        public Morada Morada { get => morada; set { morada = value; NotifyPropertyChanged(); } }
        private Morada morada;
        public MoradasSelect()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            if(tag == "__RESULT")
            {
                ((Window)this.Parent).DialogResult = true;
            }
        }
    }
}
