using MeioMundo.Ferramentas.Internal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Internal
{
    public class ProdutosSage
    {
        public static List<Internal.Models.Produto> Produtos
        {
            get { return m_produtos; }
            set { m_produtos = value; }
        }

        private static List<Internal.Models.Produto> m_produtos;


        public static async Task<Models.Produto[]> GetProdutosAsync()
        {
            List<Models.Produto> produtos = new List<Models.Produto>();

            string serverPath = @"srvmm";
            using (Network.NetworkShareAccesser.Access(serverPath, "meiomundo", "meiomundo"))
            {
                string filePath = @"\\Srvmm\USR\MeioMundo_Local\Listagem de Artigos _EUROS_.TXT";
                FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(1252);

                StreamReader reader = new StreamReader(file);

                string line = string.Empty;
                int index = 0;
                //Index Colluns
                int RefIndex = 0;
                int NomeIndex = 1;
                int PvpIndex = 2;
                int IvaIndex = 5;
                int StockIndex = 3;

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
                            Models.Produto p = new Models.Produto();
                            p.REF = cols[RefIndex].Replace("\"", "");
                            p.Nome = cols[NomeIndex].Replace("\"", "");
                            float preco = float.NaN;
                            if (float.TryParse(cols[PvpIndex].Replace('.', ','), out preco))
                                p.Preco_cIVA = preco;
                            p.TaxaImposto = Produto.SetTaxaImposto(cols[IvaIndex]);
                            p.Preco_sIVA = p.Preco_cIVA / (1 + p.TaxaIVA);
                            int stockSage = 0;
                            if (int.TryParse(cols[StockIndex], out stockSage))
                                p.StockSage = stockSage;

                            produtos.Add(p);
                        }
                        index++;
                    }

                }
            }
            return produtos.ToArray();
        }
    }
}
