using MeioMundo.Ferramentas.Correio.Internal;
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

namespace MeioMundo.Ferramentas.Correio
{
    /// <summary>
    /// Interaction logic for CartaCreator.xaml
    /// </summary>
    public partial class CartaCreator : UserControl, INotifyPropertyChanged
    {

        public double Zoom { get => _zoom; set { _zoom = value; ApplyZoom(value); OnPropertyChanged(); } }
        private double _zoom;

        public ObservableCollection<CartaMainPage> Paginas { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public CartaCreator()
        {
            Load();
            InitializeComponent();
            
            this.Loaded += CartaCreator_Loaded;
            
        }
        private void Load()
        {
            Paginas = new ObservableCollection<CartaMainPage>();
            Paginas.Add(new CartaMainPage());
        }

        private void CreateNewPage()
        {
            
        }

        private void CartaCreator_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNew();
        }

        void LoadNew()
        {

            double zoomToFit = (viewBox.ViewportWidth - 18) / viewContent.ActualWidth;
            zoomSlider.Value = Math.Round(zoomToFit * 100,0);
        }

        private double ApplyZoom(double d)
        {
            scaleTransform.ScaleX = scaleTransform.ScaleY = d / 100;
            return d;
        }

        private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue > 100)
                Zoom = Math.Round(100 + ((e.NewValue - 100) * 4),0);
            else
                Zoom = Math.Round(e.NewValue, 0);
        }
    }
}
