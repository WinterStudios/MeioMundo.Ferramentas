using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MeioMundo.Ferramentas.ViewModels;
using System.Text.RegularExpressions;

namespace MeioMundo.Ferramentas.Systems
{
    public class Stock
    {
        public static async Task<List<Produto>> GetProdutosAsync()
        {            

            List<Produto> produtos = new List<Produto>();
            string serverPath = @"srvmm";
            using (Network.NetworkShareAccesser.Access(serverPath, "meiomundo", "meiomundo"))
            {
                string filePath = @"\\Srvmm\USR\MeioMundo_Local\Listagem de Artigos _EUROS_.TXT";
                FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                MemoryStream ms = new MemoryStream();
                int b = 0;
                byte[] buffer = new byte[1024 * 2];
                while((b = await file.ReadAsync(buffer ,0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, buffer.Length);
                }
                file.Close();
                ms.Position = 0;                    // -> Its requerired to reader start from the start. The position its at the end, so 
                                                    //    when the streamreader start read it start from the end and return nothing.

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(1252);

                StreamReader reader = new StreamReader(ms);

                string line = string.Empty;
                int index = 0;

                //Index Colluns
                int RefIndex = 0;
                int NomeIndex = 1;
                int PvpIndex = 2;
                int IvaIndex = 5;
                int StockIndex = 3;
                int CreationDate = 6;
                int CodigoBarraExternal = 7;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] cols = line.Split(',');
                    if (cols.Length > 5)
                    {
                        if (index == 0)
                        {
                            //RefIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "REF");
                            //NomeIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "PRODUTO");
                            //PvpIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "PVP");
                            //IvaIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "IMPOSTO");
                            //StockIndex = cols.ToList().FindIndex(0, cols.Length, x => x == "DISPONÍVEL");
                        }
                        else
                        {
                            Produto p = new Produto();
                            p.Ref = cols[RefIndex].Replace("\"", "");
                            p.Nome = cols[NomeIndex].Replace("\"", "");
                            float preco = float.NaN;
                            if (float.TryParse(cols[PvpIndex].Replace('.', ','), out preco))
                                p.Preco_cIVA = preco;
                            p.Imposto = TipoImposto.Parse(cols[IvaIndex]);
                            int stockSage = 0;
                            if (int.TryParse(cols[StockIndex], out stockSage))
                                p.Stock = stockSage;

                            if (DateTime.TryParse(cols[CreationDate], out _))
                                p.CreationDate = DateTime.Parse(cols[CreationDate]);

                            produtos.Add(p);
                        }
                        index++;
                    }
                }
            }
            return produtos;
        }

        public static IEnumerable<Produto> GetProdutosWebSiteAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                return ReadWebFile(openFileDialog.FileName);
            }
            else return new Produto[0];
        }
        private static IEnumerable<Produto> ReadWebFile(string path)
        {
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(stream);

                List<Produto> produtos = new List<Produto>();

                string line = string.Empty;
                int index = 0;

                //Index Colluns
                int RefIndex = 0;
                int NomeIndex = 1;
                int PvpIndex = 2;
                int IvaIndex = 5;
                int StockIndex = 3;

                while ((line = reader.ReadLine()) != null)
                {
                    //Define pattern
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                    //Separating columns to array
                    string[] cols = CSVParser.Split(line);

                    if(index == 0)
                    {
                        var headers = cols.ToList();
                        RefIndex = headers.FindIndex(x => x == "REF");
                        NomeIndex = headers.FindIndex(x => x == "Nome");
                        IvaIndex = headers.FindIndex(x => x == "\"Classe de imposto\"");
                        StockIndex = headers.FindIndex(x => x == "Stock");
                        PvpIndex = headers.FindIndex(x => x == "\"Preço normal\"");

                        index++;
                        continue;
                    }

                    Produto produto = new Produto();
                    produto.Ref = cols[RefIndex];
                    produto.Nome = cols[NomeIndex].Replace("\"","");

                    produto.Stock = cols[StockIndex].TryParseToInt();
                    produto.Preco_cIVA = cols[PvpIndex].TryParseToFloat();

                    produto.Imposto = TipoImposto.TryParse(cols[IvaIndex]);
                   
                    
                    
                    

                    produtos.Add(produto);
                    index++;
                }
                return produtos;
            }
        }
    }
    public static class Extensions
    {
        public static float TryParseToFloat(this string s)
        {
            float f = 0;
            bool b = float.TryParse(s, out f);
            if (!b)
                U_System.Debug.Log.LogMessage($"Convert Fail to int:{s} ", typeof(Extensions), U_System.Debug.LogMessageType.Warning);
            return 0f;
        }       
        public static int TryParseToInt(this string s)
        {
            int i = 0;
            bool b = int.TryParse(s, out i);
            if (!b)
                U_System.Debug.Log.LogMessage($"Convert Fail to int:{s} ", typeof(Extensions) ,U_System.Debug.LogMessageType.Warning);
            return i;
        }
    }
    
}
