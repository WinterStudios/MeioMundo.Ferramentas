﻿using MeioMundo.Ferramentas.Correio.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

using MeioMundo.Ferramentas.Internal;
using System.Windows.Markup;
using System.IO;
using System.Xml;
using System.Windows.Automation.Peers;

namespace MeioMundo.Ferramentas.Correio
{
    /// <summary>
    /// Interaction logic for CartaCreator.xaml
    /// </summary>
    public partial class CartaCreator : UserControl, INotifyPropertyChanged
    {

        public double Zoom { get => _zoom; set { _zoom = value; ApplyZoom(value); OnPropertyChanged(); } }
        private double _zoom;

        public ObservableCollection<UserControl> Paginas { get; set; }
        //private List<UserControl> _paginas;

        public static bool IsText_Bold { get => _IsText_Bold; set { _IsText_Bold = value; } }
        private static bool _IsText_Bold;



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public CartaCreator()
        {
            Paginas = new ObservableCollection<UserControl>();
            Paginas.Add(new Internal.CartaBodyTemplate()); 
            this.Loaded += CartaCreator_Loaded;
            InitializeComponent();
            Pages.ItemsSource = Paginas;

            
        }
        private void Load()
        {

        }

        private void CreateNewPage()
        {
            
        }

        private void CartaCreator_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNew();
        }

        void LoadNew()
        {
           
            double zoomToFit = (viewBox.ViewportWidth - 18) / viewContent.ActualWidth;
            if(!double.IsInfinity(zoomToFit))
                zoomSlider.Value = Math.Round(zoomToFit * 100,0);
        }

        private void ZoomToFit()
        {
            double zoomToFit = (viewBox.ViewportWidth - 18) / viewContent.ActualWidth;
            if (!double.IsInfinity(zoomToFit))
                zoomSlider.Value = Math.Round(zoomToFit * 100, 0);
        }
        private double ApplyZoom(double d)
        {
            scaleTransform.ScaleX = scaleTransform.ScaleY = d / 100;
            UpdateZoomPagesLayout();
            return d;
        }

