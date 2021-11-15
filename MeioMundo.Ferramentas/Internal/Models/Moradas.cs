using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Internal.Models
{
    public class Moradas
    {
        public DateTime ClientesLastModifyLocal { get; set; }
        public DateTime FornecedoresLastModifyLocal { get; set; }
        public People[] Clientes { get; set; }
        public People[] Fornecedores { get; set; }       
    }
}
