using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MeioMundo.Ferramentas.ViewModels;

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
    }
}
