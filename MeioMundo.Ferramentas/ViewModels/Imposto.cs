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
        public string Percentagem { get => _percentagem; set { _percentagem = value; OnPropertyChanged(); } }
        public string Nome { get => _nome; set { _nome = value; OnPropertyChanged(); } }
        public Taxa TipoTaxa { get => _tipoTaxa; set { _tipoTaxa = SetTipoTaxa(value); OnPropertyChanged(); } }
        

        private float _value;
        private string _percentagem;
        private string _nome;
        private Taxa _tipoTaxa;

        private Taxa SetTipoTaxa(Taxa value)
        {
            switch (value)
            {
                case Taxa.Isento:
                    Nome = "Isento";
                    Value = 0f;
                    Percentagem = $"{Value * 100} %";
                    break;
                case Taxa.TaxaReduzida:
                    Nome = "Taxa Reduzida";
                    Value = .06f;
                    Percentagem = $"{Value * 100} %";
                    break;
                case Taxa.TaxaIntermedia:
                    Nome = "Taxa Intermédia";
                    Value = .13f;
                    Percentagem = $"{Value * 100} %";
                    break;
                case Taxa.TaxaNormal:
                    Nome = "Normal";
                    Value = .23f;
                    Percentagem = $"{(Value * 100f).ToString("0")} %";
                    break;
                case Taxa.Unknow:
                    Nome = "?";
                    value = 0;
                    break;
            }
            return value;
        }



        public static TipoImposto[] GetTipoImpostos()
        {
            List<TipoImposto> list = new List<TipoImposto>();
            foreach (var item in Enum.GetValues<Taxa>()) 
            {
                TipoImposto imposto = new TipoImposto();
                imposto.SetTipoTaxa(item);
                list.Add(imposto);
            }
            return list.ToArray();
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
                    case "":
                        tipoImposto.TipoTaxa = Taxa.TaxaNormal;
                        return tipoImposto;
                        
                }
            }
            catch 
            {
                throw new Exception(s);
            }
            return null;
        }
        public static TipoImposto TryParse(string s)
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
                        tipoImposto.TipoTaxa = Taxa.Isento;
                        return tipoImposto;

                    // FROM THE WEBSITE 
                    case "taxa-reduzida":
                        tipoImposto.TipoTaxa = Taxa.TaxaReduzida;
                        return tipoImposto;
                    case "":
                        tipoImposto.TipoTaxa = Taxa.TaxaNormal;
                        return tipoImposto;

                    default:
                        tipoImposto.TipoTaxa = Taxa.TaxaNormal;
                        return tipoImposto;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public override string ToString()
        {
            return $"{Nome} {Percentagem}";
        }
    }
    
}
