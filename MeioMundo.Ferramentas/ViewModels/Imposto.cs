using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MeioMundo.Ferramentas.Internal;

namespace MeioMundo.Ferramentas.ViewModels
{
    public class TipoImposto : ViewModelBase
    {
        public float Value { get => _value; set { _value = value; OnPropertyChanged(); } }
        public string Nome { get => _nome; set { _nome = value; OnPropertyChanged(); } }
        public Taxa TipoTaxa { get => _tipoTaxa; set { _tipoTaxa = SetTipoTaxa(value); OnPropertyChanged(); } }
        

        private float _value;
        private string _nome;
        private Taxa _tipoTaxa;
        public enum Taxa
        {
            Isento,
            TaxaReduzida,
            TaxaIntermedia,
            TaxaNormal,

            Unknow
        }

        private Taxa SetTipoTaxa(Taxa value)
        {
            switch (value)
            {
                case Taxa.Isento:
                    Nome = "Isento";
                    Value = 0;
                    break;
                case Taxa.TaxaReduzida:
                    Nome = "Taxa Reduzida";
                    Value = 6;
                    break;
                case Taxa.TaxaIntermedia:
                    Nome = "Taxa Intermédia";
                    Value = 13;
                    break;
                case Taxa.TaxaNormal:
                    Nome = "Normal";
                    Value = 23;
                    break;
                case Taxa.Unknow:
                    Nome = "?";
                    value = 0;
                    break;
            }
            return value;
        }

        /// <summary>
        /// Converts the string representation of a text to type of tax.
        /// </summary>
        /// <param name="s">A string that contains a number to convert.</param>
        /// <returns>A tax equivalent to the type of tax specified in s</returns>
        /// <exception cref="FormatException">s does not represent a number in a valid format.</exception>
        public static TipoImposto Parse(string s)
        {
            try
            {
                TipoImposto tipoImposto = new TipoImposto();
                switch (s)
                {
                    case "IVA Taxa Normal":
                        tipoImposto.TipoTaxa = Taxa.TaxaNormal;
                        return tipoImposto;
                    case "Iva Taxa Intermédia":
                        tipoImposto.TipoTaxa = Taxa.TaxaReduzida;
                        return tipoImposto;
                    case "Iva Taxa Reduzida":
                        tipoImposto.TipoTaxa = Taxa.TaxaReduzida;
                        return tipoImposto;
                    case "Isento":
                        tipoImposto.TipoTaxa = Taxa.TaxaReduzida;
                        return tipoImposto;
                    default:
                        throw new Exception(s);
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
    
}
