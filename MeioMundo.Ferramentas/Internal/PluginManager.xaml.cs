using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeioMundo.Ferramentas.Internal
{
    /// <summary>
    /// Interaction logic for PluginManager.xaml
    /// </summary>
    public partial class PluginManager : UserControl
    {
        public PluginManager()
        {
            InitializeComponent();
        }
        private void Nav_Button_Click(object sender, RoutedEventArgs e)
        {
            int windowIndex = int.Parse(((Button)sender).Tag.ToString());
            double d = scroll.ActualWidth * windowIndex;
            Scroll(d);
        }

        private void Scroll(double d)
        {
            Storyboard story = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = scroll.HorizontalOffset;
            animation.To = d;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            story.Children.Add(animation);
            Storyboard.SetTarget(animation, scroll);
            Storyboard.SetTargetProperty(animation, new PropertyPath(CustomScrollViewer.CurrentHorizontalOffsetProperty));
            
            story.Begin();
        }
    }
}
