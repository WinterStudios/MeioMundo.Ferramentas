using System;
using System.Collections.Generic;
using System.Text;
using U_System.External;
using U_System.External.Plugin;

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
                PluginTypeBehavior = PluginTypeBehavior.Tab
            },
            new Module() {
                Name = "Manuais Escolares",
                Type = typeof(Escola.ManuaisEscolares).FullName,
                Path = "Ferramentas>Manuais Escolares",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                TabIconLocations = "/MeioMundo.Ferramentas;component/Assets/books.png"
            },
            new Module() {
                Name = "Site",
                Type = typeof(Site.Core).FullName,
                Path = "Ferramentas>Site",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                TabIconLocations = "/MeioMundo.Ferramentas;component/Assets/globe.png"
            }
        };
        public bool ShowWelcomePage => true;
        public Module WelcomePage => new Module() {
            Name = "Welcome",
            Type = typeof(WelcomePage).FullName,
            Path = "Ferramentas>Welcome",
            PluginTypeBehavior = PluginTypeBehavior.Tab
        };
    }
}
