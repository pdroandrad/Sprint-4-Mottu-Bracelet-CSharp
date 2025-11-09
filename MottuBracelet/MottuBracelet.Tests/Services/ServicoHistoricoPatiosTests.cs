using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.Model;
using MottuBracelet.Services;
using Xunit;

namespace MottuBracelet.Tests.Services
{
    public class ServicoHistoricoPatiosTests
    {
        private AppDbContext NovoContexto()
        {
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(opts);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarHistorico()
        {
            var ctx = NovoContexto();
            var servico = new ServicoHistoricoPatios(ctx);

            var historico = new HistoricoPatio
            {
                MotoId = null,
                PatioId = null,
                DataEntrada = DateTime.Now
            };

            var criado = await servico.CriarAsync(historico);

            Assert.NotNull(criado);
            Assert.Single(ctx.HistoricoPatio);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarHistorico()
        {
            var ctx = NovoContexto();
            var servico = new ServicoHistoricoPatios(ctx);

            ctx.HistoricoPatio.Add(new HistoricoPatio
            {
                DataEntrada = DateTime.Now
            });
            await ctx.SaveChangesAsync();

            var historico = await servico.ObterPorIdAsync(1);

            Assert.NotNull(historico);
            Assert.Null(historico.MotoId);
        }

        [Fact]
        public async Task RemoverAsync_DeveRemoverHistorico()
        {
            var ctx = NovoContexto();
            var servico = new ServicoHistoricoPatios(ctx);

            ctx.HistoricoPatio.Add(new HistoricoPatio
            {
                DataEntrada = DateTime.Now
            });
            await ctx.SaveChangesAsync();

            var removido = await servico.RemoverAsync(1);

            Assert.True(removido);
            Assert.Empty(ctx.HistoricoPatio);
        }
    }
}
