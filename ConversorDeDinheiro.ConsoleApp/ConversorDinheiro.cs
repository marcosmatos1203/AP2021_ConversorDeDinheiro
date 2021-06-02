using System;
using System.Collections.Generic;

namespace ConversorDeDinheiro.ConsoleApp
{
    public class ConversorDinheiro
    {
        private decimal quantia;
        private readonly Dictionary<decimal, string> numerosPorExtenso =
            new Dictionary<decimal, string>();

        private readonly Dictionary<decimal, string> centenas =
            new Dictionary<decimal, string>();

        public ConversorDinheiro(decimal quantia)
        {
            this.quantia = quantia;
            ConfigurarListagemDeNumeros();
            ConfigurarListagensDeCentenas();
        }

        public string PorExtenso()
        {
            string resultado = "Operação inválida";

            int valorReais = (int)Math.Truncate(quantia);
            decimal valorCentavos = quantia - (int)Math.Truncate(quantia);

            List<string> dinheiroPorExtenso = new List<string>();

            if (valorReais > 0)
            {
                dinheiroPorExtenso.AddRange(ConverterReaisPorExtenso(valorReais));

                string reais;

                if (valorReais % 1000000 == 0)
                    reais = "de reais";
                else
                    reais = valorReais > 1 ? "reais" : "real";                

                dinheiroPorExtenso.Add(reais);
            }            

            if (valorReais > 0 && valorCentavos > 0)
                dinheiroPorExtenso.Add("e");

            if (valorCentavos > 0)
            {
                dinheiroPorExtenso.AddRange(ConverterCentavosPorExtenso(valorCentavos));

                int quantiaEmCentavos = (int)(valorCentavos * 100);

                string centavos;

                if (valorReais == 0)
                    centavos = quantiaEmCentavos > 1 ? "centavos de real" : "centavo de real";
                else
                    centavos = quantiaEmCentavos > 1 ? "centavos" : "centavo";

                dinheiroPorExtenso.Add(centavos);
            }

            if (dinheiroPorExtenso.Count > 0)
                resultado = string.Join(" ", dinheiroPorExtenso.ToArray());

            return resultado.ToSentenceCase();
        }

        #region métodos privados

        private List<string> ConverterReaisPorExtenso(int valor, bool temConjuncao = true)
        {
            List<string> quantiaPorExtenso = new List<string>();

            if (valor >= 1000000)
            {
                int valorMilhar = valor / 1000000;

                if (valorMilhar >= 20)
                    quantiaPorExtenso.AddRange(ConverterReaisPorExtenso(valorMilhar));
                else
                    quantiaPorExtenso.Add(numerosPorExtenso[valorMilhar]);

                if (valor >= 2000000)
                    quantiaPorExtenso.Add("milhões");
                else
                    quantiaPorExtenso.Add("milhão");

                valor %= 1000000;
            }

            if (valor >= 1000)
            {
                int valorRestoMilhar = valor % 1000;

                int valorMilhar = valor / 1000;

                if (valorMilhar >= 100)
                    quantiaPorExtenso.AddRange(ConverterReaisPorExtenso(valorMilhar));

                else if (valorMilhar >= 20)
                    quantiaPorExtenso.AddRange(ConverterReaisPorExtenso(valorMilhar, false));

                else
                    quantiaPorExtenso.Add(numerosPorExtenso[valorMilhar]);

                quantiaPorExtenso.Add("mil");

                if (centenas.ContainsKey(valorRestoMilhar))
                    quantiaPorExtenso.Add("e");

                valor %= 1000;
            }

            if (valor >= 100)
            {
                if (valor == 100)
                    quantiaPorExtenso.Add("Cem");
                else
                    quantiaPorExtenso.Add(ObtemNumeroPorExtenso(valor, 100));

                valor %= 100;

                if (valor > 0 && temConjuncao)
                    quantiaPorExtenso.Add("e");
            }

            if (valor >= 20)
            {
                quantiaPorExtenso.Add(ObtemNumeroPorExtenso(valor, 10));

                valor %= 10;

                if (valor > 0 && temConjuncao)
                    quantiaPorExtenso.Add("e");
            }

            if (valor > 0 && valor < 19)
            {
                quantiaPorExtenso.Add(numerosPorExtenso[valor]);
            }

            return quantiaPorExtenso;
        }

