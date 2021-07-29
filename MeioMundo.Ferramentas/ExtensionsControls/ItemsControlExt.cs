using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeioMundo.Ferramentas.ExtensionsControls
{
    public class ItemsControlExt : ItemsControl
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ContentControl();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            // Even wrap other ContentControls
            return false;
        }
    }
}
