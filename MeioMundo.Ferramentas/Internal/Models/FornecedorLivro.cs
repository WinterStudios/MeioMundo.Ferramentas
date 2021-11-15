using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Internal.Models
{
    public class FornecedorLivro
    {
        public int ID { get; set; }
        public string Fornecedor { get; set; }
        public string FornecedorIcon { get; set; }
        public List<Editora> Editoras { get; set; }

    }
}
