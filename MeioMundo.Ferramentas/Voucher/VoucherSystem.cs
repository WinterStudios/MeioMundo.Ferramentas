using MeioMundo.Ferramentas.Voucher.Modelo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace MeioMundo.Ferramentas.Voucher
{
    public class VoucherSystem : INotifyPropertyChanged
    {
        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    
        public static List<VoucherData> Vouchers { get => vouchers; set { vouchers = value; } }
        private static List<VoucherData> vouchers;

        private static string VoucherFileNetworkLocation { get => @"\\Srvmm\USR\MeioMundo_Local\Voucheres.json"; }

        public static void Inicialize()
        {
            Vouchers = new List<VoucherData>();
            Load();

        }

        public static void Load()
        {
            try
            {
                if (File.Exists(VoucherFileNetworkLocation))
                {
                    string json = File.ReadAllText(VoucherFileNetworkLocation);
                    Vouchers = JsonSerializer.Deserialize<List<VoucherData>>(json);
                }
            }
            catch (Exception ex)
            {
                using (new Internal.Net.NetworkConnection(@"\\srvmm", new NetworkCredential("meiomundo", "meiomundo")))
                {
                    if (File.Exists(VoucherFileNetworkLocation))
                    {
                        string json = File.ReadAllText(VoucherFileNetworkLocation);
                        Vouchers = JsonSerializer.Deserialize<List<VoucherData>>(json);
                    }
                    //else
                        //Save();
                }
            }
        }

        internal static int GetLastSerialNumber() 
        {
            if (Vouchers.Count == 0)
                return 50000;
            else
                return Vouchers.Last().VoucherSerialNumber;
        }

        internal static void Save()
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { WriteIndented = true };
            string json = JsonSerializer.Serialize(Vouchers, serializerOptions);

            using (new Internal.Net.NetworkConnection(@"\\srvmm", new NetworkCredential("meiomundo", "meiomundo")))
            {
                FileStream stream = new FileStream(VoucherFileNetworkLocation, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] jsonByte = Encoding.UTF8.GetBytes(json);
                byte[] buffer = new byte[1024];

                stream.Write(jsonByte, 0, json.Length);
                stream.Close();
            }    
        }

        internal static void VoucherEmission(int price, int serialNumeber, DateTime emissionDate)
        {
            VoucherData voucher = new VoucherData()
            {
                VoucherPrice = price,
                VoucherSerialNumber = serialNumeber,
                VoucherCreationDate = emissionDate,
            };
            Vouchers.Add(voucher);
            Save();
        }


        public static void PrintTest(int value, int serialNumber)
        {

            UserControl[] controls = new UserControl[2];
            {
                controls[0] = GetFront(value);
                controls[1] = GetBack(value, serialNumber);
            }
            
            MeioMundo.Ferramentas.Internal.PrintSystem.Print(controls);
            for (int i = 0; i < 3; i++)
            {
                VoucherEmission(value, serialNumber+i, DateTime.Today);
            }
        }

        private static UserControl GetFront(int price)
        {
            UserControl control = new UserControl();
            control.Width = (double)new LengthConverter().ConvertFromString("21cm");
            control.Height = (double)new LengthConverter().ConvertFromString("29,7cm");
            control.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //viewbox.Child = control;

            Border border = new Border() { Padding = new Thickness((double)new LengthConverter().ConvertFromString("0,5cm")) };

            control.Content = border;

            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical };
            border.Child = stackPanel;
            int rep = 3;
            for (int i = 0; i < rep; i++)
            {
                Viewbox viewboxVoucher = new Viewbox();
                Modelo.VoucherModelo_Front front = new Modelo.VoucherModelo_Front(price);
                viewboxVoucher.Child = front;
                
                stackPanel.Children.Add(viewboxVoucher);
            }
            return control;
        }
        private static UserControl GetBack(int price, int serialNumber)
        {
            UserControl control = new UserControl();
            control.Width = (double)new LengthConverter().ConvertFromString("21cm");
            control.Height = (double)new LengthConverter().ConvertFromString("29,7cm");
            control.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //viewbox.Child = control;

            Border border = new Border() { Padding = new Thickness((double)new LengthConverter().ConvertFromString("0,5cm")) };

            control.Content = border;

            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical };
            border.Child = stackPanel;
            int rep = 3;
            for (int i = 0; i < rep; i++)
            {
                Viewbox viewboxVoucher = new Viewbox();
                Modelo.VoucherModelo_Back back = new Modelo.VoucherModelo_Back(price, serialNumber + i);
                viewboxVoucher.Child = back;

                stackPanel.Children.Add(viewboxVoucher);
            }
            return control;
        }
        internal static UIElement GetVoucherPreview(int price, int serialNumber)
        {
            StackPanel stack = new StackPanel();

            Modelo.VoucherModelo_Front front = new Modelo.VoucherModelo_Front(price);
            
            Modelo.VoucherModelo_Back back = new Modelo.VoucherModelo_Back(price, serialNumber);
            
            stack.Orientation = Orientation.Vertical;
            stack.Children.Add(front);
            stack.Children.Add(back);

            return (UIElement)stack;
        }
        internal static void SetGiftCardExit()
        {

        }
    }
}
