using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public interface ModeloInfo
    {
        public string Version { get; }
        public string Escola { get; set; }
        public Ano Ano { get; set; }
        public UserControl __UserControl { get; }
        public void Run();
    }
}
