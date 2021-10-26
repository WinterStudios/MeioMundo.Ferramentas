using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using MeioMundo.Ferramentas.Internal;

namespace MeioMundo.Ferramentas.Barcode.Internal
{
    public class Code39 : ViewModelBase, IBarCode
    {
        public string Nome
        {
            get { return m_Nome; }
            set { m_Nome = value; OnPropertyChanged(); }
        }
        public string Code 
        {
            get { return m_code; }
            set { m_code = CheckCode(value.ToUpper()); Draw(); OnPropertyChanged(); }
        }
        public DisplayCodeType DisplayCodeType
        {
            get { return m_displayCodeType; }
            set { m_displayCodeType = value; Draw(); OnPropertyChanged(); }
        }
        public BarcodeImageResolution BarcodeImageResolution
        {
            get { return m_barcodeImageResolution; }
            set { m_barcodeImageResolution = value; Draw(); OnPropertyChanged(); }
        }
        public BarcodeHeight BarcodeHeight
        {
            get { return m_barcodeHeight; }
            set { m_barcodeHeight = value; Draw(); OnPropertyChanged(); }
        }
        public BarType BarType { get => BarType.Code39; }

        public char[] Chars => new char[] {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'Y', 'X', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '*', ' ', '-', '+', '$', '%'
        };

        public byte[] Symbles => null;

        public BitmapSource CodeImage
        {
            get { return m_codeImage; }
            set { m_codeImage = value; OnPropertyChanged(); }
        }

        public int Count
        {
            get { return m_Count; }
            set { m_Count = value; OnPropertyChanged(); }
        }

        private string m_Nome;
        private string m_code;
        private int m_Count;
        private DisplayCodeType m_displayCodeType;
        private BarcodeImageResolution m_barcodeImageResolution;
        private BarcodeHeight m_barcodeHeight;
        private BitmapSource m_codeImage;


        Brush BarColor = Brushes.Black;
        Brush BackgroundColor = Brushes.White;


        public Code39()
        {
            BarcodeImageResolution = BarcodeImageResolution.Medium;
            BarcodeHeight = BarcodeHeight.Normal;
            DisplayCodeType = DisplayCodeType.Center;
            Code = "";
        }

