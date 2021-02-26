using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public class EditorExtensions
    {
        public static AnoUX[] GetAnosUX()
        {
            List<AnoUX> anos = new List<AnoUX>();
            foreach (var item in Anos.GetAnos())
            {
                AnoUX ano = new AnoUX();
                ano.ID = item.Key;
                ano.Nome = item.Value;
                anos.Add(ano);
            }
            return anos.ToArray();
        }

        public static DisciplinasUX[] GetDisciplinasUX()
        {
            List<DisciplinasUX> disciplinas = new List<DisciplinasUX>();

            foreach (var item in Disciplinas.GetDisciplinas())
            {
                DisciplinasUX disciplina = new DisciplinasUX();
                disciplina.ID = item.Key;
                disciplina.Nome = item.Value;
                disciplinas.Add(disciplina);
            }

            return disciplinas.ToArray();
        }

        public static Escola[] GetEscolasUX()
        {
            return ManuaisSystem.Escolas.ToArray();
        }
    }

    public class AnoUX
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool IsEnable { get; set; }
    }

    public class DisciplinasUX
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool IsEnable { get; set; }
    }


    public class StringToCharValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value.ToString();
            int charIndex = -1;
            if (int.TryParse(parameter.ToString(), out charIndex) && s.Length > charIndex)
                return s[charIndex];
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class IsAnoCiclo : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = -1;
            bool? b = null;
            if(int.TryParse(value.ToString(), out id))
            {
                if (id % 10 == 0)
                    b = true;
                else
                    b = false;
            }
            if (parameter != null && parameter.ToString() == "IsEnabled")
                b = !b;

            return b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IntToStringConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = -1;
            string nome = "";
            if (int.TryParse(value.ToString(), out id))
            {
                if(parameter != null && parameter.ToString() == "AnoID")
                {
                    nome = Anos.__Anos.First(x => x.Key == id).Value;
                }
            }
            return nome;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IntToBoolConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _int = -1;
            bool? b = null;
            if (int.TryParse(value.ToString(), out _int))
            {
                if (parameter != null)
                {
                    string[] cmd = parameter.ToString().Split('/');
                    //if(cmd[0] == "Escola" && cmd[1] == ">=")
                        //if(value >= )
                }
            }
                
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
