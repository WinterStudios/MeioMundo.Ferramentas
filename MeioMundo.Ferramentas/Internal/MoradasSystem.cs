using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.Win32;

using U_System.External;
using U_System.External.Plugin;

using MeioMundo.Ferramentas.Internal.Models;


namespace MeioMundo.Ferramentas.Internal
{
    public class MoradasSystem
    {
        internal static Moradas Moradas { get; set; }
        public static IEnumerable<People> Clientes { get; set; }
        public static IEnumerable<People> Fornecedores { get; set; }

        public static async Task Inicialize()
        {
            await LoadJson();
            Clientes = new List<People>();
            //Fornecedores = new List<People>();
            //await Load();
        }
        public static People[] GetFornecedores() => Fornecedores.ToArray();
        public static async Task LoadJson()
        {
            string fileLocation = @"\\srvmm\USR\MeioMundo_Local\Moradas.json";
            string moradasJson = Network.AccessFiles.ReadJsonFile(fileLocation, "meiomundo", "meiomundo");

            if (!string.IsNullOrEmpty(moradasJson))
            {
                Moradas = JsonSerializer.Deserialize<Moradas>(moradasJson);
            }
            else
            {
                Moradas = new Moradas();
                Clientes = await LoadClientes();
                Fornecedores = await LoadFornecedores();
            }
        }
        public static async Task<IEnumerable<People>> LoadClientes()
        {
            return null;
        }
        public static async Task<IEnumerable<People>> LoadFornecedores()
        {
            string fornecedoresMoradasFileLocation = @"\\srvmm\USR\MeioMundo_Local\Fornecedores - Moradas.txt";            
            using (FileStream fileStream = Network.AccessFiles.ReadFile(fornecedoresMoradasFileLocation, "meiomundo", "meiomundo"))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(1252);
                StreamReader streamReader = new StreamReader(fileStream, encoding);
                return await ReadFornecedorMoradasAsync(streamReader);
            }
        }
        //public static async Task LoadFromFile()
        //{
        //    string peopleFileOutput = string.Empty;
        //    string moradasFileOutput = string.Empty;
        //    OpenFileDialog peopleDialog = new OpenFileDialog();
        //    OpenFileDialog moradasDialog = new OpenFileDialog();
        //    bool loadFornecedore = false;
        //    if (peopleDialog.ShowDialog() == true)
        //    {
        //        peopleFileOutput = peopleDialog.FileName;
        //        loadFornecedore = true;
        //    }
        //    if (loadFornecedore && moradasDialog.ShowDialog() == true)
        //    {
        //        moradasFileOutput = moradasDialog.FileName;
        //    }
        //    else return;


        //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //    Encoding encoding = Encoding.GetEncoding(1252);

        //    StreamReader fornecedorReader = new StreamReader(fornedorFileOutput, encoding, true);
        //    StreamReader moradasReader = new StreamReader(moradasFileOutput, encoding, true);

        //    Morada[] moradas = await ReadFornecedorMoradasAsync(moradasReader);
        //    await ReadFornecedorAsync(fornecedorReader, moradas);
        //    await Save();
        //}
        //private static async Task ReadFornecedorAsync(StreamReader reader, Morada[] moradas)
        //{

        //    int Index = 1;
        //    int _INDEX_ID = 0;
        //    int _INDEX_FORNECEDOR_NOME = 1;
        //    int _INDEX_MORADA = 2;
        //    int _INDEX_LOCALIDADE = 3;
        //    int _INDEX_CONSELHO = 4;
        //    int _INDEX_DISTRITO = 5;
        //    int _INDEX_PAIS = 6;
        //    int _INDEX_CODIGO_POSTAL = 7;
        //    int _INDEX_CONTRIBUINTE = 8;

        //    string line = string.Empty;
        //    Regex Spliter = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        //    TextInfo textInfo = new CultureInfo("pt-PT", false).TextInfo;
        //    while ((line = await reader.ReadLineAsync()) != null)
        //    {
        //        if (!string.IsNullOrEmpty(line))
        //        {
        //            People fornecedor = new People();
        //            string[] _values = Spliter.Split(line);

        //            if (Index < 1)
        //            {

