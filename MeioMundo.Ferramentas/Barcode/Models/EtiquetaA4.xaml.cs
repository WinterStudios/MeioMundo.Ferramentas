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

        private int MaxY { get; set; }

        public EtiquetaA4()
        {
            InitializeComponent();
            //Etiquetas.CollectionChanged += Etiquetas_CollectionChanged;
            MaxY = 5;
            //for (int i = 0; i < 5; i++)
            //{
            //    UC_StackList.Children.Add(new Rectangle() { Width = 30, Height = 20, Fill = Brushes.Gray });

            //}
        }

        internal void LoadEtiquetas(IEtiqueta[] etiquetas)
        {   
            //Etiquetas = new ObservableCollection<IEtiqueta>(etiquetas);
            

        }

        internal void UpdateList()
        {
            int m_lines = Etiquetas.Count / MaxY + 1;
            UC_StackList.Children.Clear();
            for (int i = 0; i < m_lines; i++)
            {
                StackPanel stackHorizontal = new StackPanel();
                stackHorizontal.Orientation = Orientation.Horizontal;
                var m_etiquetasX = Etiquetas.Skip(i * MaxY).Take(MaxY).ToList();
                for (int x = 0; x < m_etiquetasX.Count; x++)
                {
                    Type m_EtiquetaType = m_etiquetasX[x].GetType();
                    UserControl m_etiqueta = (UserControl)Activator.CreateInstance(m_EtiquetaType, m_etiquetasX[x]);
                    stackHorizontal.Children.Add(m_etiqueta);
                }
                UC_StackList.Children.Add(stackHorizontal);

            }
        }
    }
}
