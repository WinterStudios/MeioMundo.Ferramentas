﻿using MeioMundo.Ferramentas.Barcode.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeioMundo.Ferramentas.Barcode
{
    public class Barcode
    {
        public static BitmapSource GetBarcodeToImage(string code, BarType type, BarcodeImageResolution resolution = BarcodeImageResolution.MediumLow, BarcodeHeight height = BarcodeHeight.Normal, DisplayCodeType displayText = DisplayCodeType.Center)
        {
            switch (type)
            {
                case BarType.Code39:
                    Code39 m_code = new Code39();
                    m_code.BarcodeHeight = height;
                    m_code.BarcodeImageResolution = resolution;
                    m_code.DisplayCodeType = displayText;
                    m_code.Code = code;
                    return m_code.CodeImage;
                case BarType.EAN13:
                    return null;
                default:
                    return null;
            }
        }
    }
}
    //public class Barcode : IBarCode
    
        //public string Code { get; set; }

        //internal DrawingVisual Draw { get; set; }

        //internal float Resolution { get; private set; }
        //internal int Width { get; private set; }
        //public int Height { get; private set; }

        //public Barcode()
        //{

        //}

        //internal static Barcode CreateBarcode(string v, BarcodeEncoding encoding)
        //{
        //    Barcode barcode = new Barcode();
        //    barcode.Resolution = 4f;
        //    int width = 0;
        //    int height = 0;
        //    switch (encoding)
        //    {
        //        case BarcodeEncoding.Code39:
        //            barcode.Code = v;
        //            //barcode.Draw = Internal.Code39.Draw(v, barcode.Resolution, out width, out height);
        //            break;
        //        default:
        //            break;
        //    }
        //    barcode.Width = width;
        //    barcode.Height = height;

        //    return barcode;
        //}
        //internal static BitmapSource CreateBarcodeToImage(string code, BarcodeEncoding encoding, int resolution = 300, bool text = true)
        //{
        //    Barcode barcode = new Barcode();
        //    int width = 900;
        //    int height = 200;
        //    switch (encoding)
        //    {
        //        case BarcodeEncoding.Code39:
        //            barcode.Code = code;
        //            barcode.Draw = Internal.Code39.Draw(code, 6, text);
        //            break;
        //        case BarcodeEncoding.EAN13:
        //            barcode.Code = code;
        //            //barcode.Draw = Internal.EAN13.Draw(code, resolution, text);
        //            break;
        //        default:
        //            break;
        //    }
        //    width = (int)Math.Round(barcode.Draw.ContentBounds.Width);
        //    //barcode.Height = 20;

        //    var image = new RenderTargetBitmap((int)barcode.Draw.ContentBounds.Width, (int)barcode.Draw.ContentBounds.Height, 96, 96, PixelFormats.Pbgra32);
        //    image.Render(barcode.Draw);

        //    return image;
        //}
