using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MeioMundo.Ferramentas.Barcode
{
    public interface IBarCode
    {
        string Code { get; set; }
        DisplayCodeType DisplayCodeType { get; set; }
        BarcodeImageResolution BarcodeImageResolution { get; set; }
        BarType BarType { get; }
        BarcodeHeight BarcodeHeight { get; set; }
        char[] Chars { get; }        
        BitmapSource CodeImage { get; set; }
        void Draw();
        string CheckCode(string s);
    }
}
