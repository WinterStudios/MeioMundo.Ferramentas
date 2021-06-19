using System;
using System.Collections.Generic;
using System.Linq;
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
            //Window window = new Window();
            Viewbox viewbox = new Viewbox();
            //window.Content = viewbox;
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
                Modelo.VoucherModelo_Front front = new Modelo.VoucherModelo_Front();
                viewboxVoucher.Child = front;
                stackPanel.Children.Add(viewboxVoucher);
            }
            

            PrintDialog printDialog = new PrintDialog();
            System.Printing.PageMediaSize a4 = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISOA4);
            printDialog.PrintTicket.PageMediaSize = a4;

            if (printDialog.ShowDialog() == true)
            {
                var document = new FixedDocument();
                document.DocumentPaginator.PageSize = new Size(
                    (double)new LengthConverter().ConvertFromString("21cm"),
                    (double)new LengthConverter().ConvertFromString("29,7cm")
                    );

                Size pageSize = new Size(control.Width, control.Height);

                FixedPage fixedPage = new FixedPage();
                fixedPage.Width = pageSize.Width;
                fixedPage.Height = pageSize.Height;

                fixedPage.Children.Add((UIElement)control);
                fixedPage.Measure(pageSize);
                fixedPage.Arrange(new Rect(new Point(), pageSize));
                fixedPage.UpdateLayout();
                // Add page to document
                var pageContent = new PageContent();
                ((IAddChild)pageContent).AddChild(fixedPage);
                document.Pages.Add(pageContent);
                printDialog.PrintDocument(document.DocumentPaginator, "Mod.2021 - Encomendas Escolares");
            }
        }
    }
}
