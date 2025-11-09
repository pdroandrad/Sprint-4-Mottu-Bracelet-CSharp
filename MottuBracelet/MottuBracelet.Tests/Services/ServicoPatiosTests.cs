using Microsoft.EntityFrameworkCore;
using MottuBracelet.Data;
using MottuBracelet.Model;
using MottuBracelet.Services;
using Xunit;

namespace MottuBracelet.Tests.Services
{
    public class ServicoPatiosTests
    {
        private AppDbContext NovoContexto()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarPatio()
        {
            var ctx = NovoContexto();
            var servico = new ServicoPatios(ctx);

            var patio = new Patio
            {
                Nome = "Pátio Central",
                CapacidadeMaxima = 50,
                AdministradorResponsavel = "João",
                Endereco = new Endereco
                {
                    Logradouro = "Rua A",
                    Numero = 10,
                    Cidade = "SP",
                    Pais = "BR",
                    Cep = "12345-000"
                }
            };

            var criado = await servico.CriarAsync(patio);

            Assert.NotNull(criado);
            Assert.Equal("Pátio Central", criado.Nome);
            Assert.Equal(1, ctx.Patio.Count());
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarPatio()
        {
            var ctx = NovoContexto();
            var servico = new ServicoPatios(ctx);

            ctx.Patio.Add(new Patio
            {
                Nome = "Teste",
                CapacidadeMaxima = 20,
                AdministradorResponsavel = "Maria",
                Endereco = new Endereco()
            });

            await ctx.SaveChangesAsync();

            var patio = await servico.ObterPorIdAsync(1);

            Assert.NotNull(patio);
            Assert.Equal("Teste", patio.Nome);
        }

        [Fact]
        public async Task RemoverAsync_DeveRemoverPatio()
        {
            var ctx = NovoContexto();
            var servico = new ServicoPatios(ctx);

            ctx.Patio.Add(new Patio
            {
                Nome = "Remover",
                CapacidadeMaxima = 10,
                AdministradorResponsavel = "Pedro",
                Endereco = new Endereco()
            });
            await ctx.SaveChangesAsync();

            var removido = await servico.RemoverAsync(1);

            Assert.True(removido);
            Assert.Empty(ctx.Patio);
        }
    }
}
