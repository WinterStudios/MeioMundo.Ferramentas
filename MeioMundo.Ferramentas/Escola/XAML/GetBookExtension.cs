using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeioMundo.Ferramentas.Escola.XAML
{
    public class GetBookByIDExtension : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int valueID = -1;
            if (value == null)
                return string.Empty;
            try
            {
                if (int.TryParse(value.ToString(), out valueID))
                    if (parameter != null && valueID > 0)
                    {
                        var book = ManuaisSystem.Livros.FirstOrDefault(x => x.ID == valueID);
                        if (book == null)
                            return string.Empty;

                        if (parameter.ToString() == "Livro_ISBN")
                            return book.ISBN.ToString("### ### ### ###-#");
                        if (parameter.ToString() == "Livro_Nome")
                            return book.Nome;
                        if (parameter.ToString() == "Livro_Autor")
                            return book.Autor;
                        if (parameter.ToString() == "Livro_Editora")
                            return book.Editora;
                        if (parameter.ToString() == "Livro_Disc.Geral")
                            return book.Disciplina;


                        return "";
                    }
                return "";
            }
            catch
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