        private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue > 100)
                Zoom = Math.Round(100 + ((e.NewValue - 100) * 4),0);
            else
                Zoom = Math.Round(e.NewValue, 0);
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            //System.Printing.PageMediaSize envelopeDL = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISODLEnvelope);
            //printDialog.PrintTicket.PageMediaSize = envelopeDL;
            //printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
            if (printDialog.ShowDialog() == true)
            {
                
                
                var document = new FixedDocument();
                var listparent = new DependencyObject[Paginas.Count];
                for (int i = 0; i < Paginas.Count; i++)
                {
                    if (Paginas[i].GetType() == typeof(CartaBodyTemplate))
                        ((CartaBodyTemplate)Paginas[i]).richtextbox.SpellCheck.IsEnabled = false;
                    Size pageSize = new Size(Paginas[i].RenderSize.Width, Paginas[i].RenderSize.Height); // A4 page, at 96 dpi
                    // Create FixedPage
                    var fixedPage = new FixedPage();

                    // Add visual, measure/arrange page.

                    fixedPage.Width = pageSize.Width;
                    fixedPage.Height = pageSize.Height;

                    document.DocumentPaginator.PageSize = pageSize;
                    listparent[i] = ((FrameworkElement)Paginas[i]).GetParent(false);
                    ((FrameworkElement)Paginas[i]).DisconnectIt();
                    fixedPage.Children.Add((UIElement)Paginas[i]);
                    fixedPage.Measure(pageSize);
                    fixedPage.Arrange(new Rect(new Point(), pageSize));
                    fixedPage.UpdateLayout();

                    // Add page to document
                    var pageContent = new PageContent();
                    ((IAddChild)pageContent).AddChild(fixedPage);
                    document.Pages.Add(pageContent);
                }
                System.Printing.PageMediaSize a4 = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISOA4);
                printDialog.PrintTicket.PageMediaSize = a4;
                printDialog.PrintTicket.PageResolution = new System.Printing.PageResolution(System.Printing.PageQualitativeResolution.Normal);

                printDialog.PrintDocument(document.DocumentPaginator, "TEst");
                for (int z = 0; z < Paginas.Count; z++)
                {
                    ((FrameworkElement)Paginas[z]).DisconnectIt();
                    Paginas[z].AddToParent(listparent[z]);
                    if (Paginas[z].GetType() == typeof(CartaBodyTemplate))
                        ((CartaBodyTemplate)Paginas[z]).richtextbox.SpellCheck.IsEnabled = true;
                }
            }
        }

        private void ToolBar_Insirir_Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            if(tag == "Pagina.New")
            {
                CartaMainPage page = new CartaMainPage();
                Paginas.Add(page);
                UpdatePagesNumbers();
            }
        }
        private void UpdatePagesNumbers()
        {
            foreach (var page in Paginas)
            {
                TextBlock block = (TextBlock)page.FindName("pageNumber");
                block.Dispatcher.Invoke(() => block.Text = string.Format("Pagina {0} de {1}", Paginas.IndexOf(page) + 1, Paginas.Count));
            }
        }

        private void Pages_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           if(Pages.IsLoaded)
                UpdateZoomPagesLayout();
        }
        private void UpdateZoomPagesLayout()
        {
            double d = (double)(new LengthConverter().ConvertFrom("21cm"));
            if (Pages.Items.Count > 0 && viewBoxGrid.ActualWidth > viewBox.ViewportWidth * (Zoom / 100))
            {
                Pages.MaxWidth = viewBox.ViewportWidth / (Zoom / 100);
            }
        }

        private void BTN_CLICK_TEXT(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            bool textSelect = false;
            RichTextBox textBox = null;
            for (int i = 0; i < Paginas.Count; i++)
            {
                if(Paginas[i].GetType() == typeof(CartaBodyTemplate) && ((CartaBodyTemplate)Paginas[i]).richtextbox.IsFocused == true)
                {
                    textSelect = true;
                    textBox = ((CartaBodyTemplate)Paginas[i]).richtextbox;
                }
            }

            if(textSelect && textBox != null)
            {
                if(tag == "TEXT_BOLD")
                {
                    object isBold = textBox.Selection.GetPropertyValue(FontWeightProperty);
                    Type isBoldType = isBold.GetType();

                    if (isBoldType == typeof(FontWeight) && (FontWeight)isBold != FontWeights.Bold)
                        textBox.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
                    if (isBoldType == typeof(FontWeight) && (FontWeight)isBold == FontWeights.Bold)
                        textBox.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);

                    if (isBoldType != typeof(FontWeight))
                        textBox.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);

                }
                if (tag == "TEXT_ITALIC")
                {
                    object isItalic = textBox.Selection.GetPropertyValue(FontStyleProperty);
                    Type isItalicType = isItalic.GetType();

                    if (isItalicType == typeof(FontStyle) && (FontStyle)isItalic != FontStyles.Italic)
                        textBox.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
                    if (isItalicType == typeof(FontStyle) && (FontStyle)isItalic == FontStyles.Italic)
                        textBox.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);

                    if (isItalicType != typeof(FontStyle))
                        textBox.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);

                }

            }
            //TextRange text = new TextRange()
        }

        private void TYPE_FONT_SELECT_CHANGED(object sender, SelectionChangedEventArgs e)
        {
            string tag = ((ComboBox)sender).Tag.ToString();

            for (int i = 0; i < Paginas.Count; i++)
            {
                if (Paginas[i].GetType() == typeof(CartaBodyTemplate))
                {
                    if (tag == "FONT_TYPE")
                    {
                        string fontFamily = ((TextBlock)((ComboBoxItem)(((ComboBox)e.OriginalSource).SelectedItem)).Content).Text;
                        ((CartaBodyTemplate)Paginas[i]).richtextbox.Document.SetCurrentValue(FontFamilyProperty, new FontFamily(fontFamily));
                    }
                }
            }
        }
    }
}
