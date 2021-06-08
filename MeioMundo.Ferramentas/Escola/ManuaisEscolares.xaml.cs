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
        
        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Internal.Escola Escola { get => escola; set { escola = value;

                if (value.Anos.Count < UC_ComboBox_Anos.SelectedIndex)
                    UC_ComboBox_Anos.SelectedItem = value.Anos[UC_ComboBox_Anos.SelectedIndex];
                LoadPreviewModelo();
                NotifyPropertyChanged(); } }
        private Internal.Escola escola;

        public Internal.Ano Ano { get => ano; set { ano = value; LoadPreviewModelo(); } }
        private Internal.Ano ano;
        public ManuaisEscolares()
        {
            ManuaisSystem.Inicialize();
            InitializeComponent();
            LoadDefault();
        }

        private void LoadDefault()
        {
            if (ManuaisSystem.Escolas.Count > 0)
            {
                UC_ComboBox_Escolas.SelectedIndex = 0;
                Escola = (Internal.Escola)UC_ComboBox_Escolas.SelectedItem;
                if(Escola.Anos.Count > 0)
                    Ano = Escola.Anos[0];
                LoadPreviewModelo();
            }
        }

        private void LoadPreviewModelo()
        {
            UC_Viewbox_PreviewModelo.Child = null;
            
            if(Ano == null)
                UC_Viewbox_PreviewModelo.Child = ManuaisSystem.GetModelo((ManuaisSystem.Modelos)UC_ComboBox_Version.SelectedIndex, Escola.Nome, Escola.Anos[0]);
            else
                UC_Viewbox_PreviewModelo.Child = ManuaisSystem.GetModelo((ManuaisSystem.Modelos)UC_ComboBox_Version.SelectedIndex, Escola.Nome, Ano);
        }

        private void Editores_Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();

            if (string.IsNullOrEmpty(tag))
                return;

            if(tag == "__EDITOR_LIVROS")
            {
                Window window = new Window();
                Editor_Livros editor = new Editor_Livros();
                window.Content = editor;
                window.Show();
            }
            if(tag == "__EDITOR_ESCOLAS")
            {
                Window window = new Window();
                Editor_Geral editor = new Editor_Geral();
                window.Content = editor;
                window.Show();
            }
            if(tag == "__PRINT_SINGLE")
            {
                PrintDialog printDialog = new PrintDialog();
                System.Printing.PageMediaSize a4 = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISOA4);
                printDialog.PrintTicket.PageMediaSize = a4;
                printDialog.PrintTicket.Duplexing = System.Printing.Duplexing.OneSided;

                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(UC_Viewbox_PreviewModelo, "My First Print Job");
                }

            }
            if(tag == "__PRINT_MULTIPLE") 
            {
                ManuaisSystem.PrintModelos(ManuaisSystem.Modelos.v_2021_06);
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            scaleTransform.ScaleX = scaleTransform.ScaleY = e.NewValue;
        }
        private void ZoomToFit()
        {
            scaleTransform.ScaleX = scaleTransform.ScaleY = 0;
        }

        private void UC_ComboBox_Version_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UC_Viewbox_PreviewModelo.Child = ManuaisSystem.GetModelo((ManuaisSystem.Modelos)UC_ComboBox_Version.SelectedIndex, Escola.Nome, Ano);
        }
    }
}
