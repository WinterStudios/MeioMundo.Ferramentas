﻿using MeioMundo.Ferramentas.Escola.Internal;
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

namespace MeioMundo.Ferramentas.Escola.Modelos
{
    /// <summary>
    /// Interaction logic for Modelo_2020_07.xaml
    /// </summary>
    public partial class Modelo_2021_06 : UserControl, ModeloInfo
    {
        public Modelo_2021_06()
        {
            InitializeComponent();
        }

        public string Version => "v.2021.06";
        public string Escola { get; set; }
        public Ano Ano { get; set; }
        public UserControl __UserControl => this;

        public Ciclo _Ciclo { get => _ciclo; set { _ciclo = value; } }
        private Ciclo _ciclo;

        private int _DisciplinasGerais
        {
            get
            {
                switch (_Ciclo)
                {
                    case Ciclo.Primaria:
                        return 5;
                    case Ciclo.Basico:
                        return 15;
                    case Ciclo.Secundario:
                        return 9;
                    default:
                        return 0;

                }
            }
        }

        public void Run()
        {
            _Ciclo = Ciclo.Basico;
            if (Ano.ID.ToString()[0] == '1')
                _Ciclo = Ciclo.Primaria;
            if (Ano.ID.ToString()[0] == '4')
                _Ciclo = Ciclo.Secundario;

            SetUI(_Ciclo);
        }


        /// <summary>
        /// 
        /// </summary>
        public void LoadDisciplinas()
        {
            List<Internal.Disciplina> __Disc_CG = new List<Disciplina>(Ano.Disciplinas.Where(x => x.Disc_Espe == false));
            List<Internal.Disciplina> __Disc_CE = new List<Disciplina>(Ano.Disciplinas.Where(x => x.Disc_Espe == true));
            for (int i = __Disc_CG.Count; i < _DisciplinasGerais; i++)
            {
                __Disc_CG.Add(new Disciplina());
            }
            for (int i = __Disc_CE.Count; i < 6; i++)
            {
                __Disc_CE.Add(new Disciplina());
            }
            UC_DISC_COMP_GERAL.ItemsSource = __Disc_CG;
            UC_DISC_COMP_ESPEC.ItemsSource = __Disc_CE;
        }

        public void SetUI(Ciclo ciclo)
        {
            UC_TextBox_Ano.Text = Ano.ToString();
            UC_TextBox_Escola.Text = Escola;
            if (ciclo == Ciclo.Primaria)
            {
                UC_STACKPANEL_COMP_ESPECIFICA.Visibility = Visibility.Collapsed;
            }
            if (ciclo == Ciclo.Basico)
            {
                UC_STACKPANEL_COMP_ESPECIFICA.Visibility = Visibility.Collapsed;
            }
            if (ciclo == Ciclo.Secundario)
            {
                UC_STACKPANEL_COMP_ESPECIFICA.Visibility = Visibility.Visible;
            }
            LoadMatricula();
            LoadDisciplinas();
        }
        public void LoadMatricula()
        {
            if (_Ciclo == Ciclo.Primaria)
            {
                for (int i = 0; i < 3; i++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = (VisualBrush)this.FindResource("Color.Brush.Pattern.Right");
                    rectangle.Height = (double)new LengthConverter().ConvertFrom("0,2cm");
                    double margin = (double)new LengthConverter().ConvertFrom("0,35cm");
                    rectangle.Margin = new Thickness(0, margin, 0, margin);
                    UC_StackPanel_Matriculas.Children.Add(rectangle);
                    Modelo_2021_06_Escolha_Primaria escolha = new Modelo_2021_06_Escolha_Primaria();

                    Disciplina[] disc = new Disciplina[5];
                    for (int z = 0; z < 5; z++)
                    {
                        if (z < Ano.Disciplinas.Count)
                            disc[z] = Ano.Disciplinas[z];
                        else
                        {
                            disc[z] = new Disciplina();
                        }
                            
                    }

                    escolha.UC_ListBox_Discplinas_CG.ItemsSource = disc;
                    UC_StackPanel_Matriculas.Children.Add(escolha);
                    escolha.__CICLO = Ciclo.Primaria;
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = (VisualBrush)this.FindResource("Color.Brush.Pattern.Right");
                    rectangle.Height = (double)new LengthConverter().ConvertFrom("0,2cm");
                    double margin = (double)new LengthConverter().ConvertFrom("0,2cm");
                    rectangle.Margin = new Thickness(0, margin, 0, margin);
                    UC_StackPanel_Matriculas.Children.Add(rectangle);
                    Modelo_2021_06_Box escolha = new Modelo_2021_06_Box();

                    if(_Ciclo == Ciclo.Basico)
                    {
                        int discLenghtDG = 17;
                        Disciplina[] discG = new Disciplina[discLenghtDG];
                        for (int z = 0; z < discLenghtDG; z++)
                        {
                            if (z < Ano.Disciplinas.Count)
                                discG[z] = Ano.Disciplinas[z];
                            else
                            {
                                discG[z] = new Disciplina();
                            }

                        }
                        escolha.UC_ListBox_Discplinas_CG.ItemsSource = discG;
                        UC_StackPanel_Matriculas.Children.Add(escolha);
                            escolha.__CICLO = Ciclo.Basico;
                    }
                    else
                    {
                        int discLenghtDG = 9;
                        int discLenghtCE = 7;
                        List<Disciplina> discG = Ano.Disciplinas.Where(x => x.Disc_Espe == false).ToList();
                        List<Disciplina> discE = Ano.Disciplinas.Where(x => x.Disc_Espe == true).ToList();
                        for (int z = discG.Count; z < discLenghtDG; z++)
                        {
                            discG.Add(new Disciplina());
                        }
                        for (int z = discE.Count; z < discLenghtCE; z++)
                        {
                            discE.Add(new Disciplina());
                        }
                        escolha.UC_ListBox_Discplinas_CG.ItemsSource = discG;
                        escolha.UC_ListBox_Discplinas_CE.ItemsSource = discE;
                        UC_StackPanel_Matriculas.Children.Add(escolha);
                        escolha.__CICLO = Ciclo.Secundario;
                    }

                }
            }
        }
    }
}
