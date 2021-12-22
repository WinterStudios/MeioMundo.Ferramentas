using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MeioMundo.Ferramentas.Extensions
{
    public static class ChildHelper
    {
        /// <summary>
        /// Disconnects <paramref name="child"/> from it's parent if any.
        /// </summary>
        /// <param name="child"></param>
        public static void DisconnectIt(this FrameworkElement child)
        {
            var parent = child.Parent;
            if (parent == null)
                return;

            if (parent is Panel panel)
            {
                panel.Children.Remove(child);
                return;
            }

            if (parent is Decorator decorator)
            {
                if (decorator.Child == child)
                    decorator.Child = null;

                return;
            }

            if (parent is ContentPresenter contentPresenter)
            {
                if (contentPresenter.Content == child)
                    contentPresenter.Content = null;
                return;
            }

            if (parent is ContentControl contentControl)
            {
                if (contentControl.Content == child)
                    contentControl.Content = null;
                return;
            }
            if (parent.GetType() == typeof(FixedPage))
            {
                FixedPage page = (FixedPage)parent;
                page.Children.Remove(child);
                return;
            }
            //if (parent is ItemsControl itemsControl)
            //{
            //  itemsControl.Items.Remove(child);
            //  return;
            //}

        }
        public static DependencyObject GetParent(this DependencyObject depObj, bool isVisualTree)
        {
            if (isVisualTree)
            {
                if (depObj is Visual || depObj is Visual3D)
                    return VisualTreeHelper.GetParent(depObj);
                return null;
            }
            else
                return LogicalTreeHelper.GetParent(depObj);
        }

        /// <summary>
        /// Adds or inserts a child back into its parent
        /// </summary>
        /// <param name="child"></param>
        /// <param name="index"></param>
        public static void AddToParent(this UIElement child, DependencyObject parent, int? index = null)
        {
            if (parent == null)
                return;

            if (parent is ItemsControl itemsControl)
                if (index == null)
                    itemsControl.Items.Add(child);
                else
                    itemsControl.Items.Insert(index.Value, child);
            else if (parent is Panel panel)
                if (index == null)
                    panel.Children.Add(child);
                else
                    panel.Children.Insert(index.Value, child);
            else if (parent is Decorator decorator)
                decorator.Child = child;
            else if (parent is ContentPresenter contentPresenter)
                contentPresenter.Content = child;
            else if (parent is ContentControl contentControl)
                contentControl.Content = child;
        }
    }
}
