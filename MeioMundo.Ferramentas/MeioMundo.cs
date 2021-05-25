using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MeioMundo.Ferramentas
{
    public class MeioMundoInformation
    {
        public static string DominaçãoSocial { get => "Meio Mundo - Livraria e Papelaria, Lda"; }        
        public static double CapitalSocial { get => 5000; }
        public static string NIF { get => "506849210"; }
        public static string Telefone { get => "238601375"; }
        public static string Email { get => "geral@papelariameiomundo.com"; }
        public static string WebPage { get => "papelariameiomundo.com"; }
    }
    public struct MeioMundoInformationMorada
    {
        public static string Rua { get => "Rua Prof. Antonio R. Garcia Vasconcelos, Lote 40/41"; }
        public static string CodigoPostal { get => "3400-132 Oliveira do Hospital"; }
        public static string Pais { get => "Portugal"; }
    }
}
