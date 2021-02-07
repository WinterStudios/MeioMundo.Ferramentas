using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Escola
{
    public class ManuaisSystem
    {
        public static ObservableCollection<Internal.Escola> Escolas { get; set; }
        
        private static string DataLocationFolder { get {
                string pluginAssemblyDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace("\\","/");
                string pluginDataDirectory = pluginAssemblyDirectory;
                if (!System.IO.Directory.Exists(pluginDataDirectory + "/Data/MeioMundo.Ferramentas/Escola"))
                    System.IO.Directory.CreateDirectory(pluginDataDirectory + "/Data/MeioMundo.Ferramentas/Escola");
                return pluginDataDirectory + "/Data/MeioMundo.Ferramentas/Escola/";
            } }

        public static void Inicialize()
        {
            LoadEscolas();
        }

        public static void LoadEscolas()
        {
            string escolaFile = DataLocationFolder + "Escolas.json";
            if (System.IO.File.Exists(escolaFile))
            {
                string json = System.IO.File.ReadAllText(escolaFile);
                Escolas = new ObservableCollection<Internal.Escola>(System.Text.Json.JsonSerializer.Deserialize<Internal.Escola[]>(json).ToList());
            }

        }

    }
}
