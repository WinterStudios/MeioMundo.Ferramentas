using MeioMundo.Ferramentas.Escola.Internal;
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
  /// Interaction logic for Modelo_2021.xaml
  /// </summary>
  public partial class Modelo_2021_06_Outro : UserControl, ModeloInfo
  {

    public string Version => "v2021.06";
    public string Escola { get; set; }

    private int _DisciplinasGerais { get; set; } = 10;
    public Ano Ano { get; set; }

    public UserControl __UserControl => this;

    public Disciplina[] Disciplinas { get; set; }


    public void Run()
    {
      LoadDisciplinas();
    }
    public Modelo_2021_06_Outro()
    {
      this.DataContext = this;
      InitializeComponent();
      string[] disciplinas = Internal.Disciplinas.GetDisciplinas().Values.Where(x => !string.IsNullOrEmpty(x)).ToArray();
      //UC_ItemControl_Disciplinas.ItemsSource = disciplinas;
      
    }

    public void LoadDisciplinas()
    {
      List<Internal.Disciplina> __Disc_CG = new List<Disciplina>(Ano.Disciplinas.Where(x => x.Disc_Espe == false));
      for (int i = __Disc_CG.Count; i < _DisciplinasGerais; i++)
      {
        __Disc_CG.Add(new Disciplina());
      }

      UC_DISC_COMP_GERAL.ItemsSource = __Disc_CG;
      UC_ItemControl_Disciplinas.ItemsSource = __Disc_CG;
      UC_ItemControl_Disciplinas_Copy.ItemsSource = __Disc_CG;

      Disciplinas = __Disc_CG.ToArray();
    }
    public void SetEscola(string escola)
    {
      //UC_TextBox_Escola.Text = escola;
    }

  }
}
