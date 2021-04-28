using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

using U_System.External;
using U_System.External.Plugin;

namespace MeioMundo.Ferramentas.Internal
{
    public class FornecedoresSystem
    {
        public static List<Fornecedor> Fornecedores { get; set; }

        public static void Inicialize()
        {
            Fornecedores = new List<Fornecedor>();
            LoadFromFile();
            string lo = EntryPoint.DataLocation;
        }


        public static void LoadFromFile()
        {
            string fornedorFileOutput = string.Empty;
            string moradasFileOutput = string.Empty;
            OpenFileDialog fornecedorDialog = new OpenFileDialog();
            OpenFileDialog moradasDialog = new OpenFileDialog();
            bool loadFornecedore = false;
            if (fornecedorDialog.ShowDialog() == true)
            {
                fornedorFileOutput = fornecedorDialog.FileName;
                loadFornecedore = true;
            }
            if (loadFornecedore && moradasDialog.ShowDialog() == true)
            {
                moradasFileOutput = moradasDialog.FileName;
            }
            else return;


            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding(1252);

            StreamReader fornecedorReader = new StreamReader(fornedorFileOutput, encoding, true);
            StreamReader moradasReader = new StreamReader(moradasFileOutput, encoding, true);

            ReadFornecedorAsync(fornecedorReader);

        }
        private static async Task ReadFornecedorAsync(StreamReader reader)
        {
            int Index = 1;
            int ID_Index = 0;
            int ID_ForncederNome = 1;

            string line = string.Empty;



            while((line = await reader.ReadLineAsync()) != null)
            {

                Fornecedor fornecedor = new Fornecedor();

                string[] _values = line.Split(',');

                if(Index < 1)
                {

                }
                else
                {
                    fornecedor.ID = int.Parse(_values[ID_Index]);
                    fornecedor.Nome = _values[ID_ForncederNome].Replace("\"", "");
                }

                Fornecedores.Add(fornecedor);
                Index++;
            }
            Fornecedores = Fornecedores.OrderBy(x => x.ID).ToList();
        }        

    }
}

