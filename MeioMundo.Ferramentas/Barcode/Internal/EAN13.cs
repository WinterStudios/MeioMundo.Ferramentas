using MeioMundo.Ferramentas.Escola;
using MeioMundo.Ferramentas.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeioMundo.Ferramentas.Barcode.Internal
{
    public class EAN13 : ViewModelBase, IBarCode
    {
        public string Code
        {
            get { return m_code; }
            set { m_code = value.ToUpper(); Draw(); }
        }
        public DisplayCodeType DisplayCodeType
        {
            get { return m_displayCodeType; }
            set { m_displayCodeType = value; Draw(); }
        }
        public BarcodeImageResolution BarcodeImageResolution
        {
            get { return m_barcodeImageResolution; }
            set { m_barcodeImageResolution = value; Draw(); OnPropertyChanged(); }
        }
        public BarcodeHeight BarcodeHeight
        {
            get { return m_barcodeHeight; }
            set { m_barcodeHeight = value; Draw(); }
        }
        public BarType BarType { get => BarType.EAN13; }

        public char[] Chars => new char[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        };

        public byte[] Symbles => null;

        public BitmapSource CodeImage
        {
            get { return m_codeImage; }
            set { m_codeImage = value; OnPropertyChanged(); }
        }

        private string m_code;
        private DisplayCodeType m_displayCodeType;
        private BarcodeImageResolution m_barcodeImageResolution;
        private BarcodeHeight m_barcodeHeight;
        private BitmapSource m_codeImage;


        Brush BarColor = Brushes.Black;
        Brush BackgroundColor = Brushes.White;


        private enum GroupSide
        {
            L,
            G,
            R
        }

        public EAN13()
        {
            DisplayCodeType = DisplayCodeType.PerChar;
        }

        public EAN13(string code, BarcodeImageResolution res, BarcodeHeight height, DisplayCodeType displayType)
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
                    return 1f;
                case BarcodeHeight.Low:
                    return 2f;
                case BarcodeHeight.Normal:
                    return 3f;
                case BarcodeHeight.High:
                    return 6f;
                case BarcodeHeight.VeryHigh:
                    return 9f;
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


        public void Draw()
        {
            if (string.IsNullOrEmpty(Code) || Code.Length < 10)
                return;

            DrawingVisual visual = new DrawingVisual();
            DrawingContext drawingSpace = visual.RenderOpen();

            float resolution = GetResolution();
            int defaultWidthChar = 14;  // 26 pixels
            float defaultHeightChar = (float)defaultWidthChar * GetCharHeight();

            Typeface font = new Typeface(new FontFamily("Calibri"), FontStyles.Normal, FontWeights.SemiBold, FontStretches.Normal);

            FormattedText charFormatted = new FormattedText(Code, CultureInfo.GetCultureInfo("pt-PT"), FlowDirection.LeftToRight, font, 16 * resolution, BarColor, 1);
            FormattedText fristNumber = new FormattedText(Code[0].ToString(), CultureInfo.GetCultureInfo("pt-PT"), FlowDirection.LeftToRight, font, 16 * resolution, BarColor, 1);


            double charTextHeightOffset = 2 * resolution;
            double defaultCharHeight = charFormatted.Height - (charFormatted.Baseline - charFormatted.Extent);

            double centerX = (defaultWidthChar * resolution / 2) - (charFormatted.Width / 2);
            double centerY = defaultHeightChar * resolution - ((charFormatted.Baseline - charFormatted.Extent) / 2);
            drawingSpace.DrawRectangle(BackgroundColor, null, new Rect(0, 0, defaultWidthChar * resolution, (defaultHeightChar * resolution + defaultCharHeight + charTextHeightOffset)));

            //Draw Frist Number
            if (DisplayCodeType == DisplayCodeType.PerChar || DisplayCodeType == DisplayCodeType.Center)
                drawingSpace.DrawText(fristNumber, new Point(0, defaultHeightChar * resolution - (fristNumber.OverhangLeading * 2)));
            //Draw C1 (Start)
            BitmapSource C1_image = DrawChar("C1", true);
            Size C1 = new Size(C1_image.Width, C1_image.Height + fristNumber.Extent);
            drawingSpace.DrawImage(C1_image, new Rect(new Point(fristNumber.Width, 0), C1));

            string leftSide = Code.Substring(1, 6);
            string rightSide = Code.Substring(7);

            if (rightSide.Length == 5)
                rightSide += CalcCheckDigit(Code);

            double leftSideStartX = C1.Width;

            string LeftPatter = GetGroupLetter(Code[0]);

            double codeWidth = leftSideStartX;

            // Draw Left Side

            for (int L = 0; L < leftSide.Length; L++)
            {
                string enc = string.Format("{0}{1}", LeftPatter[L], leftSide[L]);
                BitmapSource L_image = DrawChar(enc, false);
                Size L_Size = new Size(L_image.Width, L_image.Height);
                Rect L_rect = new Rect(leftSideStartX + L_Size.Width * L, 0, L_Size.Width, L_Size.Height);
                codeWidth += L_Size.Width;
                drawingSpace.DrawImage(L_image, L_rect);
                if (DisplayCodeType == DisplayCodeType.PerChar || DisplayCodeType == DisplayCodeType.Center)
                {
                    FormattedText L_C = new FormattedText(leftSide[L].ToString(), CultureInfo.GetCultureInfo("pt-PT"), FlowDirection.LeftToRight, font, 16 * resolution, BarColor, 1);
                    drawingSpace.DrawText(L_C, new Point(L_rect.Left, L_rect.Height - (fristNumber.OverhangLeading * 2)));
                }
            }

            // Draw C2 (middle Separeter)

            BitmapSource C2_Image = DrawChar("C2", true);
            drawingSpace.DrawImage(C2_Image, new Rect(codeWidth, 0, C2_Image.Width, C2_Image.Height + fristNumber.Extent));

            codeWidth += C2_Image.Width - (C2_Image.Width / 5);

            // Draw Right Side 

            for (int R = 0; R < rightSide.Length; R++)
            {
                string enc = string.Format("R{0}", rightSide[R]);
                BitmapSource R_image = DrawChar(enc, false);
                Size R_Size = new Size(R_image.Width, R_image.Height);
                Rect R_rect = new Rect(codeWidth, 0, R_Size.Width, R_Size.Height);
                drawingSpace.DrawImage(R_image, R_rect);
                if (DisplayCodeType == DisplayCodeType.Center || DisplayCodeType == DisplayCodeType.PerChar)
                {
                    FormattedText L_C = new FormattedText(rightSide[R].ToString(), CultureInfo.GetCultureInfo("pt-PT"), FlowDirection.LeftToRight, font, 16 * resolution, BarColor, 1);
                    drawingSpace.DrawText(L_C, new Point(R_rect.Left, R_rect.Height - (fristNumber.OverhangLeading * 2)));
                }
                codeWidth += R_Size.Width;
            }

            // Draw C3 (End Point)
            BitmapSource C3_Image = DrawChar("C3", true);
            drawingSpace.DrawImage(C3_Image, new Rect(codeWidth, 0, C3_Image.Width, C3_Image.Height + fristNumber.Extent));

            drawingSpace.Close();

            int sizeX = (int)Math.Round(visual.Drawing.Bounds.Width);
            int sizeY = (int)Math.Round(visual.Drawing.Bounds.Height);

            var targetBitmap = new RenderTargetBitmap(sizeX, sizeY, 96, 96, PixelFormats.Pbgra32);
            targetBitmap.Render(visual);
            CodeImage = targetBitmap;

        }

        internal BitmapSource DrawChar(string c, bool heightPlus)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext drawingSpace = visual.RenderOpen();

            float resolution = GetResolution();
            int defaultWidthChar = 14;  // 26 pixels
            float defaultHeightChar = (float)defaultWidthChar * GetCharHeight();
            
            drawingSpace.DrawRectangle(BackgroundColor, null, new Rect(0, 0, defaultWidthChar * resolution, (defaultHeightChar * resolution)));

            byte[] data = GetSymble(c);

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 1)
                    drawingSpace.DrawRectangle(BarColor, null, new Rect(i * 2 * resolution, 0, 2 * resolution, defaultHeightChar * resolution));
            }

            drawingSpace.Close();

            int sizeX = (int)Math.Round(visual.Drawing.Bounds.Width);
            int sizeY = (int)Math.Round(visual.Drawing.Bounds.Height);

            var targetBitmap = new RenderTargetBitmap(sizeX, sizeY, 96, 96, PixelFormats.Pbgra32);
            targetBitmap.Render(visual);
            return (BitmapSource)targetBitmap;
        }


        private static string GetGroupLetter(char inicialNumber)
        {
            switch (inicialNumber)
            {
                case '0':
                    return "LLLLLL";
                case '1':
                    return "LLGLGG";
                case '2':
                    return "LLGGLG";
                case '3':
                    return "LLGGGL";
                case '4':
                    return "LGLLGG";
                case '5':
                    return "LGGLLG";
                case '6':
                    return "LGGGLL";
                case '7':
                    return "LGLGLG";
                case '8':
                    return "LGLGGL";
                case '9':
                    return "LGGLGL";
                default:
                    return "";
            }
        }
        private static byte[] GetSymble(string s)
        {
            switch (s)
            {
                case "C1":
                    return new byte[] { 1, 0, 1 };
                case "C2":
                    return new byte[] { 0, 1, 0, 1, 0 };
                case "C3":
                    return new byte[] { 1, 0, 1 };

                // RIGHT SIDE ---------------------------------
                case "R0":
                    return new byte[] { 1, 1, 1, 0, 0, 1, 0 };
                case "R1":
                    return new byte[] { 1, 1, 0, 0, 1, 1, 0 };
                case "R2":
                    return new byte[] { 1, 1, 0, 1, 1, 0, 0 };
                case "R3":
                    return new byte[] { 1, 0, 0, 0, 0, 1, 0 };
                case "R4":
                    return new byte[] { 1, 0, 1, 1, 1, 0, 0 };
                case "R5":
                    return new byte[] { 1, 0, 0, 1, 1, 1, 0 };
                case "R6":
                    return new byte[] { 1, 0, 1, 0, 0, 0, 0 };
                case "R7":
                    return new byte[] { 1, 0, 0, 0, 1, 0, 0 };
                case "R8":
                    return new byte[] { 1, 0, 0, 1, 0, 0, 0 };
                case "R9":
                    return new byte[] { 1, 1, 1, 0, 1, 0, 0 };

                // GREEN SIDE  -------------------------------
                case "G0":
                    return new byte[] { 0, 1, 0, 0, 1, 1, 1 };
                case "G1":
                    return new byte[] { 0, 1, 1, 0, 0, 1, 1 };
                case "G2":
                    return new byte[] { 0, 0, 1, 1, 0, 1, 1 };
                case "G3":
                    return new byte[] { 0, 1, 0, 0, 0, 0, 1 };
                case "G4":
                    return new byte[] { 0, 0, 1, 1, 1, 0, 1 };
                case "G5":
                    return new byte[] { 0, 1, 1, 1, 0, 0, 1 };
                case "G6":
                    return new byte[] { 0, 0, 0, 0, 1, 0, 1 };
                case "G7":
                    return new byte[] { 0, 0, 1, 0, 0, 0, 1 };
                case "G8":
                    return new byte[] { 0, 0, 0, 1, 0, 0, 1 };
                case "G9":
                    return new byte[] { 0, 0, 1, 0, 1, 1, 1 };

                // Left Side ---------------------------------
                case "L0":
                    return new byte[] { 0, 0, 0, 1, 1, 0, 1 };
                case "L1":
                    return new byte[] { 0, 0, 1, 1, 0, 0, 1 };
                case "L2":
                    return new byte[] { 0, 0, 1, 0, 0, 1, 1 };
                case "L3":
                    return new byte[] { 0, 1, 1, 1, 1, 0, 1 };
                case "L4":
                    return new byte[] { 0, 1, 0, 0, 0, 1, 1 };
                case "L5":
                    return new byte[] { 0, 1, 1, 0, 0, 0, 1 };
                case "L6":
                    return new byte[] { 0, 1, 0, 1, 1, 1, 1 };
                case "L7":
                    return new byte[] { 0, 1, 1, 1, 0, 1, 1 };
                case "L8":
                    return new byte[] { 0, 1, 1, 0, 1, 1, 1 };
                case "L9":
                    return new byte[] { 0, 0, 0, 1, 0, 1, 1 };
                default:
                    return new byte[] { };
            }
        }

        private static int? CalcCheckDigit(string code)
        {
            int[] numbers = new int[code.Length];
            int checkDigit = 0;
            int checkSum = 0;
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                int.TryParse(code[i].ToString(), out numbers[i]);
                if (i == 12)
                    checkDigit = numbers[i];
            }
            for (int i = 0; i < 12; i += 2)
            {
                sum += numbers[i] * 1;
            }
            for (int i = 1; i < 12; i += 2)
            {
                sum += numbers[i] * 3;
            }
            checkSum = (10 - (sum % 10)) % 10;
            return checkSum;
        }

        private static bool CheckIfEAN13(string code)
        {
            if (code.Length > 13 && code.Length < 12)
                return false;

            for (int i = 0; i < code.Length; i++)
            {
                if (!char.IsDigit(code[i]))
                    return false;
            }
            return true;
        }

        public string CheckCode(string s)
        {
            throw new NotImplementedException();
        }

        //    private const double InchsToCmFactor = 2.54d;
        //    private const double CmToPixelsFactor = 37.79d;
        //    private double _height;
        //    public double Height
        //    {
        //        get { return _height; }
        //        set { _height = value; }
        //    }
        //    public int Resolution { get; set; } = 150;
        //    public string Codigo { get; set; }
        //    public string EncodeCode { get; set; }
        //    public bool IncluidNumbers { get; set; } = false;

        //    internal static DrawingVisual Draw(string code, int resolution, bool showText = true, bool transparency = false) 
        //    {
        //        DrawingVisual visual = new DrawingVisual();

        //        DrawingContext drawingSpace = visual.RenderOpen();

        //        Brush brush = Brushes.White;
        //        Pen pen = new Pen(brush, 1);

        //        /// Draw Section 


        //        // Draw Background
        //        if(!transparency)
        //            drawingSpace.DrawRectangle(brush, null, new Rect(0, 0, 0, 0));




        //        return null; 
        //    }

        //    public static void Draw()
        //    {

        //    }
        //    public struct _Chars
        //    {
        //        public static byte[] L0 { get => new byte[] { 0, 1 }; }


        //        public static byte[] R0 { get => new byte[] { 0, 1 }; }


        //        public static byte[] G0 { get => new byte[] { 0, 1 }; }
        //    }

        //    private int CheckSum() { return 0; }
        //    private static bool CheckSum(string code)
        //    {
        //        int[] numbers = new int[code.Length];
        //        int checkDigit = 0;
        //        int checkSum = 0;
        //        int sum = 0;
        //        for (int i = 0; i < numbers.Length; i++)
        //        {
        //            int.TryParse(code[i].ToString(), out numbers[i]);
        //            if (i == 12)
        //                checkDigit = numbers[i];
        //        }
        //        for (int i = 0; i < 12; i += 2)
        //        {
        //            sum += numbers[i] * 1;
        //        }
        //        for (int i = 1; i < 12; i += 2)
        //        {
        //            sum += numbers[i] * 3;
        //        }
        //        checkSum = (10 - (sum % 10)) % 10;
        //        if (checkSum == checkDigit)
        //            return true;
        //        else return false;
        //    }

        //    private static int? CalcCheckDigit(string code)
        //    {
        //        if (code.Length < 12 || CheckIfEAN13(code))
        //            return null;


        //        int[] _n = new int[12];

        //        for (int i = 0; i < code.Length; i++)
        //        {
        //            _n[i] = int.Parse(code[i].ToString());
        //        }

        //        return CalcCheckDigit(_n);
        //    }
        //    private static int CalcCheckDigit(int[] numbers)
        //    {
        //        int sum = 0;
        //        int checkSum = 0;
        //        for (int i = 0; i < 12; i += 2)
        //        {

        //            sum += numbers[i] * 1;
        //        }
        //        for (int i = 1; i < 12; i += 2)
        //        {
        //            sum += numbers[i] * 3;
        //        }
        //        checkSum = (10 - (sum % 10)) % 10;
        //        return checkSum;
        //    }

        //    private static bool CheckEncoding() { return false; }
        //    private static bool CheckIfEAN13(string code) 
        //    {
        //        if (code.Length > 13 && code.Length < 12)
        //            return false;

        //        for (int i = 0; i < code.Length; i++)
        //        {
        //            if (!char.IsDigit(code[i]))
        //                return false;
        //        }
        //        return true;
        //    }
        //    private static string Encoded(string code) { return ""; }

        //}

    }
}