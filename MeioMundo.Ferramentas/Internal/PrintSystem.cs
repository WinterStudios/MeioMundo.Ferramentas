using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace MeioMundo.Ferramentas.Internal
{
    public static class PrintSystem
    {
        /// <summary>
        /// Print the controls list
        /// </summary>
        /// <param name="controls">Visuals to Print</param>
        /// <param name="paperSize">The Size of the Paper</param>
        /// <param name="printJobName">Job Description</param>
        /// <remarks>Waring!: Only print paper size of "A4"</remarks>
        /// 
        public static void Print(UserControl[] controls, PaperSize paperSize = PaperSize.A4, string printJobName = "A Imprimir Trabalhos Meio Mundo")
        {
            PrintDialog printDialog = new PrintDialog();
            Paper paper = new Paper(paperSize);

            System.Printing.PageMediaSize a4 = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISOA4);
            printDialog.PrintTicket.PageMediaSize = a4;

            if (printDialog.ShowDialog() == true)
            {
                var document = new FixedDocument();
                document.DocumentPaginator.PageSize = new Size(
                    (double)new LengthConverter().ConvertFromString("21cm"),
                    (double)new LengthConverter().ConvertFromString("29,7cm")
                    );

                

                for (int pages = 0; pages < controls.Length; pages++)
                {
                    Size pageSize = new Size(controls[pages].Width, controls[pages].Height);
                    FixedPage fixedPage = new FixedPage();
                    fixedPage.Width = pageSize.Width;
                    fixedPage.Height = pageSize.Height;

                    fixedPage.Children.Add((UIElement)controls[pages]);
                    fixedPage.Measure(pageSize);
                    fixedPage.Arrange(new Rect(new Point(), pageSize));
                    fixedPage.UpdateLayout();

                    // Add page to document
                    var pageContent = new PageContent();
                    ((IAddChild)pageContent).AddChild(fixedPage);
                    document.Pages.Add(pageContent);
                }
                
                printDialog.PrintDocument(document.DocumentPaginator, printJobName);
            }
        }

    }
    /// <summary>
    /// Generic Zone for print almost everting
    /// 
    /// EM DESENVOLVIMENTO
    /// </summary>

    public enum PaperSize
    {
        A4 = 0
    }
    public class Paper
    {
        public PaperSize PaperSize { get; set; }
        public double Width { get => GetSize(PaperSize).Width; }
        public double Height { get => GetSize(PaperSize).Height; }

        public Paper() { }

        public Paper(PaperSize size)
        {
            PaperSize = size;
        }

        private static Size GetSize(PaperSize size)
        {
            Size _paperSize = new Size();
            switch (size)
            {
                case Internal.PaperSize.A4:
                    _paperSize.Width = (double)new LengthConverter().ConvertFromString("21cm");
                    _paperSize.Height = (double)new LengthConverter().ConvertFromString("29,7cm");
                    break;
                default:
                    break;
            }
            return _paperSize;
        }
    }
}
