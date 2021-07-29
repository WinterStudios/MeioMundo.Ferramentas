using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace MeioMundo.Ferramentas.Barcode.Models
{
    /// <summary>
    /// Interaction logic for EtiquetaA4.xaml
    /// </summary>
    public partial class EtiquetaA4 : UserControl
    {
        public ObservableCollection<IEtiqueta> Etiquetas { get; set; }
        public EtiquetaA4()
        {
            Etiquetas = new ObservableCollection<IEtiqueta>();
            Etiquetas.CollectionChanged += Etiquetas_CollectionChanged;
            InitializeComponent();
        }

        private void Etiquetas_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {

            }
        }

        private void AddEtiquetas()
        {

        }

    }
}
