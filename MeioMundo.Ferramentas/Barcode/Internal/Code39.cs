﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeioMundo.Ferramentas.Barcode.Internal
{
    public class Code39 : IBarCode
    {
        //internal static DrawingVisual Draw(string code, double resolution, bool showText = true)
        //{

        //    code = code.ToUpper();   
        //    string codeToPrint = string.Format("*{0}*", code);
        //    int charHeight = 4;
        //    int charWidth = 14;


        //    DrawingVisual visual = new DrawingVisual();

        //    DrawingContext drawingSpace = visual.RenderOpen();

        //    Brush brush = Brushes.White;
        //    Pen pen = new Pen(brush, 1);

        //    //width = (int)Math.Round((float)code.Length * (float)charWidth * resolution);
        //    int _charHeight = (int)Math.Round(charHeight * resolution / 2);
        //    drawingSpace.DrawRectangle(brush, null, new Rect(0, 0, (double)codeToPrint.Length * (double)charWidth * resolution, _charHeight * resolution));

        //    Brush b = Brushes.Black;

        //    for (int i = 0; i < codeToPrint.Length; i++)
        //    {
        //        char c = codeToPrint[i];
        //        byte[] _c = Chars.ToChar(c);
        //        if (c == '-')
        //            b = Brushes.Black;
        //        else
        //            b = Brushes.Black;
        //        for (int z = 0; z < _c.Length; z++)
        //        {
        //            if(_c[z] == 1)
        //                drawingSpace.DrawRectangle(b, null, new Rect(((float)i * (float)charWidth * resolution) + ((float)z * (float)resolution), 0, 1 * (float)resolution, _charHeight * resolution));
        //        }
        //    }

        //    //height = _charHeight;
        //    if (showText)
        //    {
        //        // Add Pixel Density for .Net 6
        //        FormattedText textToFormat = new FormattedText(code, CultureInfo.GetCultureInfo("pt-PT"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 10d * resolution, Brushes.Black, 300);
        //        //height = (int)Math.Round(textToFormat.Height) + _charHeight;
        //        double textSpace = textToFormat.WidthIncludingTrailingWhitespace;
        //        double star = ((double)codeToPrint.Length * (double)charWidth * resolution - textSpace) / 2;
        //        drawingSpace.DrawText(textToFormat, new Point((int)Math.Round((float)star), _charHeight * resolution));
        //    }

        //    drawingSpace.Close();

        //    return visual;
        //}

        //public struct Chars
        //{
        //    /// <value>*</value>
        //    public static byte[] _asterisk => new byte[] { 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _less => new byte[] { 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1 }; // Leitor le:"'"
        //    public static byte[] _space => new byte[] { 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 0 };
        //    public static byte[] _dolar => new byte[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1 };
        //    public static byte[] _percent => new byte[] { 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 };
        //    public static byte[] _plus => new byte[] { 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1 };     // Leitor le: "»"
        //    public static byte[] _dot => new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _slash_r => new byte[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1 };            // Leitor le: "-"
        //    public static byte[] _0 => new byte[] { 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _1 => new byte[] { 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _2 => new byte[] { 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _3 => new byte[] { 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1 };
        //    public static byte[] _4 => new byte[] { 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _5 => new byte[] { 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1 };
        //    public static byte[] _6 => new byte[] { 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 1 };
        //    public static byte[] _7 => new byte[] { 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1 };
        //    public static byte[] _8 => new byte[] { 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _9 => new byte[] { 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _A => new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1 };
        //    public static byte[] _B => new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1 };
        //    public static byte[] _C => new byte[] { 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1 };
        //    public static byte[] _D => new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 1 };
        //    public static byte[] _E => new byte[] { 0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 1, 0, 1 };
        //    public static byte[] _F => new byte[] { 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1 };
        //    public static byte[] _G => new byte[] { 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1 };
        //    public static byte[] _H => new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1 };
        //    public static byte[] _I => new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1 };
        //    public static byte[] _J => new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1 };
        //    public static byte[] _K => new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1 };
        //    public static byte[] _L => new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1 };
        //    public static byte[] _M => new byte[] { 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1 };
        //    public static byte[] _N => new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1 };
        //    public static byte[] _O => new byte[] { 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1 };
        //    public static byte[] _P => new byte[] { 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1 };
        //    public static byte[] _Q => new byte[] { 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1 };
        //    public static byte[] _R => new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1 };
        //    public static byte[] _S => new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 1 };
        //    public static byte[] _T => new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1 };
        //    public static byte[] _U => new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _V => new byte[] { 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _W => new byte[] { 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1 };
        //    public static byte[] _X => new byte[] { 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _Y => new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1 };
        //    public static byte[] _Z => new byte[] { 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1 };




        //    public static byte[] ToChar(char c)
        //    {
        //        //c = c.ToString().ToUpper().ToCharArray();
        //        byte[] data = new byte[] { };
        //        switch (c)
        //        {
        //            case '*':
        //                return _asterisk;
        //            case '-':
        //                return _slash_r; // _less;
        //            case '$':
        //                return _dolar;
        //            case ' ':
        //                return _space;
        //            case '%':
        //                return _percent;
        //            case '+':
        //                return _plus;
        //            case '0':
        //                return _0;
        //            case '1':
        //                return _1;
        //            case '2':
        //                return _2;
        //            case '3':
        //                return _3;
        //            case '4':
        //                return _4;
        //            case '5':
        //                return _5;
        //            case '6':
        //                return _6;
        //            case '7':
        //                return _7;
        //            case '8':
        //                return _8;
        //            case '9':
        //                return _9;
        //            case 'A':
        //                return _A;
        //            case 'B':
        //                return _B;
        //            case 'C':
        //                return _C;
        //            case 'D':
        //                return _D;
        //            case 'E':
        //                return _E;
        //            case 'F':
        //                return _F;
        //            case 'G':
        //                return _G;
        //            case 'H':
        //                return _H;
        //            case 'I':
        //                return _I;
        //            case 'J':
        //                return _J;
        //            case 'K':
        //                return _K;
        //            case 'L':
        //                return _L;
        //            case 'M':
        //                return _M;
        //            case 'N':
        //                return _N;
        //            case 'O':
        //                return _O;
        //            case 'P':
        //                return _P;
        //            case 'Q':
        //                return _Q;
        //            case 'R':
        //                return _R;
        //            case 'S':
        //                return _S;
        //            case 'T':
        //                return _T;
        //            case 'U':
        //                return _U;
        //            case 'V':
        //                return _V;
        //            case 'W':
        //                return _W;
        //            case 'X':
        //                return _X;
        //            case 'Y':
        //                return _Y;
        //            case 'Z':
        //                return _Z;
        //            default:
        //                return new byte[] { };
        //        }
        //    }
        //}
        public string Code 
        {
            get { return m_code; }
            set { m_code = value; Draw(); }
        }
        public DisplayCodeType DisplayCodeType
        {
            get { return m_displayCodeType; }
            set { m_displayCodeType = value; Draw(); }
        }
        public BarcodeImageResolution BarcodeImageResolution
        {
            get { return m_barcodeImageResolution; }
            set { m_barcodeImageResolution = value; Draw(); }
        }
        public BarcodeHeight BarcodeHeight
        {
            get { return m_barcodeHeight; }
            set { m_barcodeHeight = value; Draw(); }
        }
        public BarType BarType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public char[] Chars => new char[] {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'Y', 'X', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '*', ' ', '-', '+', '$', '%'
        };

        public byte[] Symbles => null;

        public BitmapSource CodeImage
        {
            get { return m_codeImage; }
            set { m_codeImage = value; }
        }

        private string m_code;
        private DisplayCodeType m_displayCodeType;
        private BarcodeImageResolution m_barcodeImageResolution;
        private BarcodeHeight m_barcodeHeight;
        private BitmapSource m_codeImage;


        Brush BarColor = Brushes.Black;
        Brush BackgroundColor = Brushes.Violet;


        public Code39()
        {

        }


        private float GetCharHeight(float customRatio = 1)
        {
            switch (BarcodeHeight)
            {
                case BarcodeHeight.Minimal:
                    return 0.25f;
                case BarcodeHeight.Low:
                    return 0.5f;
                case BarcodeHeight.Normal:
                    return 1f;
                case BarcodeHeight.High:
                    return 2f;
                case BarcodeHeight.VeryHigh:
                    return 3f;
                case BarcodeHeight.Custom:
                    return customRatio;
                default:
                    return 1f;
            }
        }

        public void Draw()
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext drawingSpace = visual.RenderOpen();

            
        }

        
        internal BitmapSource DrawChar(char c, float resolution, bool drawChar)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext drawingSpace = visual.RenderOpen();

            int defaultWidthChar = 26;  // 26 pixels
            float defaultHeightChar = (float)defaultWidthChar * GetCharHeight();

            FormattedText charFormatted = new FormattedText(c.ToString().ToUpper(), CultureInfo.GetCultureInfo("pt-PT"), FlowDirection.LeftToRight, new Typeface("Calibri"), 11d * resolution, BarColor, 1);

            double defaultCharHeight = (charFormatted.Extent + charFormatted.OverhangAfter) * resolution;

            drawingSpace.DrawRectangle(BackgroundColor, null, new Rect(0, 0, defaultWidthChar * resolution, (defaultHeightChar * resolution + defaultCharHeight)));

            byte[] data = GetSymbles(c);

            for (int z = 0; z < data.Length; z++)
            {
                if (data[z] == 1)
                    drawingSpace.DrawRectangle(BarColor, null, new Rect(z * 2 * resolution, 0, 2 * resolution, defaultHeightChar * resolution));
            }

            if (drawChar)
            {
                double center = (defaultWidthChar * resolution / 2) - (charFormatted.Width / 2);
                drawingSpace.DrawText(charFormatted, new Point(center, defaultHeightChar));
            }
            drawingSpace.Close();

            int sizeX = (int)Math.Round(visual.Drawing.Bounds.Width);
            int sizeY = (int)Math.Round(visual.Drawing.Bounds.Height);

            var targetBitmap = new RenderTargetBitmap(sizeX, sizeY, 96, 96, PixelFormats.Pbgra32);
            targetBitmap.Render(visual);
            return (BitmapSource)targetBitmap;
        }

        private static byte[] GetSymbles(char c)
        {
            switch (c)
            {
                case '0':
                    return new byte[] { 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1 };
                case '1':
                    return new byte[] { 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1 };
                case '2':
                    return new byte[] { 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1 };
                case '3':
                    return new byte[] { 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1 };
                case '4':
                    return new byte[] { 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1 };
                case '5':
                    return new byte[] { 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1 };
                case '6':
                    return new byte[] { 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 1 };
                case '7':
                    return new byte[] { 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1 };
                case '8':
                    return new byte[] { 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1 };
                case '9':
                    return new byte[] { 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1 };
                default:
                    return new byte[] { };
            }
        }
        //    public static byte[] _asterisk => new byte[] { 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _less => new byte[] { 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1 }; // Leitor le:"'"
        //    public static byte[] _space => new byte[] { 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 0 };
        //    public static byte[] _dolar => new byte[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1 };
        //    public static byte[] _percent => new byte[] { 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 };
        //    public static byte[] _plus => new byte[] { 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1 };     // Leitor le: "»"
        //    public static byte[] _dot => new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _slash_r => new byte[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1 };            // Leitor le: "-"
        //    public static byte[] _0 => new byte[] { 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _1 => new byte[] { 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _2 => new byte[] { 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _3 => new byte[] { 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1 };
        //    public static byte[] _4 => new byte[] { 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _5 => new byte[] { 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1 };
        //    public static byte[] _6 => new byte[] { 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 1 };
        //    public static byte[] _7 => new byte[] { 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1 };
        //    public static byte[] _8 => new byte[] { 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _9 => new byte[] { 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _A => new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1 };
        //    public static byte[] _B => new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1 };
        //    public static byte[] _C => new byte[] { 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1 };
        //    public static byte[] _D => new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 1 };
        //    public static byte[] _E => new byte[] { 0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 1, 0, 1 };
        //    public static byte[] _F => new byte[] { 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1 };
        //    public static byte[] _G => new byte[] { 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1 };
        //    public static byte[] _H => new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1 };
        //    public static byte[] _I => new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1 };
        //    public static byte[] _J => new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1 };
        //    public static byte[] _K => new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1 };
        //    public static byte[] _L => new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1 };
        //    public static byte[] _M => new byte[] { 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1 };
        //    public static byte[] _N => new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1 };
        //    public static byte[] _O => new byte[] { 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1 };
        //    public static byte[] _P => new byte[] { 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1 };
        //    public static byte[] _Q => new byte[] { 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1 };
        //    public static byte[] _R => new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1 };
        //    public static byte[] _S => new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 1 };
        //    public static byte[] _T => new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1 };
        //    public static byte[] _U => new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _V => new byte[] { 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _W => new byte[] { 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1 };
        //    public static byte[] _X => new byte[] { 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1 };
        //    public static byte[] _Y => new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1 };
        //    public static byte[] _Z => new byte[] { 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1 };

    }

}
