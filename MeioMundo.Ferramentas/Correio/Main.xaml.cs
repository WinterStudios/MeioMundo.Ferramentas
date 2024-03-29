﻿using MeioMundo.Ferramentas.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MeioMundo.Ferramentas.Correio
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl, INotifyPropertyChanged
    {

        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public Morada FornecedorMorada { get => fornecedorMorada; set { fornecedorMorada = value; NotifyPropertyChanged(); } }
        private Morada fornecedorMorada;
        
        public People People { get => people; set { people = value; NotifyPropertyChanged(); } }
        private People people;

        public TypeRegister RegisterType { get => _registerType; set { _registerType = value; } }
        private TypeRegister _registerType;


        public ImageBrush SearchIcon { get => null; }
        private ImageBrush searchIcon;



        public Main()
        {
            InitializeComponent();
            //UC_Viewbox_PreviewModelo.Child = new CartaTemplate();
            //combox_registorsTypes.ItemsSource = Enum.GetValues(typeof(TypeRegister)).Cast<TypeRegister>();
            //combox_registorsTypes.SelectedIndex = 0;
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tag = ((TextBox)sender).Tag.ToString();
            string text = ((TextBox)sender).Text;

            if (tag == "Morada_Rua")
                carta.Morada_Rua = text;
            if (tag == "Morada_Localidade")
                carta.Morada_Localidade = text;
            if (tag == "Morada_CodigoPostal")
                carta.Morada_CodigoPostal = text;
            if (tag == "Morada_Pais")
                carta.Morada_Pais = text;

            if (tag == "Registo")
                carta.ResgistoCode = text;
            carta.UpdateLayout();
        }

        private void combox_registorsTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            carta.UpdateSelo(RegisterType);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            if (tag == "PrintEnvelope")
            {
                Window window = new Window();
                StackPanel panel = new StackPanel();
                window.Content = panel;
                window.SizeToContent = SizeToContent.WidthAndHeight;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                TextBlock text = new TextBlock();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.Margin = new Thickness(20,10,20,10);
                text.Text = "Coloque o envelope na bandaja MF com o logo volta para cima";
                panel.Children.Add(text);

                Image image = new Image();
                image.Margin = new Thickness(20,0,20,0);
                image.Height = 256;
                image.Width = 256;
                image.Source = new BitmapImage(new Uri("pack://application:,,,/MeioMundo.Ferramentas;component/Assets/correio-intrucoesdeimpressao.png"));
                panel.Children.Add(image);

                Button button = new Button();
                button.Content = "OK";
                button.Margin = new Thickness(20);
                button.Padding = new Thickness(20);
                button.Click += (sender, arg) =>
                {
                    window.Close();
                };
                panel.Children.Add(button);
                window.ShowDialog();
                

                PrintDialog printDialog = new PrintDialog();
                System.Printing.PageMediaSize envelopeDL = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISODLEnvelope);
                printDialog.PrintTicket.PageMediaSize = envelopeDL;
                printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
                if (printDialog.ShowDialog() == true)
                {
                    try
                    {
                        printDialog.PrintVisual(carta, "My First Print Job");
                    }
                    catch { }
                    
                }
            }

            if(tag == "SelectFornAddress")
            {
                MoradasSelect Address = new MoradasSelect();

                Window window = new Window();

                window.Content = Address;
                window.SizeToContent = SizeToContent.WidthAndHeight;

                if(window.ShowDialog() == true)
                {
                    FornecedorMorada = Address.Morada;
                    People = Address.Person;

                    UX_TextBox_Letter_AddressName.Text = People.Nome;
                    UX_TextBox_Letter_AddressAddress.Text = string.Format("{0}\n{1}", fornecedorMorada.Rua, fornecedorMorada.Localidade);
                    UX_TextBox_Letter_AddressZipCode.Text = fornecedorMorada.ZipCode;
                    UX_TextBox_Letter_AddressContry.Text = fornecedorMorada.Country;
                }

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool check = ((CheckBox)sender).IsChecked.Value;

            if (check)
                carta.GridLogoVisiblity = Visibility.Visible;
            else
                carta.GridLogoVisiblity = Visibility.Hidden;
        }
    }
}