        private List<string> ConverterCentavosPorExtenso(decimal valor)
        {
            int quantiaEmCentavos = (int)(valor * 100);

            List<string> quantiaPorExtenso = new List<string>();

            if (quantiaEmCentavos >= 20)
            {
                quantiaPorExtenso.Add(ObtemNumeroPorExtenso(quantiaEmCentavos, 10));

                quantiaEmCentavos %= 10;

                if (quantiaEmCentavos > 0)
                    quantiaPorExtenso.Add("e");
            }

            if (quantiaEmCentavos > 0 && quantiaEmCentavos < 19)
            {
                quantiaPorExtenso.Add(numerosPorExtenso[quantiaEmCentavos]);
            }

            return quantiaPorExtenso;
        }

        public string ObtemNumeroPorExtenso(int quantia, int unidade)
        {
            int valorResto = quantia % unidade;

            int valor = quantia - valorResto;

            return numerosPorExtenso[valor];
        }

        private void ConfigurarListagensDeCentenas()
        {
            centenas.Add(100, "cem");
            centenas.Add(200, "duzentos");
            centenas.Add(300, "trezentos");
            centenas.Add(400, "quatrocentos");
            centenas.Add(500, "quinhentos");
            centenas.Add(600, "seiscentos");
            centenas.Add(700, "setecentos");
            centenas.Add(800, "oitocentos");
            centenas.Add(900, "novecentos");
        }

        private void ConfigurarListagemDeNumeros()
        {
            numerosPorExtenso.Add(1, "um");
            numerosPorExtenso.Add(2, "dois");
            numerosPorExtenso.Add(3, "três");
            numerosPorExtenso.Add(4, "quatro");
            numerosPorExtenso.Add(5, "cinco");
            numerosPorExtenso.Add(6, "seis");
            numerosPorExtenso.Add(7, "sete");
            numerosPorExtenso.Add(8, "oito");
            numerosPorExtenso.Add(9, "nove");
            numerosPorExtenso.Add(10, "dez");
            numerosPorExtenso.Add(11, "onze");
            numerosPorExtenso.Add(12, "doze");
            numerosPorExtenso.Add(13, "treze");
            numerosPorExtenso.Add(14, "quatorze");
            numerosPorExtenso.Add(15, "quinze");
            numerosPorExtenso.Add(16, "dezesseis");
            numerosPorExtenso.Add(17, "dezessete");
            numerosPorExtenso.Add(18, "dezoito");
            numerosPorExtenso.Add(19, "dezenove");
            numerosPorExtenso.Add(20, "vinte");
            numerosPorExtenso.Add(30, "trinta");
            numerosPorExtenso.Add(40, "quarenta");
            numerosPorExtenso.Add(50, "cinquenta");
            numerosPorExtenso.Add(60, "sessenta");
            numerosPorExtenso.Add(70, "setenta");
            numerosPorExtenso.Add(80, "oitenta");
            numerosPorExtenso.Add(90, "noventa");
            numerosPorExtenso.Add(100, "cento");
            numerosPorExtenso.Add(200, "duzentos");
            numerosPorExtenso.Add(300, "trezentos");
            numerosPorExtenso.Add(400, "quatrocentos");
            numerosPorExtenso.Add(500, "quinhentos");
            numerosPorExtenso.Add(600, "seiscentos");
            numerosPorExtenso.Add(700, "setecentos");
            numerosPorExtenso.Add(800, "oitocentos");
            numerosPorExtenso.Add(900, "novecentos");
        }

        #endregion
    }

}
