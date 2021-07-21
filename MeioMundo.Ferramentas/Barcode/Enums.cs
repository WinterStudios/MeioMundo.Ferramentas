using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Barcode
{
    public enum DisplayCodeType
    {
        None = 0,
        Center = 1,
        PerChar = 2
    }
    public enum BarcodeImageResolution
    {
        VeryLow = 0,
        Low = 1,
        MediumLow = 2,
        Medium = 3,
        MediumHigh = 4,
        High = 5,
        VeryHigh = 6,
        Extreme = 7
    }
    public enum BarcodeHeight
    {
        Minimal = 0,
        Low = 1,
        Normal = 2,
        High = 3,
        VeryHigh = 4,
        Custom = 10
    }
    public enum BarType
    {
        Code39 = 0,
        EAN13 = 1,
    }
}