        public Code39(string code, BarcodeImageResolution res, BarcodeHeight height, DisplayCodeType displayType)
        {
            Code = code;
            BarcodeImageResolution = res;
            BarcodeHeight = height;
            DisplayCodeType = displayType;
            Draw();
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
        private int GetResolution()
        {
            switch (BarcodeImageResolution) // default char width (26) * factor
            {
                case BarcodeImageResolution.VeryLow:
                    return 1;
                case BarcodeImageResolution.Low:
                    return 2;
                case BarcodeImageResolution.MediumLow:
                    return 3;
                case BarcodeImageResolution.Medium:
                    return 4;
                case BarcodeImageResolution.MediumHigh:
                    return 5;
                case BarcodeImageResolution.High:
                    return 6;
                case BarcodeImageResolution.VeryHigh:
                    return 7;
                case BarcodeImageResolution.Extreme:
                    return 8;
                default:
                    return 3;
            }
        }

        public string CheckCode(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                bool exist = false;
                for (int x = 0; x < Chars.Length; x++)
                {
                    if (s[i] == Chars[x])
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                    return string.Empty;
            }
            return s;
        }

        public void Draw()
        {
            if (string.IsNullOrEmpty(Code))
                return;

            DrawingVisual visual = new DrawingVisual();
            DrawingContext drawingSpace = visual.RenderOpen();

            string codeToDraw = string.Format("*{0}*", Code);

            double widthBar = 0;
            double heighBar = 0;
            for (int i = 0; i < codeToDraw.Length; i++)
            {
                bool showCharPerSymble = false;
                if (DisplayCodeType == DisplayCodeType.PerChar)
                    showCharPerSymble = true;
                BitmapSource image = DrawChar(codeToDraw[i], showCharPerSymble);
                drawingSpace.DrawImage(image, new Rect(image.Width * i, 0, image.Width, image.Height));
                widthBar += image.Width;
                heighBar = image.Height;
            }

            if (DisplayCodeType == DisplayCodeType.Center)
            {
                FormattedText charFormatted = new FormattedText(Code.ToString().ToUpper(), CultureInfo.GetCultureInfo("pt-PT"), FlowDirection.LeftToRight, new Typeface("Calibri"), 14 * GetResolution(), BarColor, 1);
                double centerX = (widthBar / 2) - (charFormatted.Width / 2);
                double centerY = heighBar + charFormatted.OverhangAfter;
                drawingSpace.DrawText(charFormatted, new Point(centerX, centerY)); //defaultHeightChar + (charTextHeightOffset * resolution / 2)));
            }


            drawingSpace.Close();

            int sizeX = (int)Math.Round(visual.Drawing.Bounds.Width);
            int sizeY = (int)Math.Round(visual.Drawing.Bounds.Height);

            var targetBitmap = new RenderTargetBitmap(sizeX, sizeY, 96, 96, PixelFormats.Pbgra32);
            targetBitmap.Render(visual);
            CodeImage = targetBitmap;
        }

        
        internal BitmapSource DrawChar(char c, bool drawChar)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext drawingSpace = visual.RenderOpen();

            float resolution = GetResolution();
            int defaultWidthChar = 26;  // 26 pixels
            float defaultHeightChar = (float)defaultWidthChar * GetCharHeight();
            

            FormattedText charFormatted = new FormattedText(c.ToString().ToUpper(), CultureInfo.GetCultureInfo("pt-PT"), FlowDirection.LeftToRight, new Typeface("Calibri"), 12 * resolution, BarColor, 1);
            
            double charTextHeightOffset = 2 * resolution;
            double defaultCharHeight = charFormatted.Height - (charFormatted.Baseline - charFormatted.Extent);
            if (drawChar)
                drawingSpace.DrawRectangle(BackgroundColor, null, new Rect(0, 0, defaultWidthChar * resolution, (defaultHeightChar * resolution + defaultCharHeight + charTextHeightOffset)));
            else
                drawingSpace.DrawRectangle(BackgroundColor, null, new Rect(0, 0, defaultWidthChar * resolution, (defaultHeightChar * resolution)));

            byte[] data = GetSymbles(c);

            for (int z = 0; z < data.Length; z++)
            {
                if (data[z] == 1)
                    drawingSpace.DrawRectangle(BarColor, null, new Rect(z * 2 * resolution, 0, 2 * resolution, defaultHeightChar * resolution));
            }

            if (drawChar)
            {
                double centerX = (defaultWidthChar * resolution / 2) - (charFormatted.Width / 2);
                double centerY = defaultHeightChar * resolution - ((charFormatted.Baseline - charFormatted.Extent)/2);
                if (charFormatted.Text == "*")
                    centerY += charFormatted.OverhangTrailing;
                drawingSpace.DrawText(charFormatted, new Point(centerX, centerY)); //defaultHeightChar + (charTextHeightOffset * resolution / 2)));
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
                // NUMEROS --------------------------------------------------------
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

                // LETRAS ----------------------------------------------------------
                case 'A':
                    return new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1 };
                case 'B':
                    return new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1 };
                case 'C':
                    return new byte[] { 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1 };
                case 'D':
                    return new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 1 };
                case 'E':
                    return new byte[] { 0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 1, 0, 1 };
                case 'F':
                    return new byte[] { 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1 };
                case 'G':
                    return new byte[] { 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1 };
                case 'H':
                    return new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1 };
                case 'I':
                    return new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1 };
                case 'J':
                    return new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1 };
                case 'K':
                    return new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1 };
                case 'L':
                    return new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1 };
                case 'M':
                    return new byte[] { 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1 };
                case 'N':
                    return new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1 };
                case 'O':
                    return new byte[] { 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1 };
                case 'P':
                    return new byte[] { 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1 };
                case 'Q':
                    return new byte[] { 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1 };
                case 'R':
                    return new byte[] { 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1 };
                case 'S':
                    return new byte[] { 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 0, 1 };
                case 'T':
                    return new byte[] { 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1 };
                case 'U':
                    return new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1 };
                case 'V':
                    return new byte[] { 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1 };
                case 'W':
                    return new byte[] { 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1 };
                case 'X':
                    return new byte[] { 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1 };
                case 'Y':
                    return new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1 };
                case 'Z':
                    return new byte[] { 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1 };


                // Simblos ----------------------------------------------------------
                case '*':
                    return new byte[] { 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1 };
                case '%':
                    return new byte[] { 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 };
                case '-':                                                                   // Real:  new byte[] { 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1 };           // Leitor lê:"'"
                    return new byte[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1 };            // ->  (Simblo Incorreto: Leitor Lê '/' como '-')
                case '+':
                    return new byte[] { 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1 };            // Leitor le: "»"
                case '$':
                    return new byte[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1 };
                case '/':
                    return new byte[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1 };           // Leitor lê: '-'
                case '.':
                    return new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1 };
                case ' ':
                    return new byte[] { 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 0 };
                default:
                    return new byte[] { };
            }
        }

        
        //    public static byte[] _asterisk => new byte[] { 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _less => new byte[] { 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1 };           // Leitor le:"'"
        //    public static byte[] _space => new byte[] { 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 0 };
        //    public static byte[] _dolar => new byte[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1 };
        //    public static byte[] _percent => new byte[] { 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 };
        //    public static byte[] _plus => new byte[] { 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1 };           // Leitor le: "»"
        //    public static byte[] _dot => new byte[] { 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1 };
        //    public static byte[] _slash_r => new byte[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1 };        // Leitor le: "-"
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
