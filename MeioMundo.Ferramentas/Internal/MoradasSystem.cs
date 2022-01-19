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
        public static People[] GetClientes() => Clientes.ToArray();
        public static async Task LoadJson()
        {
            string fileLocation = @"\\srvmm\USR\MeioMundo_Local\Moradas.json";
            try
            {
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
            catch (Exception ex)
            {
                U_System.Debug.Log.LogMessage(ex.Message, typeof(MoradasSystem), U_System.Debug.LogMessageType.Error);
            }
        }
        public static async Task<IEnumerable<People>> LoadClientes()
        {
            string clientesMoradasFileLocation = @"\\srvmm\USR\MeioMundo_Local\Clientes Moradas.txt";
            string username = SettingsManager.Settings.First(x => x.Name == "NETWORK_CREDECIAL_USERNAME").Value;
            string password = SettingsManager.Settings.First(x => x.Name == "NETWORK_CREDECIAL_PASSWORD").Value;
            using (FileStream fileStream = Network.AccessFiles.ReadFile(clientesMoradasFileLocation, username, password))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(1252);
                StreamReader streamReader = new StreamReader(fileStream, encoding);
                return await ReadClientesMoradasAsync(streamReader);
            }
        }
        public static async Task<IEnumerable<People>> LoadFornecedores()
        {
            string fornecedoresMoradasFileLocation = @"\\srvmm\USR\MeioMundo_Local\Fornecedores - Moradas.txt";
            string username = SettingsManager.Settings.First(x => x.Name == "NETWORK_CREDECIAL_USERNAME").Value;
            string password = SettingsManager.Settings.First(x => x.Name == "NETWORK_CREDECIAL_PASSWORD").Value;
            using (FileStream fileStream = Network.AccessFiles.ReadFile(fornecedoresMoradasFileLocation, username, password))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(1252);
                StreamReader streamReader = new StreamReader(fileStream, encoding);
                return await ReadFornecedorMoradasAsync(streamReader);
            }
        }
        private static async Task<IEnumerable<People>> ReadClientesMoradasAsync(StreamReader streamReader)
        {
            List<People> clientes = new List<People>();

            uint INDEX = 0;
            uint INDEX_NOME_CLIENTE = 1;
            uint INDEX_CLIENTE_MORADA_TIPO = 4;
            uint INDEX_CLIENTE_RUA = 5;
            uint INDEX_CLIENTE_LOCALIDADE = 6;
            uint INDEX_CLIENTE_ZIPCODE = 7;
            uint INDEX_CLIENTE_CONCELHO = 8;
            uint INDEX_CLIENTE_DISTRITO = 9;
            uint INDEX_CLIENTE_COUNTRY = 10;

            string _null = "#NULL#";

            Regex Spliter = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            string line = string.Empty;
            TextInfo textInfo = new CultureInfo("pt-PT", false).TextInfo;

            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] values = Spliter.Split(line);
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Replace("\"", "");
                    }
                    Morada morada = new Morada();
                    string nomeCliente = string.Empty;
                    if (values[INDEX_NOME_CLIENTE] != _null)
                        nomeCliente = textInfo.ToTitleCase(values[INDEX_NOME_CLIENTE].ToLower());
                    if (values[INDEX_CLIENTE_RUA] != _null)
                        morada.Rua = values[INDEX_CLIENTE_RUA];
                    if (values[INDEX_CLIENTE_LOCALIDADE] != _null)
                        morada.Localidade = values[INDEX_CLIENTE_LOCALIDADE];
                    if (values[INDEX_CLIENTE_ZIPCODE] != _null)
                        morada.ZipCode = values[INDEX_CLIENTE_ZIPCODE];
                        string tipo = values[INDEX_CLIENTE_MORADA_TIPO];

                    switch (tipo)
                    {
                        case "Devoluções":
                            morada.TipoMorada = TipoMorada.Devoluções;
                            break;
                        default:
                            morada.TipoMorada = TipoMorada.Normal;
                            break;
                    }

                    People cliente = Clientes.FirstOrDefault(x=> x.Nome == nomeCliente);
                    if(cliente == null)
                    {
                        cliente = new People();
                        clientes.Add(cliente);
                        if (values[INDEX] != "#NULL#")
                            cliente.ID = int.Parse(values[INDEX]);
                        cliente.Nome = nomeCliente;
                        cliente.Moradas = new List<Morada>();
                    }
                    cliente.Moradas.Add(morada);

                }
            }

            return Clientes;
        }
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

