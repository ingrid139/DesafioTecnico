using AutoFixture;
using Intituicao.Financeira.Application.Shared.Repository.Models;

namespace Instituicao.Financeira.Tests
{
    public class DefaultFixture
    {
        private readonly Fixture _fixture;
        public DefaultFixture()
        {
            _fixture = new Fixture();
        }
        public ContratoDto ContratoMock(Guid id)
        {
            return _fixture.Build<ContratoDto>()
                    .With(x => x.Id, id)
                    .With(x => x.PrazoMeses, 12)
                    .With(x => x.TaxaMensal, 0.1M)
                    .With(x => x.TipoVeiculoId, 1)
                    .With(x => x.CondicaoVeiculoId, 1)
                    .With(x => x.ValorTotal, 30000M)
                    .With(x => x.ClienteCpfCnpj, 13456)
                    .With(x => x.DataVencimentoPrimeiraParcela, DateTime.Now.AddDays(30)) 
                    .With(x => x.Pagamentos, new List<PagamentoDto>()
                    {
                        new()
                        {
                            Id = Guid.NewGuid(),
                            ContratoId = id,
                            DataPagamento = DateTime.Now.AddDays(15),
                            DataVencimento = DateTime.Now.AddDays(30),
                            StatusPagamentoId = 1,
                            SaldoDevedor = 27500M
                        },
                        new()
                        {
                            Id = Guid.NewGuid(),
                            ContratoId = id,
                            DataPagamento = DateTime.Now.AddDays(45),
                            DataVencimento = DateTime.Now.AddDays(60),
                            StatusPagamentoId = 1,
                            SaldoDevedor = 26500M
                        },
                        new()
                        {
                            Id = Guid.NewGuid(),
                            ContratoId = id,
                            DataPagamento = DateTime.Now.AddDays(120),
                            DataVencimento = DateTime.Now.AddDays(90),
                            StatusPagamentoId = 2,
                            SaldoDevedor = 25500M
                        },
                    }.ToArray)
                    .Create();
        }

        public PagamentoDto PagamentoMock(Guid id, Guid contratoId)
        {
            return _fixture.Build<PagamentoDto>()
                    .With(x => x.Id, id)
                    .With(x => x.ContratoId, contratoId)
                    .With(x => x.DataPagamento, DateTime.Now.AddDays(60))
                    .With(x => x.DataVencimento, DateTime.Now.AddDays(61))
                    .With(x => x.StatusPagamentoId, 1)
                    .With(x => x.SaldoDevedor, 25000M)
                    .Create();
        }

        public TipoVeiculoDto TipoVeiculoMock()
        {
            return _fixture.Build<TipoVeiculoDto>()
                    .With(x => x.Id, 1)
                    .With(x => x.Descricao, "Automovel")
                    .Create();
        }
        public StatusPagamentoDto StatusPagamentoMock()
        {
            return _fixture.Build<StatusPagamentoDto>()
                    .With(x => x.Id, 1)
                    .With(x => x.Descricao, "Em dia")
                    .Create();
        }

        public CondicaoVeiculoDto CondicaoVeiculoMock()
        {
            return _fixture.Build<CondicaoVeiculoDto>()
                    .With(x => x.Id, 1)
                    .With(x => x.Descricao, "Novo")
                    .Create();
        }
    }
}
