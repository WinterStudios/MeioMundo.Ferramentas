using MeioMundo.Ferramentas.Voucher.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace MeioMundo.Ferramentas.Voucher
{
    public class VoucherSystem
    {
        public static void PrintTest(int value)
        {

            UserControl[] controls = new UserControl[2];
            {
                controls[0] = GetFront(value);
                controls[1] = GetBack(value, 0);
            }
            
            MeioMundo.Ferramentas.Internal.PrintSystem.Print(controls);
        }

        private static UserControl GetFront(int price)
        {
            UserControl control = new UserControl();
            control.Width = (double)new LengthConverter().ConvertFromString("21cm");
            control.Height = (double)new LengthConverter().ConvertFromString("29,7cm");
            control.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //viewbox.Child = control;

            Border border = new Border() { Padding = new Thickness((double)new LengthConverter().ConvertFromString("0,5cm")) };

            control.Content = border;

            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical };
            border.Child = stackPanel;
            int rep = 3;
            for (int i = 0; i < rep; i++)
            {
                Viewbox viewboxVoucher = new Viewbox();
                Modelo.VoucherModelo_Front front = new Modelo.VoucherModelo_Front(price);
                viewboxVoucher.Child = front;
                
                stackPanel.Children.Add(viewboxVoucher);
            }
            return control;
        }
        private static UserControl GetBack(int price, int serialNumber)
        {
            UserControl control = new UserControl();
            control.Width = (double)new LengthConverter().ConvertFromString("21cm");
            control.Height = (double)new LengthConverter().ConvertFromString("29,7cm");
            control.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //viewbox.Child = control;

            Border border = new Border() { Padding = new Thickness((double)new LengthConverter().ConvertFromString("0,5cm")) };

            control.Content = border;

            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical };
            border.Child = stackPanel;
            int rep = 3;
            for (int i = 0; i < rep; i++)
            {
                Viewbox viewboxVoucher = new Viewbox();
                Modelo.VoucherModelo_Back back = new Modelo.VoucherModelo_Back();
                viewboxVoucher.Child = back;

                stackPanel.Children.Add(viewboxVoucher);
            }
            return control;
        }
        internal static UIElement GetVoucherPreview(int price, int serialNumber)
        {
            StackPanel stack = new StackPanel();

            Modelo.VoucherModelo_Front front = new Modelo.VoucherModelo_Front(price);
            
            Modelo.VoucherModelo_Back back = new Modelo.VoucherModelo_Back(price, serialNumber);
            
            stack.Orientation = Orientation.Vertical;
            stack.Children.Add(front);
            stack.Children.Add(back);

            return (UIElement)stack;
        }
        internal static void SetGiftCardExit()
        {

        }
    }
}
