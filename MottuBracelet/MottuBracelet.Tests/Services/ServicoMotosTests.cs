using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.Model;
using MottuBracelet.Services;
using Xunit;

namespace MottuBracelet.Tests.Services
{
    public class ServicoMotosTests
    {
        private AppDbContext NovoContexto()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarMoto()
        {
            var contexto = NovoContexto();
            var servico = new ServicoMotos(contexto);

            var moto = new Moto { Imei = "123", Placa = "ABC123" };

            var criada = await servico.CriarAsync(moto);

            Assert.NotNull(criada);
            Assert.Equal("ABC123", criada.Placa);
            Assert.Equal(1, contexto.Moto.Count());
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarMoto()
        {
            var contexto = NovoContexto();
            var servico = new ServicoMotos(contexto);

            contexto.Moto.Add(new Moto { Imei = "999", Placa = "XYZ999" });
            await contexto.SaveChangesAsync();

            var moto = await servico.ObterPorIdAsync(1);

            Assert.NotNull(moto);
            Assert.Equal("XYZ999", moto.Placa);
        }

        [Fact]
        public async Task RemoverAsync_DeveRemoverMoto()
        {
            var contexto = NovoContexto();
            var servico = new ServicoMotos(contexto);

            contexto.Moto.Add(new Moto { Imei = "000", Placa = "DEL123" });
            await contexto.SaveChangesAsync();

            var removido = await servico.RemoverAsync(1);

            Assert.True(removido);
            Assert.Empty(contexto.Moto);
        }
    }
}
