using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeioMundo.Ferramentas.Barcode
{
    public class Barcode
    {
        public string Code { get; set; }

        internal DrawingVisual Draw { get; set; }

        internal float Resolution { get; private set; }
        internal int Width { get; private set; }
        public int Height { get; private set; }

        public Barcode()
        {

        }

        internal static Barcode CreateBarcode(string v, BarcodeEncoding encoding)
        {
            Barcode barcode = new Barcode();
            barcode.Resolution = 4f;
            int width = 0;
            int height = 0;
            switch (encoding)
            {
                case BarcodeEncoding.Code39:
                    barcode.Code = v;
                    barcode.Draw = Internal.Code39.Draw(v, barcode.Resolution, out width, out height);
                    break;
                default:
                    break;
            }
            barcode.Width = width;
            barcode.Height = height;

            return barcode;
        }
    }
}
