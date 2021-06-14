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
    public partial class UC_PrindDialog : UserControl, INotifyPropertyChanged
    {
        
        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ObservableCollection<PagEncomendasEscolares> Pags { get; set; }

        public PagEncomendasEscolares Encomenda { get => encomenda; set { encomenda = value; NotifyPropertyChanged(); } }
        private PagEncomendasEscolares encomenda;
        public UC_PrindDialog()
        {
            Encomenda = new PagEncomendasEscolares() { Copies = 1 };
            InitializeComponent();
            Pags = new ObservableCollection<PagEncomendasEscolares>();
            UC_ComboBox_PrintSelect_Escola_ToAdd.ItemsSource = ManuaisSystem.GetEscolas();
        }

        private void UC_ComboBox_PrintSelect_Escola_ToAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string tag = comboBox.Tag.ToString();

            if(tag == "__Escla_Select")
            {
                Internal.Escola escola = (Internal.Escola)UC_ComboBox_PrintSelect_Escola_ToAdd.SelectedItem;
                if (escola != null)
                    UC_ComboBox_PrintSelect_Escola_Ano_ToAdd.ItemsSource = escola.Anos;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            if(tag == "__Escola_Add")
            {
                PagEncomendasEscolares encomendaEscolar = Pags.SingleOrDefault(x => x.Escola.ID == Encomenda.Escola.ID && x.SelectAno.ID == Encomenda.SelectAno.ID);
                if (encomendaEscolar != null)
                {
                    encomendaEscolar.Copies++;
                }
                else
                Pags.Add(new PagEncomendasEscolares() { 
                    Escola = Encomenda.Escola,
                    SelectAno = Encomenda.SelectAno, 
                    Copies = Encomenda.Copies, 
                    ID = Pags.Count 
                });
                //Encomenda = new PagEncomendasEscolares();
            }
            if(tag == "__PRINT")
            {
                ManuaisSystem.PrintModelos(ManuaisSystem.Modelos.v_2021_06, Pags.ToArray()); 
            }
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
        public Internal.Ano SelectAno { get => selectAno; set { selectAno = value; NotifyPropertyChanged(); } }
        private Internal.Ano selectAno;
        public int Copies { get => copies; set { copies = value; NotifyPropertyChanged(); } }
        private int copies;
    }
}
