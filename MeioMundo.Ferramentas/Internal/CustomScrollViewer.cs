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
        private bool animationFinish = false;
        public CustomScrollViewer()
        {
            animation.Completed += (sender, arg) => { animationFinish = true; };
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

        private double ToVertical = 0;
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //base.OnMouseWheel(e);
            ToVertical = e.Delta * -1d;
            if(!animation.IsFrozen)
                Scroll(ToVertical);
                
        }

        private void Scroll(double d)
        {
            story.Stop();
            if (double.IsNaN(d))
                return;
            if (CurrentVerticalOffset + d > this.ScrollableHeight)
                animation.To = this.ScrollableHeight;
            else
                animation.To = CurrentVerticalOffset + d;

            animation.From = this.VerticalOffset;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
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
