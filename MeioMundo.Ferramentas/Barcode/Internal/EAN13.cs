using MeioMundo.Ferramentas.Escola;
using MeioMundo.Ferramentas.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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


        public void Draw()
        {

        }

        private void DrawSymblo(char number, GroupSide side )
        {

        }

        private static byte[] Get(string s)
        {
            switch (s)
            {
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
                    return new byte[] { };
                default:
                    return new byte[] { };
            }
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