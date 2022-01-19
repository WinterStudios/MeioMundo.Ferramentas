using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using MeioMundo.Ferramentas.ViewModels;

namespace MeioMundo.Ferramentas
{
    internal class SettingsManager
    {
        private static string SettingsFileLocation { get => $"{U_System.External.Paths.Settings}\\MeioMundo.json"; }
        public static List<Setting> Settings { get; set; }
        public static async Task Start()
        {
            await Load();
            if (Settings == null)
                await LoadDefault();
        }

        private static async Task Load()
        {

        }

        private static async Task LoadDefault()
        {
            Settings = new List<Setting>();
            Settings.Add(new Setting() { Name = "NETWORK_CREDENTIAL_USERNAME", Value = "meiomundo", _Type = SettingType.Network_Credencials });
            Settings.Add(new Setting() { Name = "NETWORK_CREDENTIAL_PASSWORD", Value = "meiomundo", _Type = SettingType.Network_Credencials });

            await Save();
        }
        private static async Task Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(Settings.ToArray(), typeof(Setting[]), new JsonSerializerOptions() { WriteIndented = true });
                await File.WriteAllTextAsync(SettingsFileLocation, json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
