using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace MottuBracelet.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MlController : ControllerBase
    {
        // Dados de treino simulados
        private static readonly List<PatioOcupacaoInput> DadosTreino = new()
        {
            new PatioOcupacaoInput { MotosAtuais = 10, EntradaMedia = 5, SaidaMedia = 2, OcupacaoFutura = 13 },
            new PatioOcupacaoInput { MotosAtuais = 20, EntradaMedia = 4, SaidaMedia = 3, OcupacaoFutura = 21 },
            new PatioOcupacaoInput { MotosAtuais = 30, EntradaMedia = 1, SaidaMedia = 4, OcupacaoFutura = 27 },
            new PatioOcupacaoInput { MotosAtuais = 40, EntradaMedia = 6, SaidaMedia = 2, OcupacaoFutura = 44 },
            new PatioOcupacaoInput { MotosAtuais = 15, EntradaMedia = 3, SaidaMedia = 1, OcupacaoFutura = 17 }
        };

        private readonly MLContext _mlContext;

        public MlController()
        {
            _mlContext = new MLContext();
        }

        [HttpPost("prever-ocupacao")]
        public ActionResult PreverOcupacao(PatioOcupacaoInput input)
        {
            var data = _mlContext.Data.LoadFromEnumerable(DadosTreino);

            var pipeline = _mlContext.Transforms.Concatenate("Features",
                    nameof(PatioOcupacaoInput.MotosAtuais),
                    nameof(PatioOcupacaoInput.EntradaMedia),
                    nameof(PatioOcupacaoInput.SaidaMedia))
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: "OcupacaoFutura"));

            var modelo = pipeline.Fit(data);

            var predictor =
                _mlContext.Model.CreatePredictionEngine<PatioOcupacaoInput, PatioOcupacaoOutput>(modelo);

            var resultado = predictor.Predict(input);

            return Ok(new
            {
                entrada = input,
                previsao = resultado.OcupacaoPrevista,
                situacao = resultado.OcupacaoPrevista >= input.CapacidadeMaxima
                    ? "Risco de lotação"
                    : "Ocupação dentro do normal"
            });
        }

        public class PatioOcupacaoInput
        {
            public float MotosAtuais { get; set; }
            public float EntradaMedia { get; set; }
            public float SaidaMedia { get; set; }
            public float CapacidadeMaxima { get; set; }
            public float OcupacaoFutura { get; set; }
        }

        public class PatioOcupacaoOutput
        {
            [ColumnName("Score")]
            public float OcupacaoPrevista { get; set; }
        }
    }
}
