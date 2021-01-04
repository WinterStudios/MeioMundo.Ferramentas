using System;
using System.Collections.Generic;
using System.Text;
using U_System.API;
using U_System.API.Plugins;

namespace MeioMundo.Ferramentas
{
    public class EntryPoint : IPlugin
    {
        public string Name => "Meio Mundo - Ferramentas";
        public string Description => "Ferramentas para a Meio Mundo";
        public SemVersion Version => throw new NotImplementedException();
        public Module[] Modules => new Module[] { 
            new Module() {
                Name = "Codigo de Barras",
                Type = typeof(UI.Barcode).FullName,
                Path = "Ferramentas>Codigos de Barras",
                PluginType = PluginType.Tab
            }
        };
          
    }
}
