using System;
using System.Collections.Generic;
using System.Text;
using U_System.External;
using U_System.External.Plugin;

namespace MeioMundo.Ferramentas
{
    public class EntryPoint : IPlugin
    {
        public static string DataLocation { get; private set; }
        public string Name => "Meio Mundo - Ferramentas";
        public string Description => "Ferramentas para a Meio Mundo";
        public SemVersion Version => throw new NotImplementedException();
        public Module[] Modules => new Module[] { 
            new Module() {
                Name = "Codigo de Barras",
                Type = typeof(UI.Barcode).FullName,
                Path = "Ferramentas>Codigos de Barras",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                TabIconLocations = "/MeioMundo.Ferramentas;component/Assets/barcode.png",
                Icon = "pack://application:,,,/MeioMundo.Ferramentas;component/Assets/Icons/barcode.png",
                Shortcut = "Ctrl + B"
            },
            new Module() {
                Name = "Manuais Escolares",
                Type = typeof(Escola.ManuaisEscolares).FullName,
                Path = "Ferramentas>Manuais Escolares",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                TabIconLocations = "/MeioMundo.Ferramentas;component/Assets/books.png",
                Icon = "pack://application:,,,/MeioMundo.Ferramentas;component/Assets/Icons/book.png",
            },
            new Module() {
                Name = "Site",
                Type = typeof(Site.Core).FullName,
                Path = "Ferramentas>Site",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                TabIconLocations = "/MeioMundo.Ferramentas;component/Assets/globe.png",
                Icon = "pack://application:,,,/MeioMundo.Ferramentas;component/Assets/Icons/barcode.png",
            },
            new Module() {
                Name = "Correio",
                Type = typeof(Correio.Main).FullName,
                Path = "Ferramentas>Correio",
                Icon = "pack://application:,,,/MeioMundo.Ferramentas;component/Assets/Icons/envelope.png",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
            },
            new Module()
            {
                Name = "Plugin Manager",
                Type = typeof(Internal.PluginManager).FullName,
                Path = "Ferramentas>Plugin Manager",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                Icon = "pack://application:,,,/MeioMundo.Ferramentas;component/Assets/Icons/plugin.png"
            }
        };
        public bool ShowWelcomePage => true;
        public Module WelcomePage => new Module() {
            Name = "Welcome",
            Type = typeof(WelcomePage).FullName,
            Path = "Ferramentas>Welcome",
            PluginTypeBehavior = PluginTypeBehavior.Tab
        };

        public string DataStorage { get => DataLocation; set => DataLocation = value; }
        public PluginInfo PluginInformation { get => Info; set => Info = value; }
        public static PluginInfo Info { get; set; }
        public void initialization()
        {
            Internal.FornecedorSystem.Inicialize();
        }
    }
}
