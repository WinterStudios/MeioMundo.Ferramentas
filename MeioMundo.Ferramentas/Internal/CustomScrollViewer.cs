using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MeioMundo.Ferramentas.Internal
{
    public class CustomScrollViewer: ScrollViewer
    {
        //Register a DependencyProperty which has a onChange callback
        public static DependencyProperty CurrentVerticalOffsetProperty = DependencyProperty.Register("CurrentVerticalOffset", typeof(double), typeof(CustomScrollViewer), new PropertyMetadata(new PropertyChangedCallback(OnVerticalChanged)));
        public static DependencyProperty CurrentHorizontalOffsetProperty = DependencyProperty.Register("CurrentHorizontalOffsetOffset", typeof(double), typeof(CustomScrollViewer), new PropertyMetadata(new PropertyChangedCallback(OnHorizontalChanged)));

        Storyboard story = new Storyboard();
        DoubleAnimation animation = new DoubleAnimation();
        public CustomScrollViewer()
        {
            story.Children.Add(animation);
            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, new PropertyPath(CustomScrollViewer.CurrentVerticalOffsetProperty));
        }

        //When the DependencyProperty is changed change the vertical offset, thus 'animating' the scrollViewer
        private static void OnVerticalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomScrollViewer viewer = (CustomScrollViewer)d;
            viewer.ScrollToVerticalOffset((double)e.NewValue);
        }

        //When the DependencyProperty is changed change the vertical offset, thus 'animating' the scrollViewer
        private static void OnHorizontalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomScrollViewer viewer = (CustomScrollViewer)d;
            viewer.ScrollToHorizontalOffset((double)e.NewValue);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //base.OnMouseWheel(e);
            double to = e.Delta * -1d;
            if(to != double.NaN)
                Scroll(to);
        }

        private void Scroll(double d)
        {
            if (double.IsNaN(d))
                return;
            animation.From = this.VerticalOffset;
            animation.To = CurrentVerticalOffset + d;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            animation.EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut };
            story.Begin();
        }

        public double CurrentHorizontalOffset
        {
            get { return (double)this.GetValue(CurrentHorizontalOffsetProperty); }
            set { this.SetValue(CurrentHorizontalOffsetProperty, value); }
        }

        public double CurrentVerticalOffset
        {
            get { return (double)this.GetValue(CurrentVerticalOffsetProperty); }
            set { this.SetValue(CurrentVerticalOffsetProperty, value); }
        }

    }
}