        //            }
        //            else
        //            {
        //                fornecedor.ID = int.Parse(_values[_INDEX_ID]);
        //                fornecedor.Nome = textInfo.ToTitleCase(_values[_INDEX_FORNECEDOR_NOME].Replace("\"", "").ToLower());
        //                fornecedor.Contribuinte = _values[_INDEX_CONTRIBUINTE].Replace("\"", "");
        //                fornecedor.Moradas = moradas.Where(x => x.FornecedorID == fornecedor.ID).ToArray();
        //                for (int i = 0; i < fornecedor.Moradas.Length; i++)
        //                {
        //                    fornecedor.Moradas[i].FornecedorNome = fornecedor.Nome;
        //                }
        //            }

        //            People.Add(fornecedor);
        //            Index++;
        //        }
        //    }
        //    People = People.OrderBy(x => x.ID).ToList();
        //}
        private static async Task<IEnumerable<People>> ReadFornecedorMoradasAsync(StreamReader reader)
        {
            List<People> fornecedores = new List<People>();


            int INDEX = 0;
            int INDEX_MORADA_TITULAR = 1;
            int INDEX_MORADA_TIPO = 4;
            int INDEX_MORADA = 5;
            int INDEX_LOCALIDADE = 6;
            int INDEX_ZIPCODE = 7;
            int INDEX_CONCELHO = 8;
            int INDEX_DISTRITO = 9;
            int INDEX_COUNTRY = 10;

            Regex Spliter = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            string line = string.Empty;
            TextInfo textInfo = new CultureInfo("pt-PT", false).TextInfo;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] values = Spliter.Split(line);
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Replace("\"", "");
                    }
                    Morada morada = new Morada();
                    string moradaTitular = string.Empty;
                    if(values[INDEX_MORADA_TITULAR] != "#NULL#")
                        moradaTitular = textInfo.ToTitleCase(values[INDEX_MORADA_TITULAR].ToLower());
                    if (values[INDEX_MORADA] != "#NULL#")
                        morada.Rua = textInfo.ToTitleCase(values[INDEX_MORADA].ToLower());
                    if(values[INDEX_LOCALIDADE] != "#NULL#")
                        morada.Localidade = textInfo.ToTitleCase(values[INDEX_LOCALIDADE].ToLower());
                    if(values[INDEX_ZIPCODE] != "#NULL#")
                        morada.ZipCode = values[INDEX_ZIPCODE];
                    if(values[INDEX_COUNTRY] != "#NULL#")
                        morada.Country = textInfo.ToTitleCase(values[INDEX_COUNTRY].ToLower());
                    if (values[INDEX_CONCELHO] != "#NULL#")
                        morada.Concelho = textInfo.ToTitleCase(values[INDEX_CONCELHO].ToLower());
                    if (values[INDEX_DISTRITO] != "#NULL#")
                        morada.Distrito = textInfo.ToTitleCase(values[INDEX_DISTRITO].ToLower());
                    if (values[INDEX_MORADA_TIPO] != "#NULL#")
                    {
                        string v = values[INDEX_MORADA_TIPO];
                        if (v == "Devoluções")
                            morada.TipoMorada = TipoMorada.Devoluções;
                    }
                    else
                        morada.TipoMorada = TipoMorada.Normal;

                    var fornecedor = fornecedores.FirstOrDefault(x => x.Nome == moradaTitular);
                    if (fornecedor == null)
                    {
                        fornecedor = new People();
                        fornecedores.Add(fornecedor);
                        if (values[INDEX] != "#NULL#")
                            fornecedor.ID = int.Parse(values[INDEX]);
                        fornecedor.Nome = moradaTitular;
                        fornecedor.Moradas = new List<Morada>();
                    }
                    fornecedor.Moradas.Add(morada);

                }
            }
            return fornecedores;
        }

        public static async Task Save()
        {
            string fileLocation = string.Format("{0}People.json", EntryPoint.Info.PluginStorageData);

            JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            //string json = JsonSerializer.Serialize(People, serializerOptions);

            //await File.WriteAllTextAsync(fileLocation, json);
        }
        private static async Task Load()
        {
            string fileLocation = @"\\srvmm\USR\MeioMundo_Local\People.json"; // string.Format("{0}Fornecedores.json", EntryPoint.Info.PluginStorageData);

            JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            FileStream stream = Network.AccessFiles.ReadFile(fileLocation, "meiomundo", "meiomundo");// new FileStream(fileLocation, FileMode.Open);

            //People = (await JsonSerializer.DeserializeAsync<People[]>(stream, serializerOptions)).ToList();

        }
    }
}

