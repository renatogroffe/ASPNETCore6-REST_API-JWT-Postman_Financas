using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using APIFinancas.Models;
using Calculos.Common;

namespace APIFinancas.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("Bearer")]
public class CalculoFinanceiroController : ControllerBase
{
    private readonly ILogger<CalculoFinanceiroController> _logger;

    public CalculoFinanceiroController(ILogger<CalculoFinanceiroController> logger)
    {
        _logger = logger;
    }

    [HttpGet("juroscompostos")]
    [ProducesResponseType(typeof(Emprestimo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FalhaCalculo), StatusCodes.Status400BadRequest)]
    public ActionResult<Emprestimo> Get(
        double valorEmprestimo, int numMeses, double percTaxa)
    {
        _logger.LogInformation(
            "Recebida nova requisi��o|" +
           $"Valor do empr�stimo: {valorEmprestimo}|" +
           $"N�mero de meses: {numMeses}|" +
           $"% Taxa de Juros: {percTaxa}");

        // FIXME: Codigo comentado para simulacaoo de falhas em testes automatizados
        /*if (valorEmprestimo <= 0)
            return GerarResultParamInvalido("Valor do Emprestimo");
        if (numMeses <= 0)
            return GerarResultParamInvalido("Numero de Meses");
        if (percTaxa <= 0)
            return GerarResultParamInvalido("Percentual da Taxa de Juros");*/

        double valorFinalJuros =
            CalculoFinanceiro.CalcularValorComJurosCompostos(
                valorEmprestimo, numMeses, percTaxa);
        _logger.LogInformation($"Valor Final com Juros: {valorFinalJuros}");

        return new Emprestimo()
        {
            ValorEmprestimo = valorEmprestimo,
            NumMeses = numMeses,
            TaxaPercentual = percTaxa,
            ValorFinalComJuros = valorFinalJuros
        };
    }

    private BadRequestObjectResult GerarResultParamInvalido(
        string nomeCampo)
    {
        var erro = $"O {nomeCampo} deve ser maior do que zero!";
        _logger.LogError(erro);
        return new BadRequestObjectResult(
            new FalhaCalculo() { Mensagem = erro });
    }
}
