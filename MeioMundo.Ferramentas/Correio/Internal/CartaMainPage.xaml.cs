﻿using System;
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

namespace MeioMundo.Ferramentas.Correio.Internal
{
    /// <summary>
    /// Interaction logic for CartaMainPage.xaml
    /// </summary>
    public partial class CartaMainPage : UserControl
    {
        public CartaMainPage()
        {
            InitializeComponent();
            dateTimeToday.Text = DateTime.Today.ToLongDateString();
        }
    }
}
