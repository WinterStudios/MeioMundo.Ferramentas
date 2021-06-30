using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Voucher.Modelo
{
    public class GiftCardManagerModel
    {
        public int Price { get; set; }
        public int QtdPrint { get; set; }
        public int QtdAvalable { get; set; }
        public int QtdCirculante { get; set; }
        public int QtdUsed { get; set; }
    }
    public class GiftCard
    {
        public int Value { get; set; }
        public int SerialNumber { get; set; }
    }
    public enum GiftCardStatus
    {
        Avalable = 0,
        Client = 1,
        Used = 2
    }
}
