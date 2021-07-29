using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Escola.Internal
{
    public static class Anos
    {

        public static Dictionary<int, string> __Anos
        {
            get
            {
                Dictionary<int, string> keys = new Dictionary<int, string>();

                // 1º Ciclo
                keys.Add(10, "1º Ciclo");
                keys.Add(11, "1º Ano");
                keys.Add(12, "2º Ano");
                keys.Add(13, "3º Ano");
                keys.Add(14, "4º Ano");

                // 2º Ciclo
                keys.Add(20, "2º Ciclo");
                keys.Add(21, "5º Ano");
                keys.Add(22, "6º Ano");

                // 3º Ciclo
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
        public static Dictionary<int, string> GetAnos() => __Anos;
    }

    public static class Disciplinas
    {
        public static Dictionary<int, string> __Disciplinas
        {
            get
            {
                Dictionary<int, string> keys = new Dictionary<int, string>();
                keys.Add(0, "");

                keys.Add(10, "Português");
                keys.Add(11, "Inglês");
                keys.Add(12, "Francês");
                keys.Add(13, "Espanhol");
                keys.Add(14, "Alemão");
                keys.Add(15, "Italiano");
                keys.Add(16, "Mandarim");

                keys.Add(26, "Literatura Portuguesa");
                keys.Add(27, "Filosofia A");
                keys.Add(28, "Psicologia B");
                keys.Add(29, "Sociologia");


                keys.Add(30, "Matemática");
                keys.Add(31, "Matemática A");
                keys.Add(32, "Matemática B");
                keys.Add(33, "MACS");
                keys.Add(36, "Economia A");
                keys.Add(38, "Economia C");


                keys.Add(40, "Estudo do Meio");
                keys.Add(41, "Ciências Naturais");
                keys.Add(42, "Ciências Físico-Químicas");
                keys.Add(43, "Biologia e Geologia");
                keys.Add(44, "Física A");
                keys.Add(45, "Química A");
                keys.Add(46, "Biologia");

                keys.Add(50, "Educação Visual");
                keys.Add(51, "Educação Tecnológica");
                keys.Add(52, "Desenho A");
                keys.Add(53, "Geometria Descritiva A");


                keys.Add(60, "História e Geo. Portugal");
                keys.Add(61, "História");
                keys.Add(62, "História Cult. das Artes");
                keys.Add(63, "História A");
                keys.Add(64, "História B");
                keys.Add(65, "Geografia");
                keys.Add(66, "Geografia A");
                keys.Add(68, "Geografia C");

                keys.Add(80, "Música");
                keys.Add(81, "Educação Moral e R.C.");
                keys.Add(82, "TIC");
                keys.Add(83, "Artes");
                keys.Add(84, "Educação Física");
                keys.Add(85, "Aplicações Informáticas");


                return keys;
            }
        }

        public static Dictionary<int, string> GetDisciplinas() => __Disciplinas;

    }
}
