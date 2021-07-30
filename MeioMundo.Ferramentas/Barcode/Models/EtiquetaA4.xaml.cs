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
        public ObservableCollection<ObservableCollection<IEtiqueta>> Etiquetas { get; set; }
        public EtiquetaA4()
        {
            Etiquetas = new ObservableCollection<ObservableCollection<IEtiqueta>>();
            InitializeComponent();
        }


        internal void AddEtiquetas(IEtiqueta etiqueta)
        {
            if(Etiquetas.Count == 0)
            {
                ObservableCollection<IEtiqueta> etiquetaY = new ObservableCollection<IEtiqueta>();
                etiquetaY.Add(etiqueta);
                Etiquetas.Add(etiquetaY);
                return;
            }

            for (int y = 0; y < Etiquetas.Count; y++)
            {
                ObservableCollection<IEtiqueta> etiquetasY = Etiquetas[y];
                int maxX = 2;
                //if(etiquetasY.Count < 1)
                   
                for (int x = 0; x < etiquetasY.Count; x++)
                {
                    IEtiqueta etiquetaX = etiquetasY[x];

                }
            }
        }

    }
}
