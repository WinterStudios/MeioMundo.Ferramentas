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

namespace MeioMundo.Ferramentas.Escola.Internal.Modelos
{
    /// <summary>
    /// Interaction logic for ModeloV2.xaml
    /// </summary>
    public partial class Modelo_v20_08 : UserControl
    {
        //public Ensino TipoEnsino { get; set; }
        //public List<Disciplina> DisciplinasGerais { get; set; }
        public Modelo_v20_08()
        {
            //DisciplinasGerais = new List<Disciplina>();
            
            
            InitializeComponent();
            //UC_DataGrid_CG.ItemsSource = DisciplinasGerais;

            //DisciplinasGerais.Add(new Disciplina { ID = 0, Nome = "Portuges" });
        }
    }
}