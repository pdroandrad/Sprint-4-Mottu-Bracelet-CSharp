using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.Model;
using MottuBracelet.Services;
using Xunit;

namespace MottuBracelet.Tests.Services
{
    public class ServicoDispositivosTests
    {
        private AppDbContext NovoContexto()
        {
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(opts);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarDispositivo()
        {
            var ctx = NovoContexto();
            var servico = new ServicoDispositivos(ctx);

            var dispositivo = new Dispositivo
            {
                StatusDispositivo = "Ativo"
            };

            var criado = await servico.CriarAsync(dispositivo);

            Assert.NotNull(criado);
            Assert.Equal("Ativo", criado.StatusDispositivo);
            Assert.Equal(1, ctx.Dispositivo.Count());
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarDispositivo()
        {
            var ctx = NovoContexto();
            var servico = new ServicoDispositivos(ctx);

            ctx.Dispositivo.Add(new Dispositivo { StatusDispositivo = "Ocioso" });
            await ctx.SaveChangesAsync();

            var dispositivo = await servico.ObterPorIdAsync(1);

            Assert.NotNull(dispositivo);
            Assert.Equal("Ocioso", dispositivo.StatusDispositivo);
        }

        [Fact]
        public async Task RemoverAsync_DeveRemoverDispositivo()
        {
            var ctx = NovoContexto();
            var servico = new ServicoDispositivos(ctx);

            ctx.Dispositivo.Add(new Dispositivo { StatusDispositivo = "Falha" });
            await ctx.SaveChangesAsync();

            var removido = await servico.RemoverAsync(1);

            Assert.True(removido);
            Assert.Empty(ctx.Dispositivo);
        }
    }
}
