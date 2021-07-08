using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeioMundo.Ferramentas.Barcode
{
    public static class BarcodeExtensions
    {
        public static ImageSource ToImage(this Barcode barcode, int width, int height)
        {
            //RenderTargetBitmap bmp = new RenderTargetBitmap(barcode.Width, barcode.Height, 96, 96, PixelFormats.Default);
            //bmp.Render(barcode.Draw);
            //WriteableBitmap writeable = new WriteableBitmap(bmp);
            return null;
        }
        
    }
}
