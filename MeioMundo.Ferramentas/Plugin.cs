using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using U_System.External;
using U_System.External.Plugin;

namespace MeioMundo.Ferramentas
{
    public class EntryPoint : IPlugin
    {
        public ResourceDictionary Icons { get 
            {
                if(icons == null)
                {
                    icons = new ResourceDictionary();
                    icons.Source = new Uri("pack://application:,,,/MeioMundo.Ferramentas;component/Resources/ModernIcons.xaml", UriKind.Absolute);   
                }
                return icons;
            }
            set => icons = value; }
        private ResourceDictionary icons;


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
                Icon = Icons["ICON.Barcode"],
                Shortcut = "Ctrl + B"
            },
            new Module() {
                Name = "Manuais Escolares",
                Type = typeof(Escola.ManuaisEscolares).FullName,
                Path = "Ferramentas>Manuais Escolares",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                TabIconLocations = "/MeioMundo.Ferramentas;component/Assets/books.png",
                Icon = Icons["ICON.ManuaisEscolares"],
            },
            new Module() {
                Name = "Site",
                Type = typeof(Site.Core).FullName,
                Path = "Ferramentas>Site>Gestão de Stocks",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                TabIconLocations = "/MeioMundo.Ferramentas;component/Assets/globe.png",
                Icon = Icons["ICON.Site.ManagerStock"]
            },
            new Module() {
                Name = "Correio",
                Type = typeof(Correio.Main).FullName,
                Path = "Ferramentas>Correio",
                Icon = Icons["ICON.Mail"],
                PluginTypeBehavior = PluginTypeBehavior.Tab,
            },
            new Module()
            {
                Name = "Plugin Manager",
                Type = typeof(Internal.PluginManager).FullName,
                Path = "Ferramentas>Plugin Manager",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                Icon = "pack://application:,,,/MeioMundo.Ferramentas;component/Assets/Icons/plugin.png"
            },
            new Module()
            {
                Name = "Creador de Carta",
                Type = typeof(Correio.CartaCreator).FullName,
                Path = "Ferramentas>Criar uma Carta",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                Icon = "pack://application:,,,/MeioMundo.Ferramentas;component/Assets/Icons/plugin.png"
            },
            new Module()
            {
                Name = "Voucher",
                Type = typeof(Voucher.VoucherManager).FullName,
                Path = "Ferramentas>Voucher",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                Icon = "pack://application:,,,/MeioMundo.Ferramentas;component/Assets/Icons/plugin.png"
            },
            new Module()
            {
                Name = "Emição de Voucheres",
                Type = typeof(Voucher.VoucherEmition).FullName,
                Path = "Ferramentas>Emição de Voucheres",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
                Icon = "pack://application:,,,/MeioMundo.Ferramentas;component/Assets/Icons/plugin.png"
            },
            new Module()
            {
                Name = "Criar Artigos",
                Type = "",
                Path = "Ferramentas>Site>Inportar Produtos",
                PluginTypeBehavior = PluginTypeBehavior.Tab,
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
        public string[] PluginDependicy { get => new string[0]; }

        public void initialization()
        {
            
            AppContext.SetSwitch(@"Switch.System.Windows.Controls.DoNotAugmentWordBreakingUsingSpeller", true);
            //Internal.Net.MeioMundoServer.Inicialize();
            Internal.FornecedorSystem.Inicialize();
            try
            {
                Voucher.VoucherSystem.Inicialize();
            }
            catch 
            {
                var exd = 0;
            }
        }
    }
}
