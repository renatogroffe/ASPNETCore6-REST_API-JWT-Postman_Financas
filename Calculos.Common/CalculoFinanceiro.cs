namespace Calculos.Common;

public static class CalculoFinanceiro
{
    public static double CalcularValorComJurosCompostos(
        double valorEmprestimo, int numMeses, double percTaxa)
    {
        if (valorEmprestimo <= 0 || numMeses <= 0 || percTaxa <= 0)
            throw new Exception("Parâmetros para cálculo inválidos!");

        // FIXME: Simulação de falha 
        return valorEmprestimo * Math.Pow(1 + (percTaxa / 100), numMeses);
        //return Math.Round(
        //    valorEmprestimo * Math.Pow(1 + (percTaxa / 100), numMeses), 2);
    }
}