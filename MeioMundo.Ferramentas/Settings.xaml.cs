using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeioMundo.Ferramentas
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public static ViewModels.Setting Setting { get; set; }



        private static string SettingsFileLocation { get => $"{U_System.External.Paths.Settings}\\MeioMundo.json"; }
        internal static async Task Load()
        {
            if (File.Exists(SettingsFileLocation))
                using(FileStream fileStream = File.OpenRead(SettingsFileLocation))
                    Setting = await JsonSerializer.DeserializeAsync<ViewModels.Setting>(fileStream);
            else
            {
                Setting = new ViewModels.Setting();
                using (FileStream fs = File.Open(SettingsFileLocation, FileMode.OpenOrCreate))
                    await JsonSerializer.SerializeAsync(fs, Setting, new JsonSerializerOptions() { WriteIndented = true });
            }
        }
        public Settings()
        {
            InitializeComponent();
            ListSettings.ItemsSource = SettingsManager.Settings;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListSettings.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("_Type");
            view.GroupDescriptions.Add(groupDescription);
        }
    }
}
