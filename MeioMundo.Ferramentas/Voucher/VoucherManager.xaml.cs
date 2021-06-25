using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeioMundo.Ferramentas.Voucher
{
    /// <summary>
    /// Interaction logic for VoucherManager.xaml
    /// </summary>
    public partial class VoucherManager : UserControl
    {
        public int GitfCar_Value { get => _giftCar_Value; set => _giftCar_Value = value; }
        public int _giftCar_Value;
        public UIElement PreviewGiftCard { get => _previewGiftCard; set => _previewGiftCard = value; }
        private UIElement _previewGiftCard;
        public VoucherManager()
        {
            InitializeComponent();
            this.Loaded += VoucherManager_Loaded;
            UC_ComboBox_GiftCard_Value.ItemsSource = new int[]
            {
                5,10,15,20,25,30,40,50,75
            };
            UC_ComboBox_GiftCard_Value.SelectedItem = 5;
            
        }

        private void VoucherManager_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            if(tag == "__Print_Gifts")
            {
                VoucherSystem.PrintTest((int)UC_ComboBox_GiftCard_Value.SelectedValue);
            }
        }

        private void UC_ComboBox_GiftCard_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PreviewGiftCard = VoucherSystem.GetVoucherPreview((int)UC_ComboBox_GiftCard_Value.SelectedValue, 30);
            UC_VoucherPreview_Border.Child = PreviewGiftCard;
        }
    }
}
