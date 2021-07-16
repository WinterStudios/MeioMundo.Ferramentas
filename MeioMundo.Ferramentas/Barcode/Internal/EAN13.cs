using MeioMundo.Ferramentas.Escola;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MeioMundo.Ferramentas.Barcode.Internal
{
    public class EAN13
    {
        private const double InchsToCmFactor = 2.54d;
        private const double CmToPixelsFactor = 37.79d;
        private double _height;
        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public int Resolution { get; set; } = 150;
        public string Codigo { get; set; }
        public string EncodeCode { get; set; }
        public bool IncluidNumbers { get; set; } = false;

        internal static DrawingVisual Draw(string code, int resolution, bool showText = true, bool transparency = false) 
        {
            DrawingVisual visual = new DrawingVisual();

            DrawingContext drawingSpace = visual.RenderOpen();

            Brush brush = Brushes.White;
            Pen pen = new Pen(brush, 1);

            /// Draw Section 


            // Draw Background
            if(!transparency)
                drawingSpace.DrawRectangle(brush, null, new Rect(0, 0, 0, 0));


            

            return null; 
        }

        public static void Draw()
        {

        }
        public struct _Chars
        {
            public static byte[] L0 { get => new byte[] { 0, 1 }; }


            public static byte[] R0 { get => new byte[] { 0, 1 }; }


            public static byte[] G0 { get => new byte[] { 0, 1 }; }
        }

        private int CheckSum() { return 0; }
        private static bool CheckSum(string code)
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
            if (checkSum == checkDigit)
                return true;
            else return false;
        }

        private static int? CalcCheckDigit(string code)
        {
            if (code.Length < 12 || CheckIfEAN13(code))
                return null;


            int[] _n = new int[12];

            for (int i = 0; i < code.Length; i++)
            {
                _n[i] = int.Parse(code[i].ToString());
            }

            return CalcCheckDigit(_n);
        }
        private static int CalcCheckDigit(int[] numbers)
        {
            int sum = 0;
            int checkSum = 0;
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

        private static bool CheckEncoding() { return false; }
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
        private static string Encoded(string code) { return ""; }

    }
}
