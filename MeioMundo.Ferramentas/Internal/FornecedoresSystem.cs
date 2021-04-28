using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace MeioMundo.Ferramentas.Internal
{
    public class FornecedoresSystem
    {
        public static Fornecedor[] Fornecedores { get; set; }


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


            StreamReader fornecedorReader = new StreamReader(fornedorFileOutput);
            StreamReader moradasReader = new StreamReader(moradasFileOutput);

        }
        private static async Task ReadFornecedorAsync(StreamReader reader)
        {
            int Index = 0;
            int ID_Index = 0;


            string line = string.Empty;
            while((line = await reader.ReadLineAsync()) != null)
            {
                if(Index == 0)
                {

                }
                else
                {

                }
                Index++;
            }
        }        

    }
}

