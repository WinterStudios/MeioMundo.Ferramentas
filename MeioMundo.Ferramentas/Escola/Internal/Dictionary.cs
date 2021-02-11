using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public class Anos
    {
        public static Dictionary<int, string> GetAnos()
        {
            Dictionary<int, string> keys = new Dictionary<int, string>();
            keys.Add(10, "1º Ciclo");
            keys.Add(11, "1º Ano");
            keys.Add(12, "2º Ano");
            keys.Add(13, "3º Ano");
            keys.Add(14, "4º Ano");

            keys.Add(20, "2º Ciclo");
            keys.Add(21, "5º Ano");
            keys.Add(22, "6º Ano");

            keys.Add(30, "3º Ciclo");
            keys.Add(31, "7º Ano");
            keys.Add(32, "8º Ano");
            keys.Add(33, "9º Ano");

            // Secundario
            keys.Add(400, "10º Ano");
            keys.Add(401, "10º Ano - Cientifico");
            keys.Add(402, "10º Ano - Humanidades");
            keys.Add(403, "10º Ano - Arte");
            keys.Add(404, "10º Ano - Economia");

            keys.Add(410, "11º Ano");
            keys.Add(411, "11º Ano - Cientifico");
            keys.Add(412, "11º Ano - Humanidades");
            keys.Add(413, "11º Ano - Arte");
            keys.Add(414, "11º Ano - Economia");

            keys.Add(420, "12º Ano");
            keys.Add(421, "12º Ano - Cientifico");
            keys.Add(422, "12º Ano - Humanidades");
            keys.Add(423, "12º Ano - Arte");
            keys.Add(424, "12º Ano - Economia");

            return keys;
        }
    }
    public class AnosDictionary
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool IsSelect { get; set; }
    }
}
